using Bestrade.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bestrade.Controllers
{
    public class PackController : Controller
    {
        //
        // GET: /Pack/

        public ActionResult Index()
        {
            BestradeContext btContext = new BestradeContext();
            List<Shipment> shipments = btContext.Shipments.Where(s => s.complete == false).ToList();
            ViewData["shipments"] = shipments;
            //SqlConnection con = new SqlConnection(@"Server=.\SQLEXPRESS;Database=Bestrade;Integrated Security=SSPI");
            //SqlCommand cmd = new SqlCommand("select * from Pack_Shipment_View where p_qty > s_qty", con);
            //con.Open();
            //SqlDataReader reader = cmd.ExecuteReader();
            //PackShipmentView view = new PackShipmentView();
            //List<PackShipmentView> list = new List<PackShipmentView>();
            //while (reader.Read())
            //{
            //    view.purchase_id = reader[0].ToString();
            //    view.sku = reader[1].ToString();
            //    view.p_qty = reader[2].ToString();
            //    view.s_qty = reader[3].ToString();
            //    view.pack_remark = reader[4].ToString();
            //    list.Add(view);
            //}
            //con.Close();
            return View(PackShipmentView.view());
            //return View(Pack.All());
        }
        public ActionResult PackFromPurchase(string purchase_id)
        {
            BestradeContext btContext = new BestradeContext();
            List<Pack> pack = btContext.Packs.Where(p => p.purchase_id == purchase_id).ToList();
            ViewData["purchase_id"] = btContext.Purchases.Single(p => p.purchase_id == purchase_id).purchase_id;
            return View(pack);
        }
        //public ActionResult PackFromShipment(string shipment_id)
        //{
        //    BestradeContext btContext = new BestradeContext();
        //    List<Pack> pack = btContext.Packs.Where(p => p.shipment_id == shipment_id).ToList();
        //    ViewData["shipment_id"] = btContext.Shipments.Single(p => p.shipment_id == shipment_id).shipment_id;
        //    return View(pack);
        //}
        public ActionResult UpdateView(string purchase_id, string sku)
        {
            return View(Pack.Single(purchase_id, sku));
        }
        public ActionResult RemarkView(string purchase_id, string sku)
        {
            return View(Pack.Single(purchase_id, sku));
        }
        [HttpPost]
        public ActionResult AddPack(string purchase_id, string sku, string unit_cost, string qty, string remark)
        {
            if(sku.Length == 0 || unit_cost.Length==0|| qty.Length==0)
            {
                return RedirectToAction("Error", "Shared", new { message = "SKU-单价-数量不能为空" });
            }
            try
            {
                using (var btContext = new BestradeContext())
                {
                    btContext.Packs.Add(new Pack
                    {
                        purchase_id = purchase_id,
                        sku = sku,
                        unit_cost = Convert.ToDouble(unit_cost),
                        qty = Convert.ToInt32(qty),
                        pack_remark = remark
                    });
                    btContext.SaveChanges();
                }
            }
            catch(DbUpdateException e)
            {
                return RedirectToAction("Error", "Shared", new { message = "SKU在本单中重复或者不存在" });
            }
            catch(FormatException e)
            {
                return RedirectToAction("Error", "Shared", new { message = "请确认日期-单价-数量是否为正确格式" });
            }
            return RedirectToAction("PackFromPurchase", "Pack", new { purchase_id = purchase_id });
        }
        //[HttpPost]
        //public ActionResult AddPackShipment(string purchase_id, string sku, string shipment_id, string shipment_qty, string shipment_remark)
        //{
        //    using (var btContext = new BestradeContext())
        //    {
        //        var result = btContext.Packs.SingleOrDefault(s => s.purchase_id == purchase_id && s.sku == sku);
        //        result.shipment_id = shipment_id;
        //        result.shipment_qty = Convert.ToInt32(shipment_qty);
        //        result.shipment_remark = shipment_remark;
        //        btContext.SaveChanges();
        //    }
        //    return RedirectToAction("PackFromShipment", "Pack", new { shipment_id = shipment_id });
        //}
        [HttpPost]
        public ActionResult UpdatePack(string purchase_id, string sku, string unit_cost, string qty, string remark)
        {
            using (var btContext = new BestradeContext())
            {
                var result = btContext.Packs.SingleOrDefault(s => s.purchase_id == purchase_id && s.sku == sku);
                result.unit_cost = Convert.ToDouble(unit_cost);
                result.qty = Convert.ToInt32(qty);
                result.pack_remark = remark;
                btContext.SaveChanges();
            }
            return RedirectToAction("PackFromPurchase", "Pack", new { purchase_id = purchase_id });
        }
        [HttpPost]
        public ActionResult CompleteShipment(string shipment_id)
        {
            using (var btContext = new BestradeContext())
            {
                var result = btContext.Shipments.SingleOrDefault(s => s.shipment_id == shipment_id);
                result.complete = true;
                btContext.SaveChanges();
            }
            return RedirectToAction("Index", "Pack");
        }
    }
}
