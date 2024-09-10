using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSwimmingPool.Models
{
    public class Bookings
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Booking_Id { get; set; }
        public string Cust_Name {  get; set; }
        public int No_Visitors { get; set; }
        public int Pool_id {  get; set; }
        public virtual Pools Pools { get; set; }
        public int Time_id { get; set; }
        public virtual TimeClass TimeClass { get; set; }
        public double Cost {  get; set; }
         
        //2
        public double PullCost()
        {
            AppDbContext db = new AppDbContext();
            var sh = (from a in db.pools
                      where a.Pool_id == Pool_id
                      select a.Cost).FirstOrDefault();
            return sh;
        }
        //3
        public double CalcBasicCost()
        {
            return PullCost() * No_Visitors;
        }
        //4
        public double PullDiscRate()
        {
            AppDbContext db = new AppDbContext();
            var pr = (from b in db.timeClass
                      where b.Time_id == Time_id
                      select b.Day_Time_Disc_Rate).Single();
            return pr;
        }
        public double CalcTotalCost()
        {
            return (CalcBasicCost() -((PullDiscRate()/100)*CalcBasicCost()));
        }
        
    }
}