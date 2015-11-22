using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HouseRent.Models
{
    public class User
    {
        [Key]
        public int LoginId{get; set;}

        [DisplayName("用户名")]
        [Required(ErrorMessage = "{0}不能为空")]
        [MaxLength(10,ErrorMessage="用户名不能超过{1}个字符")]
        public string LoginName{get;set;}

        [DisplayName("用户姓名")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string UserName{get; set;}

        [DisplayName("密码")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Password{get;   set;}

        [NotMapped]
        [DisplayName("确认密码")]
        [Required(ErrorMessage = "{0}不能为空")]
        [Compare("Password", ErrorMessage = "两次密码输入不一致")]
        public string RePassword { get; set; }

        [DisplayName("电话")]
        [Required(ErrorMessage = "{0}不能为空")]
        [RegularExpression(@"^\d{6,20}$", ErrorMessage = "{0}格式错误")]
        public string Telephone{get;  set;}

        public DateTime CreateOn { get; set; }

        public ICollection<House> House { get; set; }
    }
}