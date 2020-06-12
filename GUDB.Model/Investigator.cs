using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUDB.Model
{
    [Table("Investigator")]

    ///<Summary>
    ///5.	地质灾害实况调查人员名单
    ///</Summary>
    public class Investigator
    {


        /// <summary>
        /// (1)	身份证号码(主键；不可空) [类型：字符串]；
        /// </summary>
        [Key]
        public string IId{ get; set; }
        /// <summary>
        /// (2)	所在单位编码(外键；不可空) [类型：字符串]；
        /// </summary>
        public string UId { get; set; }
        /// <summary>
        /// (3)	姓名 [类型：字符串]
        /// </summary>
        public string IName{ get; set; }

        /// <summary>
        /// (4)	电话 [类型：字符串]；
        /// </summary>
        public string IPhone { get; set; }
        /// <summary>
        /// (5) 电邮 [类型 :字符串]
        /// </summary>
        public string IEmail { get; set; }
        /// <summary>
        /// (6)	其它联系方式 [类型：字符串]；
        /// </summary>
        public string IOtherConnect { get; set; }

        /// <summary>
        /// 外键连接表 Unit
        /// </summary>
        public virtual Unit Unit { get; set; }//如果没有声明TUsers对象，则UserID是一个普通的字段，没有外键关系




    }
}
