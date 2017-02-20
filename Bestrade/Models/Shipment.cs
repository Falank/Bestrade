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
        public string shipment { get; set; }
        [Column(TypeName="Date")]
        public DateTime date { get; set; }
        public string company { get; set; }
        public double cost { get; set; }
        public string remark { get; set; }
        public bool complete { get; set; }
        public virtual Company Company { get; set; }
        public static List<Shipment> All()
        {
            BestradeContext btContext = new BestradeContext();
            List<Shipment> shipments = btContext.Shipments.ToList();
            return shipments;
        }
        public static Shipment Single(string Shipment)
        {
            BestradeContext btContext = new BestradeContext();
            Shipment shipment = btContext.Shipments.SingleOrDefault(p => p.shipment == Shipment);
            return shipment;
        }
    }
}