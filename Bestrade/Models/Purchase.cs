using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Bestrade.Models
{
    [Table("Purchases")]
    public class Purchase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string purchase_id { get; set; }
        [Column(TypeName = "Date")]
        public DateTime purchase_date { get; set; }

        public string supplier_name { get; set; }
        //[ForeignKey("supplier_name")]
        public virtual Supplier Supplier { get; set; }
        //public string supplier_name { get; set; }
        public static List<Purchase> All()
        {
            BestradeContext btContext = new BestradeContext();
            List<Purchase> purchases = btContext.Purchases.ToList();
            return purchases;
        }
        public static Purchase Single(string purchase_id)
        {
            BestradeContext btContext = new BestradeContext();
            Purchase purchase = btContext.Purchases.SingleOrDefault(p=>p.purchase_id == purchase_id);
            return purchase;
        }
    }
}