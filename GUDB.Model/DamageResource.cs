using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUDB.Model
{
    [Table("DamageResource")]
    /// <summary>
    /// 9.	地质灾害图片或视频列
    /// </summary>
    public class DamageResource
    {
        /// <summary>
        /// (1)	地质灾害图片或视频列表自增序号[类型：整数]；
        /// </summary>


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  //设置自增

        public int DRId { get; set; }

        /// <summary>
        /// (2)	对应的地质灾害事件编码(外键；不可空) [类型：字符串]
        /// </summary>
        public string EId { get; set; }

        /// <summary>
        /// (3)	图片或视频类型[类型：单个字符]
        /// </summary>

        public char DRType { get; set; }

        /// <summary>
        /// (4)	图片或视频文件名称(不可空) [类型：字符串]
        /// </summary>
        [Required]
        public string DRName { get; set; }

        /// <summary>
        /// (5)	图片或视频文件存放路径 [类型：字符串]
        /// </summary>
        public string DRSrc { get; set; }

        /// <summary>
        /// (6)	上传图片或视频对应的地质灾害实况(所有其它损失)调查人员-身份证号码(外键) [类型：字符串]
        /// </summary>
        public string IId { get; set; }

        /// <summary>
        /// (7)	确认永久保存图片或视频的Web管理人员编码(外键)
        /// </summary>
        public string AID { get; set; }
        /// <summary>
        /// （ 8）图片或视频内容说明
        /// </summary>
        public string DRDes { get; set; }
        /// <summary>
        /// (9)	其它说明
        /// </summary>
        public string DROther { get; set; }
        
       public virtual Investigator Investigator { get; set; }


        public virtual Event Event { get; set; }



        public  virtual Admin Admin { get; set; }

    }
}
