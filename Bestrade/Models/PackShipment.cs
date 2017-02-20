using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Bestrade.Models
{
    [Table("PackShipment")]
    public class PackShipment
    {
        [Column(Order = 0), Key, ForeignKey("Pack")]
        public string purchase { get; set; }
        [Column(Order = 1), Key, ForeignKey("Pack")]
        public string sku { get; set; }
        [Column(Order = 2), Key, ForeignKey("Shipment")]
        public string shipment { get; set; }
        public int qty { get; set; }
        public virtual Shipment Shipment { get; set; }
        public virtual Pack Pack { get; set; }
        public static List<PackShipment> All()
        {
            BestradeContext btContext = new BestradeContext();
            List<PackShipment> packShipment = btContext.PackShipment.ToList();
            return packShipment;
        }
        public static PackShipment Single(string purchase, string sku, string shipment)
        {
            BestradeContext btContext = new BestradeContext();
            PackShipment packShipment = btContext.PackShipment.SingleOrDefault(p => p.purchase == purchase && p.sku == sku && p.shipment == shipment);
            return packShipment;
        }
    }
}