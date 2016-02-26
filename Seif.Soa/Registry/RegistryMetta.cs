namespace Seif.Soa.Registry
{
    public class RegistryMetta
    {
        /// <summary>
        /// 接口名称
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 接口完整名称
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// 服务地址
        /// </summary>
        public string ServiceUrl { get; set; }
        /// <summary>
        /// 服务默认超时时间
        /// </summary>
        public int Timeout { get; set; }
        /// <summary>
        /// 使用协议类型
        /// </summary>
        public string Protocol { get; set; }
        /// <summary>
        /// 初始权重
        /// </summary>
        public int Priority { get; set; }
    }
}