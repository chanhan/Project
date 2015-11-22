using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HouseRent.Models
{
    public class HouseType
    {
        [DisplayName("户型")]
        [Required(ErrorMessage = "请选择{0}")]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<House> House { get; set; }
    }
}