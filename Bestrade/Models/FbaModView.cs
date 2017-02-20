using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bestrade.Models
{
    public class FbaModView
    {
        public string sku { get; set; }
        public string condition { get; set; }
        public string f_remark { get; set; }
        public string mod_num { get; set; }
        public string asin { get; set; }
        public string title { get; set; }
        public string m_remark { get; set; }
        public static List<FbaModView> All()
        {
            BestradeContext btContext = new BestradeContext();
            return btContext.Database.SqlQuery<FbaModView>("SELECT * FROM FbaModView").ToList();
        }
    }
}