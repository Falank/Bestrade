using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Bestrade.Models
{
    [Table("Shipments")]
    public class Shipment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string shipment_id { get; set; }
        [Column(TypeName="Date")]
        public DateTime shipping_date { get; set; }
        public string shipping_company { get; set; }
        public double shippment_cost { get; set; }
        public virtual Company Company { get; set; }

        public static List<Shipment> All()
        {
            BestradeContext btContext = new BestradeContext();
            List<Shipment> shipments = btContext.Shipments.ToList();
            return shipments;
        }
    }
}