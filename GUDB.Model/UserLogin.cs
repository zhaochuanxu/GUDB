using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUDB.Model
{

    //对应User表示一对一关系
    [Table("UserLogin")]

    /// <summary>
    /// 用户列表登录历史记录表 
    /// </summary>
    public class UserLogin
    {


        /// <summary>
        /// 没访问一次自增一次
        /// </summary>
        [System.ComponentModel.DataAnnotations.Key]  //主键

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  //设置自增
        public int ULId { get; set; }
        
        /// <summary>
        /// 对应UID访问历史记录
        /// </summary>
        public int UId { get; set; }



        /// <summary>
        /// 访问历史时间
        /// </summary>
        public DateTime ULTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //访问历史IP
        public string ULIP { get; set; }

        
       
        /// <summary>
        /// 访问历史地址
        /// </summary>
        public string ULAdress { get; set; }



        
        
        public virtual User User { get; set; }

    }
}
