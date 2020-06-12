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
    /// 4.	地质灾害实况调查单位名称列表
    /// </summary>
    /// 
    [Table("Unit")]
    public class Unit
    {
        /// <summary>
        /// (1)	单位编码(主键；不可空) [类型：字符串]
        /// </summary>
        [Key]     
   
        public string UId { get; set; }


        /// <summary>
        /// (2)	单位名称 [类型：字符串]
        /// </summary>
        public string UName { get; set; }
        /// <summary>
        /// (2)	单位地址 [类型：字符串]；
        /// </summary>

        public string UAddress { get; set; }

        /// <summary>
        /// (4)	单位邮编 [类型：字符串]；
        /// </summary>
        public string UEmail { get; set; }


        /// <summary>
        /// (5)	单位性质 [类型：字符串]；
        /// </summary>
        
        public string UProperty{ get; set; }

    }
}
