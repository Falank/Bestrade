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
            return View(PackShipment.All());
        }
        [HttpPost]
        public ActionResult AddPackShipment(string purchase_id, string sku, string shipment_id, string qty)
        {
            using(var btContext = new BestradeContext())
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
            return RedirectToAction("Index", "Pack");
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
            int QTY = Convert.ToInt32(qty);
            using (var btContext = new BestradeContext())
            {
                var result = btContext.PackShipment.SingleOrDefault(p => p.id == ID);
                result.purchase_id = purchase_id;
                result.sku = sku;
                result.shipment_id = shipment_id;
                result.qty = QTY;
                btContext.SaveChanges();
            }
            return RedirectToAction("Index", "PackShipment");
        }
    }
}
