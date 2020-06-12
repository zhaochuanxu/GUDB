using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUDB.Model
{
    [Table("DamageBuilding")]
    /// <summary>
    /// 7.	地质灾害建筑损失详细列表
    /// </summary>

    public class DamageBuilding
    {
        /// <summary>
        /// (1)	建筑损失编码(主键；不可空) [类型：字符串]
        /// (1)	建筑损失编码(主键；不可空) [类型：字符串]
        /// </summary>
        /// 
        [Key]
        public string DBId { get; set; }


        /// <summary>
        /// (2)	对应的地质灾害事件编码(外键；不可空) [类型：字符串]；
        /// </summary>
        public string EId { get; set; }

        /// <summary>
        /// (3)	损失程度 [类型：单个字符]；
        /// </summary>
        public char DBLevel { get; set; }

        /// <summary>
        /// (4)	损失面积 [类型：实数；单位：平方米]；
        /// </summary>
        public double DBDamageSize { get; set; }


        /// <summary>
        /// (5)	损失折算金额 [类型：实数；单位：人民币元]；
        /// </summary>
        public double DBDamageMoney { get; set; }

        /// <summary>
        /// (6)	损失建筑产权人单位或个人姓名 [类型：字符串]
        /// </summary>
        public string DBDamageName { get; set; }




        /// <summary>
        /// (7)	损失建筑产权人单位或个人联系电话 [类型：字符串]
        /// </summary>
        public string DBDamagePhone { get; set; }

        /// <summary>
        /// (8)   其他说明[类型 ：字符串]
        /// </summary>
        public string DBOtherDes { get; set; }

        /// <summary>
        /// (9)	地质灾害实况(建筑损失)调查人员-身份证号码(外键) [类型：字符串]
        /// </summary>
        public string IId { get; set; }


        /// <summary>
        /// 关联外键表 调查 Ivestigator
        /// </summary>
        public virtual Investigator Investigator { get; set; }//如果没有声明TUsers对象，则UserID是一个普通的字段，没有外键关系

        /// <summary>
        ///关联外键表 Event 事件
        /// </summary>
        ///
        ///
        ///


        public virtual Event Event { get; set; }//如果没有声明TUsers对象，则UserID是一个普通的字段，没有外键关系





    }
}
