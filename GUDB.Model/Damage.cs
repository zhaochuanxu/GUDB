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
    /// 3.	地质灾害损失概况表
    /// </summary>
    [Table("Damage")]
    public class Damage
    {


        /// <summary>
        /// 地质灾害损失概况iD
        /// </summary>
        /// 

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  //设置自增
        public string DId { get; set; }

        /// <summary>
        /// (1)	地质灾害事件编码(外键；不可空) [类型：字符串]
        /// </summary>
        /// 

        public string EId{ get; set; }

        /// <summary>
        /// (2)	记录损失日期[类型：日期，精确到天]；
        /// </summary>
        public DateTime DTime { get; set; }


        /// <summary>
        /// (3)	直接致死人数[类型：整数]；
        /// </summary>
        public int DDirDead{ get; set; }

        /// <summary>
        /// (4)	受伤人数 [类型：整数]
        /// </summary>
        public int DInjured { get; set; }

        /// <summary>
        /// (5)	间接致死(受伤后未能成功抢救而死亡)人数 [类型：整数]
        /// </summary>
        public int DInDirDead{ get; set ; }


        /// <summary>
        /// (6)	失踪人数 [类型：整数]；
        /// </summary>
        public int DMissed { get; set; }


        /// <summary>
        /// (7)	倒塌建筑-居民楼[类型：整数;单位：栋];
        /// </summary>
        public int DCollapsedLiving{ get; set; }
        /// <summary>
        /// (8)	倒塌建筑-办公楼[类型：整数;单位：平方米];
        /// </summary>

        public int DCollapsedWoking{ get; set; }

        /// <summary>
        /// (9)	倒塌建筑-工业用房[类型：整数;单位：平方米];
        /// </summary>
        public int DCollapsedFactoy{ get; set; }



        /// <summary>
        ///（10） 受损建筑-居民楼[类型：整数;单位：栋];
        /// </summary>

        public int DImpairedLiving{ get; set; }
        /// <summary>
        /// (11)受损建筑-办公楼[类型：整数;单位：平方米];
        /// </summary>
        public int DImpairedWoring{ get; set; }

        /// <summary>
        /// 受损建筑-工业用房[类型：整数;单位：平方米];
        /// </summary>
        public int DImpairedFactory { get; set; }

        /// <summary>
        /// (13)	除人建筑外的所有其它财产损失[类型：实数;单位：人民币元];
        /// </summary>
        public float DOtherDamageMoney { get; set; }


        /// <summary>
        /// (14)	 除人建筑外的所有其它财产损失描述[类型：字符串]
        /// </summary>
        public string DOtherDamageDes{ get; set; }
        /// <summary>
        /// (15)	 其它说明[类型：字符串]
        /// </summary>

        public string DOtherIllstrate { get; set; }



        public virtual Event Event { get; set; }
        /// <summary>
        /// 外键 连接Event
        /// </summary>
        public virtual Type Type { get; set; }//如果没有声明TUsers对象，则UserID是一个普通的字段，没有外键关系




    }
}
