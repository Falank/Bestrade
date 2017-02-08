using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Bestrade.Models
{
    [Table("Company")]
    public class Company
    {
        [Key]
        public string shipping_company { get; set; }
        public ICollection<Shipment> Shipment { get; set; }
    }
}