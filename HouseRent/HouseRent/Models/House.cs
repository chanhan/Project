using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HouseRent.Models
{
    public class House
    {
        [Key]
        public int HouseId{set;get;}
        [DisplayName("标题")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Title{set;get;}

        [DisplayName("面积")]
        [Required(ErrorMessage = "{0}不能为空")]
        public decimal Floorage{set;get;}
        [DisplayName("价格")]
        [Required(ErrorMessage = "{0}不能为空")]
        public decimal Price{set;get;}
     
        [DisplayName("联系方式")]
        [Required(ErrorMessage = "{0}不能为空")]
        [RegularExpression(@"^\d{6,20}$", ErrorMessage = "{0}格式错误")]
        public string Contract{set;get;}

        [DisplayName("详细信息")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Description{set;get;}

        [DisplayName("发布时间")]
        public DateTime PublishTime{get;set;} 
        
        //public int TypeId{set;get;} 
        //public int StreetId{set;get;}
        //public int PublishUserId { get; set; }

        public virtual HouseType HouseType { get; set; }
        public virtual Street Street { get; set; }
        public virtual User User { get; set; }
    }
}