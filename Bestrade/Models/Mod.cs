using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Bestrade.Models
{
    [Table("Mods")]
    public class Mod
    {
        [Key]
        public string mod_num { get; set; }
        public string asin { get; set; }
        public string title { get; set; }
        public string remark { get; set; }
        public virtual ICollection<FBA> FBA { get; set; }
        public static List<Mod> All()
        {
            BestradeContext btContext = new BestradeContext();
            List<Mod> mods = btContext.Mods.ToList();
            return mods;
        }
        public static Mod Single(string mod_num)
        {
            BestradeContext btContext = new BestradeContext();
            Mod mod = btContext.Mods.SingleOrDefault(m => m.mod_num == mod_num);
            return mod;
        }
    }
}