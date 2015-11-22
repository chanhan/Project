using HouseRent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseRent.Controllers
{
    public class BaseController : Controller
    {
        public HouseRentContext db = new HouseRentContext();

       // public HouseRentContext HouseRentDb { get; protected set; }
    }
}