using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Bestrade.Models
{
    [Table("FBA")]
    public class FBA
    {
        [Key]
        public string sku { get; set; }
        public string mod_num { get; set; }
        public string condition { get; set; }
        public string remark { get; set; }
        public virtual Mod Mod { get; set; }
        public static List<FBA> All()
        {
            BestradeContext btContext = new BestradeContext();
            List<FBA> fba = btContext.FBA.ToList();
            return fba;
        }
        public static FBA Single(string sku)
        {
            BestradeContext btContext = new BestradeContext();
            FBA fba = btContext.FBA.SingleOrDefault(f => f.sku == sku);
            return fba;
        }
    }
}