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
    /// 管理员表哥
    /// </summary>
    [Table("Admin")]
    public class Admin
    {

        /// <summary>
        /// 管理员ID
        /// </summary>
        [Key]
        public string AId { get; set; }
        
        /// <summary>
        /// 管理员名字
        /// </summary>
        public string AName { get; set; }

        /// <summary>
        /// 管理员身份证
        /// </summary>
        public string AIdNumber { get; set; }


        
        /// <summary>
        /// 管理员实名
        /// </summary>
        public string AIdName { get; set; } 

       
        public string APassword { get; set; }
        /// <summary>
        /// 管理员地址
        /// </summary>
        public string AAdress { get; set; } 

        

    }
}
