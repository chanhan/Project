using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuestBookMVC.Models
{
    public class User
    {
        public int ID{get;set;}
        [Required]
        [DisplayName("用户名")]
        [StringLength(20,ErrorMessage="{0}不超过{1}个字符")]
        public string UserName{get;set;}
        [Required]
        [DisplayName("密码")]
        [StringLength(30,ErrorMessage="{0}不超过{1}个字符")]
        public string Password{get;set;}

        [Required]
        [DisplayName("电子邮件地址")]
        public string Email{get;set;}
        [Required]
        [DisplayName("电话")]
        public string Telephone{get;set;}
        [DisplayName("创建日期")]
        public DateTime CreateTime{get;set;}

        //[Required]
        //[DisplayName("图标")]
        //[Range(1,3,ErrorMessage="输入值必须介于{0}与{1}之间")]
        //public int Icon { get; set; }
       
    }
}