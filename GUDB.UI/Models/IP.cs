using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GUDB.UI.Models
{
    public class Ip
    {



        //ip属性
        /// </summary>var lo="山东省", lc="菏泽市"; var localAddress={city:"菏泽市", province:"山东省"}


        ///</summary> city 所在城市
        public string city { get; set; }

        /// <summary>
        /// regionNames  地区所在名字
        /// </summary>
        public string regionNames { set; get; }

        /// <summary>
        /// ip地区所在省份
        /// </summary>
        public string pro { get; set; }

        /// <summary>
        /// ip地址
        /// </summary>
        public string ip { set; get; }


        public DateTime Time { set; get; }
        public string addr { set; get; }

        /// <summary>
        /// 类的默认构造函数
        /// </summary>
        public Ip()
        {

        }


    }
}