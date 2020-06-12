using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUDB.Model
{


    [Table("User")]
    public class User
   
    
    {
       

            [System.ComponentModel.DataAnnotations.Key]  //主键

            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  //设置自增
            public int UId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            /// 


            public string UName { get; set; }

        /// <summary>
        /// 用户登录密码
        /// </summary>
        public string UPassword { get; set; }
        /// <summary>
        /// 
        /// </summary
        public string UIdName { get; set; }

            /// <summary>
            /// 电话号码
            /// </summary>
            public string UPhone { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string UMail { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string UOtherConnect { get; set; }
        public string UIdNumber { get; set; }

    

          

        }
    }
