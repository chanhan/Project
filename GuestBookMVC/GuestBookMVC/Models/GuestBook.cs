using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuestBookMVC.Models
{
    public class GuestBook
    {
        public int ID { get; set; }
        [Required]
        [DisplayName("姓名")]
        public string Name { get; set; }
        [Required]
        [DisplayName("电子邮件地址")]
        public string Email { get; set; }
        [Required]
        [DisplayName("内容")]
        public string Content { get; set; }
    }
}