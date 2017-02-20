using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bestrade.Models
{
    public class Overview
    {
        public string purchase { get; set; }
        public string sku { get; set; }
        public DateTime p_date { get; set; }
        public string supplier { get; set; }
        public double unit_cost { get; set; }
        public int p_qty { get; set; }
        public string p_remark { get; set; }
        public string shipment { get; set; }
        public DateTime s_date { get; set; }
        public string company { get; set; }
        public int s_qty { get; set; }
        public string s_remark { get; set; }
        public bool complete { get; set; }
        public static List<Overview> All()
        {
            BestradeContext btContext = new BestradeContext();
            return btContext.Database.SqlQuery<Overview>("SELECT * FROM Overview").ToList();
        }
    }
}