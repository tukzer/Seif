using System;
using System.Collections.Generic;
using Seif.Rpc.Utils;

namespace Seif.Rpc.Soa
{
    public class Url
    {
        private string _protocol;
        private string _username;
        private string _password;
        private string _host;
        private int _port;
        private string _path;
        private IDictionary<string, string> _parameters;

        protected Url()
        {
            this._protocol = "";
            this._username = "";
            this._password = "";
            this._host = "";
            this._port = 0;
            this._path = "";
            this._parameters = new Dictionary<string, string>();
        }

        public string Username
        {
            get { return _username; }
        }

        public string Password
        {
            get { return _password; }
        }

        public string Host
        {
            get { return _host; }
        }

        public int Port
        {
            get { return _port; }
        }

        public string Path
        {
            get { return _path; }
        }

        public IDictionary<string, string> Parameters
        {
            get { return _parameters; }
        }

        public string Protocol
        {
            get { return _protocol; }
        }

        #region Constructors

        public Url(string protocol, string host, int port) :
            this(protocol, null, null, host, port, null, (IDictionary<string, string>) null)
        {
        }

        public Url(string protocol, string host, int port, IDictionary<string, string> parameters)
            : this(protocol, null, null, host, port, null, parameters)
        {

        }

        public Url(string protocol, string host, int port, string path)
            : this(protocol, null, null, host, port, path, (IDictionary<string, string>) null)
        {

        }

        public Url(string protocol, string host, int port, string path, IDictionary<string, string> parameters)
            : this(protocol, null, null, host, port, path, parameters)
        {

        }

        public Url(string protocol, string username, string password, string host, int port, string path)
            : this(protocol, username, password, host, port, path, (IDictionary<string, string>) null)
        {

        }

        public Url(string protocol, string username, string password, string host, int port, string path,
            IDictionary<string, string> parameters)
        {
            if (string.IsNullOrWhiteSpace(_username) && string.IsNullOrWhiteSpace(_password))
            {
                throw new ArgumentException("Invalid url, password without username!");
            }
            this._protocol = protocol;
            this._username = username;
            this._password = password;
            this._host = host;
            this._port = (port < 0 ? 0 : port);
            this._path = path;
            // trim the beginning "/"
            while (path != null && path.StartsWith("/"))
            {
                path = path.Substring(1);
            }

            this._parameters = parameters == null
                ? new Dictionary<string, string>()
                : new Dictionary<string, string>(parameters);
        }

        #endregion

        public static Url Parse(String url)
        {
            if (url == null || (url = url.Trim()).Length == 0)
            {
                throw new ArgumentException("url == null");
            }
            String protocol = null;
            String username = null;
            String password = null;
            String host = null;
            int port = 0;
            String path = null;
            IDictionary<String, String> parameters = null;
            int i = url.IndexOf("?", StringComparison.Ordinal); // seperator between body and parameters 
            if (i >= 0)
            {
                String[] parts = url.Substring(i + 1).Split('&');
                parameters = new Dictionary<String, String>();

                foreach (var item in parts)
                {
                    string pair = item.Trim();
                    if (pair.Length > 0)
                    {
                        int j = pair.IndexOf('=');
                        if (j >= 0)
                        {
                            parameters.Add(pair.Substring(0,j), pair.Substring(j + 1));
                        }
                        else
                        {
                            parameters.Add(pair, pair);
                        }
                    }
                }
                url = url.Substring(0, i);
            }
            i = url.IndexOf("://", StringComparison.Ordinal);
            if (i >= 0)
            {
                if (i == 0) throw new ArgumentException("url missing protocol: \"" + url + "\"");
                protocol = url.Substring(0, i);
                url = url.Substring(i + 3);
            }
            else
            {
                // case: file:/path/to/file.txt
                i = url.IndexOf(":/", StringComparison.Ordinal);
                if (i >= 0)
                {
                    if (i == 0) throw new ArgumentException("url missing protocol: \"" + url + "\"");
                    protocol = url.Substring(0, i);
                    url = url.Substring(i + 1);
                }
            }

            i = url.IndexOf("/", StringComparison.Ordinal);
            if (i >= 0)
            {
                path = url.Substring(i + 1);
                url = url.Substring(0, i);
            }
            i = url.IndexOf("@", StringComparison.Ordinal);
            if (i >= 0)
            {
                username = url.Substring(0, i);
                int j = username.IndexOf(":", StringComparison.Ordinal);
                if (j >= 0)
                {
                    password = username.Substring(j + 1);
                    username = username.Substring(0, j);
                }
                url = url.Substring(i + 1);
            }
            i = url.IndexOf(":", StringComparison.Ordinal);
            if (i >= 0 && i < url.Length - 1)
            {
                port = int.Parse(url.Substring(i + 1));
                url = url.Substring(0, i);
            }
            if (url.Length > 0) host = url;
            return new Url(protocol, username, password, host, port, path, parameters);
        }

        #region

        public string GetIPAddress()
        {
            return NetUtils.GetIp(Host);
        }

        #endregion
    }
}