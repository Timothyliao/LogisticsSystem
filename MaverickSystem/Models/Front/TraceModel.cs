using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MaverickSystem.Models.Front
{
    public class TraceModel
    {
        [Display(Name = "订单条码")]
        public string Code { get; set; }
    }
}