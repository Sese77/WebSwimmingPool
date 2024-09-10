using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSwimmingPool.Models
{
    public class Pools
    {
        [Key]
        public int Pool_id {  get; set; }
        public string Pool_Name {  get; set; }
        public string Location { get; set; }
        public double Cost {  get; set; }

    }
}