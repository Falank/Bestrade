using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Bestrade.Models
{
    [Table("Packs")]
    public class Pack
    {
        [Column(Order = 0), Key, ForeignKey("Purchase")]
        public string purchase_id { get; set; }
        [Column(Order = 1), Key, ForeignKey("FBA")]
        public string sku { get; set; }
        public double unit_cost { get; set; }
        public int qty { get; set; }
        public string pack_remark { get; set; }
        //public string shipment_id { get; set; }
        //public int shipment_qty { get; set; }
        //public string shipment_remark { get; set; }
        public virtual FBA FBA { get; set; }
        public virtual Purchase Purchase { get; set; }
        public static List<Pack> All()
        {
            BestradeContext btContext = new BestradeContext();
            List<Pack> packs = btContext.Packs.ToList();
            return packs;
        }
        public static Pack Single(string purchase_id, string sku)
        {
            BestradeContext btContext = new BestradeContext();
            Pack pack = btContext.Packs.SingleOrDefault(p => p.sku == sku && p.purchase_id == purchase_id);
            return pack;
        }
    }
}