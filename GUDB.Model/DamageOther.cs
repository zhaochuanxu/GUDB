using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUDB.Model
{
    [Table("DamageOther")]
    /// <summary>
    /// 8.	地质灾害所有其它损失详细列表
    /// </summary>
    public class DamageOther
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  //设置自增


        /// <summary>
        /// (1)	其它损失编码(主键；不可空) [类型：字符串]
        /// </summary>
        public string DOId { get; set; }

        /// <summary>
        /// (2)	对应的地质灾害事件编码(外键；不可空) [类型：字符串]；
        /// </summary>
        public string EId { get; set; }

        /// <summary>
        /// (3)	损失描述 [类型：字符串]；
        /// </summary>
        public string DODes { get; set; }

        /// <summary>
        /// (4)	损失数量 [类型：实数]；
        /// </summary>
        public double DONumber { get; set; }


        /// <summary>
        /// (5)	损失折算金额 [类型：实数；单位：人民币元]；
        /// </summary>
        public double DOMoney { get; set; }

        /// <summary>
        /// (6)	损失产权人单位或个人姓名 [类型：字符串]；
        /// </summary>
        public string DOName { get; set; }

        /// <summary>
        /// (7)	损失产权人单位或个人联系电话 [类型：字符串]；
        /// </summary>
        public string DOPhone { get; set; }

        /// <summary>
        /// （8):其他说明    [字符串]
        /// </summary>
        public string DOOtherDes { get; set; }

        /// <summary>
        /// （(9)	地质灾害实况(所有其它损失)调查人员-身份证号码(外键) [类型：字符串]；
        /// </summary>
        public string IId { get; set; }

        /// <summary>
        /// 关联外键表   调查人员表 Investigator 
        /// </summary>
        public virtual Investigator Investigator { get; set; }


        public virtual Event Event { get; set; }
    }
}
