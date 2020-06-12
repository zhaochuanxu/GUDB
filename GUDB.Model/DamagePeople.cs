using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUDB.Model
{
    /// <summary>
    /// 6.	地质灾害伤亡人员详细名单
    /// </summary>
    [Table("DamagePeople")]
   public class DamagePeople
    {

        /// <summary>
        /// (1)	身份证号码(主键；不可空) [类型：字符串]
        /// </summary>
        
        [Key]
        public string DPId { get; set; }

        /// <summary>
        /// (2)	姓名 [类型：字符串]；
        /// </summary>
        public string DPName { get; set; }
        
        
        /// <summary>
        /// (3)	电话 [类型：字符串]；
        /// </summary>
        public string DPPhone { get; set; }
        
        
        
        /// <summary>
        /// (4)	电邮 [类型：字符串]；
        /// </summary>
        public string DPEmail { get; set; }
        
        
        
        /// <summary>
        /// (5)	其它联系方式 [类型：字符串]
        /// </summary>
        public string DPOtherConnect { get; set; }
        
        
        
        /// <summary>
        /// (6)	所受伤害对应的地质灾害事件编码(外键；不可空) [类型：字符串]；
        /// </summary>
        public string EId { get; set; }
        
        
        
        
        /// <summary>
        /// (7)	所受伤害程度 [类型：单个字符]；
        /// </summary>
        public char DPLevel { get; set; }
        
        
        
        
        /// <summary>
        /// (8)	关联联系人姓名[类型：字符串]
        /// </summary>
        public string DPReferPeopleName { get; set; }
        
        
        
        
        /// <summary>
        /// （9)    关联联系人电话[类型：字符串]
        /// </summary>
        public string DPReferPeoplePhone { get; set; }
        
        
        
        
        /// <summary>
        /// (10)	地质灾害实况(伤亡人员)调查人员-身份证号码(外键) [类型：字符串]；
        /// </summary>
        public string IId { get; set; }


        /// <summary>
        /// 关联外键表 Investigator   调查员表
        /// </summary>
        public virtual Investigator Investigator { get; set; }//如果没有声明TUsers对象，则UserID是一个普通的字段，没有外键关系
        /// <summary>
        /// 关联外键表 Event   事件员表
        /// </summary>
        public virtual  Event Event{ get; set; }//如果没有声明TUsers对象，则UserID是一个普通的字段，没有外键关系



    }
}
