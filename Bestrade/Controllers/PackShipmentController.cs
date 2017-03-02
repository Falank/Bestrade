using Bestrade.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bestrade.Controllers
{
    public class PackShipmentController : Controller
    {
        //
        // GET: /PackShipment/

        public ActionResult Index()
        {
            BestradeContext btContext = new BestradeContext();
            List<Shipment> shipments = btContext.Shipments.Where(s => s.complete == false).ToList();
            ViewData["shipments"] = shipments;
            return View(PackShipmentView.view());
        }
        public ActionResult Overview()
        {
            return View(PackShipment.All());
        }
        [HttpPost]
        public ActionResult AddPackShipment(string purchase, string sku, string shipment, string qty)
        {
            int check_int;
            if (qty.Length == 0 || !int.TryParse(qty, out check_int) || Convert.ToInt32(qty) <= 0)
            {
                return RedirectToAction("Error", "Shared", new { message = "数量格式不对或者为空或小于等于0" });
            }
            if (purchase.Length == 0 || sku.Length == 0 || shipment.Length == 0)
            {
                return RedirectToAction("Error", "Shared", new { message = "请选择Pack和Shipment" });
            }
            try
            {
                using (var btContext = new BestradeContext())
                {
                    string sql_cmd = String.Format("SELECT p_qty, s_qty FROM PackShipmentView WHERE purchase = '{0}' AND sku = '{1}'", purchase, sku);
                    PackShipmentView view = btContext.Database.SqlQuery<PackShipmentView>(sql_cmd).FirstOrDefault();
                    int qty_left = view.p_qty - view.s_qty - Convert.ToInt32(qty);
                    if (qty_left < 0)
                    {
                        return RedirectToAction("Error", "Shared", new { message = "该Pack剩余数量低于寄出数量，请确认数量是否正确" });
                    }
                    btContext.PackShipment.Add(new PackShipment
                    {
                        purchase = purchase,
                        sku = sku,
                        shipment = shipment,
                        qty = Convert.ToInt32(qty)
                    });
                    btContext.SaveChanges();
                }
            }
            catch(DbUpdateException e)
            {
                return RedirectToAction("Error", "Shared", new { message = "同样的Pack在同一个Shipment下面已经存在，请做调整" });
            }
            catch (FormatException e)
            {
                return RedirectToAction("Error", "Shared", new { message = "数量格式不对" });
            }

            return RedirectToAction("Index", "PackShipment");
        }
        public ActionResult UpdateView(string purchase, string sku, string shipment)
        {
            return View(PackShipment.Single(purchase, sku, shipment));
        }
        [HttpPost]
        public ActionResult UpdatePackShipment(string purchase, string sku, string shipment, string qty)
        {
            int check_int;
            if (qty.Length == 0 || !int.TryParse(qty, out check_int) || Convert.ToInt32(qty) <= 0)
            {
                return RedirectToAction("Error", "Shared", new { message = "数量格式不对或者为空或小于等于0" });
            }
            try
            {
                using (var btContext = new BestradeContext())
                {
                    //Check if the number is right to update
                    string sql_cmd1 = String.Format("SELECT p_qty, s_qty FROM PackShipmentView WHERE purchase = '{0}' AND sku = '{1}'", purchase, sku);
                    string sql_cmd2 = String.Format("SELECT qty, purchase, sku, shipment FROM PackShipment WHERE purchase = '{0}' AND sku = '{1}' AND shipment = '{2}'", purchase, sku, shipment);
                    PackShipmentView view = btContext.Database.SqlQuery<PackShipmentView>(sql_cmd1).FirstOrDefault();
                    int qty_before_update = btContext.Database.SqlQuery<PackShipment>(sql_cmd2).FirstOrDefault().qty;
                    int qty_available = view.p_qty - view.s_qty + qty_before_update;   
                    if(qty_available < Convert.ToInt32(qty))
                    {
                        return RedirectToAction("Error", "Shared", new { message = "该Pack剩余数量低于寄出数量，请确认数量是否正确" });
                    }
                    //
                    var result = btContext.PackShipment.SingleOrDefault(p => p.purchase == purchase && p.sku == sku && p.shipment == shipment);
                    result.purchase = purchase;
                    result.sku = sku;
                    result.shipment = shipment;
                    result.qty = Convert.ToInt32(qty);
                    btContext.SaveChanges();
                }
            }
            catch(FormatException e)
            {
                return RedirectToAction("Error", "Shared", new { message = "数量为空或格式不对" });
            }
            catch (DbUpdateException e)
            {
                return RedirectToAction("Error", "Shared", new { message = "Pack或Shipment不存在" });
            }

            return RedirectToAction("Overview", "PackShipment");
        }
        [HttpPost]
        public ActionResult CompleteShipment(string shipment)
        {
            using (var btContext = new BestradeContext())
            {
                var result = btContext.Shipments.SingleOrDefault(s => s.shipment == shipment);
                result.complete = true;
                btContext.SaveChanges();
            }
            return RedirectToAction("Index", "PackShipment");
        }
        [HttpPost]
        public ActionResult DeletePackShipment(string purchase, string sku, string shipment)
        {
            using (var btContext = new BestradeContext())
            {
                var delete = btContext.PackShipment.SingleOrDefault(p => p.purchase == purchase && p.sku == sku && p.shipment == shipment);
                btContext.PackShipment.Remove(delete);
                btContext.SaveChanges();
            }
            return RedirectToAction("Overview", "PackShipment");
        }
    }
}