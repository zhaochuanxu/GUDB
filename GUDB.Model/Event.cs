using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// 地质灾害事件列表
/// </summary>
namespace GUDB.Model
{
    [Table("Event")]
    public class Event
    {
        /// <summary>
        /// 地址灾害事件编码
        /// </summary>
        [Key]  //主键
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]  //设置自增
        public string EId { get; set; }

        /// <summary>
        /// 灾害类型代号 外键对应type
        /// </summary>
        public int TId { get; set; }

        /// <summary>
        /// 事件发生时刻 精确到毫秒
        /// </summary>
        public DateTime ETime { get; set; }

        /// <summary>
        /// 事件发生位置 -精度
        /// </summary>
        public double ELong { get; set; }
        /// <summary>
        /// 事件发生位置-纬度
        /// </summary>
        public double ELat { get; set; }

        /// <summary>
        /// 事件发生位置地理明
        /// </summary>
        public string ELocation { get; set; }

        /// <summary>
        ///时间强烈程度等级
        /// </summary>
        public double Elevel { get; set; }


        /// <summary>
        ///时间所造成的影响范围
        /// </summary>
        public string ERange { get; set; }

        /// <summary>
        /// 事件所造成的损失概况
        /// </summary>
        public string EDamageDes { get; set; }

        /// <summary>
        ///事件发生地点的地质构造特点说明 
        /// </summary>
        public string EEarthDes { get; set; }
        /// <summary>
        /// 关联外键
        /// </summary>
        public virtual Type Type { get; set; }//如果没有声明TUsers对象，则UserID是一个普通的字段，没有外键关系
    }
}
