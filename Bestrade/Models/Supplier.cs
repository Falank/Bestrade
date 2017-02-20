using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bestrade.Models
{
    [Table("Suppliers")]
    public class Supplier
    {
        [Key]
        public string supplier { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}