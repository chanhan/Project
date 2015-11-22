using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HouseRent.Models
{
    public class District
    {
        [DisplayName("区域")]
        [Required(ErrorMessage = "请选择{0}")]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Street> Street { get; set; }
       
    }
}