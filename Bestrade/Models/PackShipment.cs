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
        [Key]
        public int id { get; set; }
        [Column(Order = 0), ForeignKey("Pack")]
        public string purchase_id { get; set; }
        [Column(Order = 1), ForeignKey("Pack")]
        public string sku { get; set; }
        [ForeignKey("Shipment")]
        public string shipment_id { get; set; }
        public int qty { get; set; }
        public virtual Shipment Shipment { get; set; }
        public virtual Pack Pack { get; set; }
    }
}