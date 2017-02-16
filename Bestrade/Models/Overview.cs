using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bestrade.Models
{
    public class Overview
    {
        public string purchase_id { get; set; }
        public string sku { get; set; }
        public DateTime purchase_date { get; set; }
        public string supplier_name { get; set; }
        public double unit_cost { get; set; }
        public int pq { get; set; }
        public string pack_remark { get; set; }
        public string shipment_id { get; set; }
        public DateTime shipping_date { get; set; }
        public string shipping_company { get; set; }
        public int sq { get; set; }
        public string shipment_remark { get; set; }
        public bool complete { get; set; }
        public static List<Overview> All()
        {
            BestradeContext btContext = new BestradeContext();
            return btContext.Database.SqlQuery<Overview>("SELECT * FROM Overview").ToList();
        }
    }
}