using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bestrade.Models
{
    public class PackShipmentView
    {
        public string purchase_id { get; set; }
        public string sku { get; set; }
        public int p_qty { get; set; }
        public int s_qty { get; set; }
        public string pack_remark { get; set; }
        public static List<PackShipmentView> view()
        {
            BestradeContext btContext = new BestradeContext();
            return btContext.Database.SqlQuery<PackShipmentView>("SELECT * FROM PackShipmentView WHERE p_qty > s_qty").ToList();
        }
    }
}