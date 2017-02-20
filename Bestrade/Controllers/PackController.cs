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
            return View(PackPurchaseView.All());
        }
        public ActionResult PackFromPurchase(string purchase)
        {
            BestradeContext btContext = new BestradeContext();
            List<Pack> pack = btContext.Packs.Where(p => p.purchase == purchase).ToList();
            ViewData["purchase"] = btContext.Purchases.Single(p => p.purchase == purchase).purchase;
            return View(pack);
        }
        public ActionResult UpdateView(string purchase, string sku, string return_view)
        {
            ViewData["return_view"] = return_view;
            return View(Pack.Single(purchase, sku));
        }
        public ActionResult RemarkView(string purchase, string sku)
        {
            return View(Pack.Single(purchase, sku));
        }
        [HttpPost]
        public ActionResult AddPack(string purchase, string sku, string unit_cost, string qty, string remark)
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
                        purchase = purchase,
                        sku = sku,
                        unit_cost = Convert.ToDouble(unit_cost),
                        qty = Convert.ToInt32(qty),
                        remark = remark
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
                return RedirectToAction("Error", "Shared", new { message = "请确认单价-数量是否为正确格式" });
            }
            return RedirectToAction("PackFromPurchase", "Pack", new { purchase = purchase });
        }
        [HttpPost]
        public ActionResult UpdatePack(string purchase, string sku, string unit_cost, string qty, string remark, string return_view)
        {
            try
            {
                using (var btContext = new BestradeContext())
                {
                    var result = btContext.Packs.SingleOrDefault(s => s.purchase == purchase && s.sku == sku);
                    result.unit_cost = Convert.ToDouble(unit_cost);
                    result.qty = Convert.ToInt32(qty);
                    result.remark = remark;
                    btContext.SaveChanges();
                }
            }
            catch (FormatException e)
            {
                return RedirectToAction("Error", "Shared", new { message = "请确认单价-数量是否为正确格式" });
            }
            if(return_view.Length == 0)
            {
                return RedirectToAction("Index", "Pack");
            }
            return RedirectToAction("PackFromPurchase", "Pack", new { purchase = purchase });
        }
        [HttpPost]
        public ActionResult DeletePack(string purchase, string sku, string return_url)
        {
            using (var btContext = new BestradeContext())
            {
                var delete = btContext.Packs.SingleOrDefault(p => p.purchase == purchase && p.sku == sku);
                btContext.Packs.Remove(delete);
                btContext.SaveChanges();
            }
            return Redirect(return_url);
            //return RedirectToAction("Index", "Pack");
        }
    }
}