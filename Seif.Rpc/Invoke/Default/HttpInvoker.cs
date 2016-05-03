using System;
using System.Collections.Generic;
using System.Linq;

namespace Seif.Rpc.Invoke.Default
{
    public class HttpInvoker : IInvoker
    {
        private readonly string _url;
        public ISerializer Serializer { get;　private set; }

        public HttpInvoker(string url, ISerializer serializer)
        {
            Serializer = serializer;
            _url = url;
        }

        public string Url
        {
            get { return _url; }
        }

        public InvokeResult Invoke(IInvocation invocation)
        {
            if (invocation == null)
                throw  new Exception("Error Invoke Parameters");

            string httpVerb = invocation.Attributes.ContainsKey(AttrKeys.HttpMethod)
                ? invocation.Attributes[AttrKeys.HttpMethod]
                : "POST";

            var apiEntrance = GetApiEntrance(invocation.Attributes, invocation.ServiceName, invocation.MethodName, httpVerb);

            switch (httpVerb)
            {
                case "Get":
                    return DoGet(this.Url, apiEntrance);
                default:
                    var requestJson = DoSerialize(invocation.Parameters);
                    return DoPost(this.Url, apiEntrance, requestJson);
            }
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

            return string.Join("&", attributes.Select(p => p.Key).Concat(new [] {serviceName, methodName}));
        }

        protected InvokeResult DoGet(string requestAddr, string endpoint)
        {
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri(endpoint);
            //    client.DefaultRequestHeaders.Accept.Add(
            //        new MediaTypeWithQualityHeaderValue("application/json"));

            //    var entry = string.Format("{0}/{1}", endpoint, ReplaceUrlParams(requestAddr));
            //    var taskRes = client.GetAsync(entry);
            //    return taskRes.Result.Content.ReadAsAsync<T>().Result;
            //}
            throw new NotImplementedException();
        }

        protected InvokeResult DoPost(string endpoint, string apiAddr, string requestJson = "{}", bool checkParams = true)
        {
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri(endpoint);
            //    client.DefaultRequestHeaders.Accept.Add(
            //        new MediaTypeWithQualityHeaderValue("application/json"));
            //    //client.DefaultRequestHeaders.cont
            //    //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            //    var entry = string.Format("{0}/{1}", endpoint, ReplaceUrlParams(apiAddr.Trim('/'), checkParams));
            //    MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
            //    var taskRes = client.PostAsync(entry, requestJson, jsonFormatter);
            //    return taskRes.Result.Content.ReadAsAsync<T>().Result;
            //}
            throw new NotImplementedException();
        }

    }
}