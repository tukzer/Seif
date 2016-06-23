using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using Seif.Rpc.Invoke;
using Seif.Rpc.Registry;
using Seif.Rpc.Utils;
using ServiceStack;

namespace Seif.Rpc.Default
{
    public class HttpInvoker : IInvoker
    {
        private readonly string _url;
        private readonly IDictionary<string, string> _attributes;
        public ISerializer Serializer { get;　private set; }

        public HttpInvoker(string url, ISerializer serializer, IDictionary<string,string> attributes )
        {
            Serializer = serializer;
            _url = url;
            _attributes = attributes ?? new Dictionary<string, string>();
        }

        public string Url
        {
            get { return _url; }
        }

        public InvokeResult Invoke(IInvocation invocation)
        {
            if (invocation == null)
                throw  new SeifException("Error Invoke Parameters");

            try
            {
                return DoInvoke(invocation);
            }
            catch (WebException webEx)
            {
                return new InvokeResult
                {
                    Status = ResultStatus.ServerNotReachable,
                    Exceptions = new Exception[] {webEx},
                    Message = webEx.Message
                };
            }
            catch (Exception ex)
            {
                return new InvokeResult
                {
                    Status = ResultStatus.UnknownError,
                    Exceptions = new Exception[] { ex },
                    Message = ex.Message
                };
            }
        }

        protected virtual InvokeResult DoInvoke(IInvocation invocation)
        {
            // 合并调用参数
            var attributes = invocation.Attributes.MergeAndNew(_attributes);

            string httpVerb = invocation.Attributes.ContainsKey(AttrKeys.HttpMethod)
                ? invocation.Attributes[AttrKeys.HttpMethod]
                : "POST";

            var apiEntrance = GetApiEntrance(attributes, invocation.ServiceName, invocation.MethodName, httpVerb);
            var url = this.Url;

            if (!url.StartsWith("http"))
            {
                url = string.Format("http://{0}", url);
            }

            var payload = new RpcPayload
            {
                Parameters = invocation.Parameters,
                Attributes = invocation.Attributes
            };
            var requestJson = DoSerialize(payload);
            InvokeResult result;

            switch (httpVerb)
            {
                case "Get":
                    result = DoGet(url, apiEntrance, requestJson);
                    break;
                default:
                    result = DoPost(url, apiEntrance, requestJson);
                    break;
            }

            return result;
        }

        protected virtual ServiceRegistryMetta GetServiceMetta(string serviceName)
        {
            return null; //SeifApplication.AppEnv.GlobalConfiguration.Registry.GetServiceRegistryMetta<>()
        }


        protected virtual string DoSerialize(object request)
        {
            return Serializer.Serialize(request);
        }

        private string GetApiEntrance(IDictionary<string, string> attributes, string serviceName, string methodName, string verb)
        {
            var getEntry = attributes[AttrKeys.ApiGetEntrance];
            if (string.IsNullOrEmpty(getEntry))
                throw new Exception("Api Entry cannot be null");

            string paraUrl = MakeUrl(attributes, serviceName, methodName);

            switch (verb)
            {
                case "Get":
                    return MakeUrl(getEntry, paraUrl);
                default:
                    var postEntry = attributes[AttrKeys.ApiPostEntrance];
                    if (string.IsNullOrEmpty(getEntry))
                    {
                        return MakeUrl(getEntry, paraUrl);
                    }

                    return MakeUrl(postEntry, paraUrl);
            }
        }

        private string MakeUrl(string entry, string paraUrl)
        {
            if (entry.Contains("?"))
            {
                return entry + "&" + paraUrl;
            }

            return entry + "?" + paraUrl;
        }

        private string MakeUrl(IDictionary<string, string> attributes, string serviceName, string methodName)
        {
            //TODO:不是系统的关键字

            var sb = new StringBuilder();
            sb.AppendFormat("&{0}={1}", "svc", serviceName);
            sb.AppendFormat("&{0}={1}", "mtd", methodName);

            //foreach (var item in attributes)
            //{
            //    sb.AppendFormat("&{0}={1}", item.Key, HttpUtility.UrlEncode(item.Value));
            //}

            return sb.ToString();
        }

        protected InvokeResult DoGet(string endpoint, string requestAddr, string requestJson = "{}" )
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(endpoint);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var entry = string.Format("{0}/{1}", endpoint, requestAddr);
                var taskRes = client.GetAsync(entry);
                return taskRes.Result.Content.ReadAsAsync<InvokeResult>().Result;
            }
        }

        protected InvokeResult DoPost(string endpoint, string apiAddr, string requestJson = "{}", bool checkParams = true)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(endpoint);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                var entry = string.Format("{0}/{1}", endpoint, apiAddr);
                MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
                var taskRes = client.PostAsync(entry, requestJson, jsonFormatter);
                return taskRes.Result.Content.ReadAsAsync<InvokeResult>().Result;
            }
        }

    }
}