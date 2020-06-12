using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUDB.Model
{
    [Table("Type")]
    public class Type
    {

        /// <summary>
        /// 灾害类型代号 主键不自增
        /// </summary>
        [Key]
        public int TId { get; set; }

        /// <summary>
        /// 灾害类型名称 非空
        /// </summary>
        
        public string TName { get; set; }

        /// <summary>
        /// 灾害主要特点
        /// </summary>
        public string TCharact { get; set; }

        /// <summary>
        /// 灾害引发主要原因
        /// </summary>
        public string TReason { get; set; }

        /// <summary>
        /// 灾害发生后的损失特点
        /// </summary>
        public string TDamageCharact { get; set; }


        /// <summary>
        /// 2020.5.23 新加 灾害类型等级描述
        /// </summary>
        public string TDamageLevelDec { get; set; }


    }

}
