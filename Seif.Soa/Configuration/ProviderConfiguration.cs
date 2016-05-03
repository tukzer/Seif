namespace Seif.Soa.Configuration
{
    public class ProviderConfiguration
    {
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