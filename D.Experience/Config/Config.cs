using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Core
{
    public class Config
    {
        /// <summary>
        /// ctc数据地址
        /// </summary>
        public string CtcPlanAddress { get; set; }
        /// <summary>
        /// 主服务端口
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// ctc数据端口
        /// </summary>
        public int CtcPlanPort { get; set; }
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// 数据中心编号
        /// </summary>
        public int DataCenterId { get; set; }
        /// <summary>
        /// 服务器编号
        /// </summary>
        public int ServerId { get; set; }
    }
}
