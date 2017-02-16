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
        public ActionResult AddPackShipment(string purchase_id, string sku, string shipment_id, string qty)
        {
            if (qty.Length == 0)
            {
                return RedirectToAction("Error", "Shared", new { message = "数量不能为空" });
            }
            if (PackShipment.Single(purchase_id,sku,shipment_id).Count > 0)
            {
                return RedirectToAction("Error", "Shared", new { message = "同样的Pack在同一个Shipment下面已经存在，请做调整" });
            }
            try
            {
                using (var btContext = new BestradeContext())
                {
                    btContext.PackShipment.Add(new PackShipment
                    {
                        purchase_id = purchase_id,
                        sku = sku,
                        shipment_id = shipment_id,
                        qty = Convert.ToInt32(qty)
                    });
                    btContext.SaveChanges();
                }
            }
            catch(DbUpdateException e)
            {
                return RedirectToAction("Error", "Shared", new { message = "请选择Pack和Shipment" });
            }
            catch (FormatException e)
            {
                return RedirectToAction("Error", "Shared", new { message = "数量格式不对" });
            }

            return RedirectToAction("Index", "PackShipment");
        }
        public ActionResult UpdateView(string id)
        {
            int ID = Convert.ToInt32(id);
            return View(PackShipment.Single(ID));
        }
        [HttpPost]
        public ActionResult UpdatePackShipment(string id, string purchase_id, string sku, string shipment_id, string qty)
        {
            int ID = Convert.ToInt32(id);
            try
            {
                using (var btContext = new BestradeContext())
                {
                    var result = btContext.PackShipment.SingleOrDefault(p => p.id == ID);
                    result.purchase_id = purchase_id;
                    result.sku = sku;
                    result.shipment_id = shipment_id;
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
    }
}