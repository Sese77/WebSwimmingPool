using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSwimmingPool.Models
{
    public class TimeClass
    {
        [Key]
        public int Time_id { get; set; }
        public string Day_Time {  get; set; }
        public double Day_Time_Disc_Rate {  get; set; }


    }
}