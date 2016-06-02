using System.Collections.Concurrent;
using System.Security.AccessControl;

namespace Seif.Rpc.Registry
{
    /// <summary>
    /// 注册数据的存储对象。关键字为（ApiDomain，ServerAddress，InterfaceName）
    /// </summary>
    public class RegistryDataInfo
    {
        #region Application Data
        public string ApiDomain { get; set; }

        #endregion Application Data

        #region Server Data
        public string ServerAddress { get; set; }

        #endregion 

        #region Service Data

        public string Id { get; set; }
        public string InterfaceName { get; set; }
        public string InstanceName { get; set; }
        public string Protocol { get; set; }
        public string SerializeMode { get; set; }
        public bool IsEnabled { get; set; }
        public string Remark { get; set; }
        public string AdditionalFields { get; set; }

        #endregion

        #region Method Data

        // Not In This Version

        #endregion

        #region Identifier

        public string Identifier
        {
            get { return string.Format("{0}-{1}-{2}", ApiDomain, ServerAddress, InterfaceName); }
        }

        #endregion

    }
}