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
        public string purchase { get; set; }
        [Column(TypeName = "Date")]
        public DateTime date { get; set; }
        public string supplier { get; set; }
        public virtual Supplier Supplier { get; set; }
        public static List<Purchase> All()
        {
            BestradeContext btContext = new BestradeContext();
            List<Purchase> purchases = btContext.Purchases.ToList();
            return purchases;
        }
        public static Purchase Single(string Purchase)
        {
            BestradeContext btContext = new BestradeContext();
            Purchase purchase = btContext.Purchases.SingleOrDefault(p => p.purchase == Purchase);
            return purchase;
        }
    }
}