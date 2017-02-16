using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bestrade.Models
{
    public class PackPurchaseView
    {
        public string purchase_id { get; set; }
        public DateTime purchase_date { get; set; }
        public string supplier_name { get; set; }
        public string sku { get; set; }
        public double unit_cost { get; set; }
        public int qty { get; set; }
        public string pack_remark { get; set; }
        public static List<PackPurchaseView> All()
        {
            BestradeContext btContext = new BestradeContext();
            return btContext.Database.SqlQuery<PackPurchaseView>("SELECT * FROM PackPurchaseView ORDER BY purchase_date DESC").ToList();
        }
    }
}