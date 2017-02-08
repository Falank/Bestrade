using Bestrade.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;

namespace Bestrade.Controllers
{
    public class PurchaseController : Controller
    {
        //
        // GET: /Purchase/

        public ActionResult Index()
        {
            BestradeContext btContext = new BestradeContext();
            List<Supplier> suppliers = btContext.Suppliers.ToList();
            ViewData["suppliers"] = suppliers;
            return View(Purchase.All());
        }
        public ActionResult PurchaseFromSupplier(string supplier_name)
        {
            BestradeContext btContext = new BestradeContext();
            List<Purchase> purchases = btContext.Purchases.Where(p=>p.supplier_name == supplier_name).ToList();
            ViewData["supplier_name"] = btContext.Suppliers.SingleOrDefault(s => s.supplier_name == supplier_name).supplier_name;
            return View(purchases);
        }
        public ActionResult UpdateView(string purchase_id)
        {
            return View(Purchase.Single(purchase_id));
        }
        [HttpPost]
        public ActionResult AddSupplier(string supplier_name)
        {
            if (supplier_name.Length == 0)
            {
                return RedirectToAction("Error", "Shared", new { message = "供货商名字不能为空" });
            }
            try
            {
                using (var btContext = new BestradeContext())
                {
                    btContext.Suppliers.Add(new Supplier
                    {
                        supplier_name = supplier_name
                    });
                    btContext.SaveChanges();
                }
            }
            catch(DbUpdateException e)
            {
                return RedirectToAction("Error", "Shared", new { message = "供货商已存在，请创建新的名字" });
            }
            return RedirectToAction("Index", "Purchase");
        }
        [HttpPost]
        public ActionResult AddPurchase(string purchase_id, string purchase_date, string supplier_name)
        {
            if (purchase_id.Length == 0 || purchase_date.Length == 0)
            {
                return RedirectToAction("Error", "Shared", new { message = "单号或者日期不能为空" });
            }
            try
            {
                using (var btContext = new BestradeContext())
                {
                    btContext.Purchases.Add(new Purchase
                    {
                        purchase_id = purchase_id,
                        purchase_date = Convert.ToDateTime(purchase_date),
                        supplier_name = supplier_name
                    });
                    btContext.SaveChanges();
                }
            }
            catch(DbUpdateException e)
            {
                return RedirectToAction("Error", "Shared", new { message = "单号已存在，请创建新的" });
            }
            catch(FormatException e)
            {
                return RedirectToAction("Error", "Shared", new { message = "日期格式不对" });
            }
            return RedirectToAction("PurchaseFromSupplier", "Purchase", new { supplier_name = supplier_name });
        }
        [HttpPost]
        public ActionResult UpdatePurchase(string purchase_id, string purchase_date, string supplier_name)
        {
            if (purchase_date.Length == 0)
            {
                return RedirectToAction("Error", "Shared", new { message = "日期不能为空" });
            }
            try
            {
                using (var btContext = new BestradeContext())
                {
                    var result = btContext.Purchases.SingleOrDefault(r => r.purchase_id == purchase_id);
                    result.purchase_date = Convert.ToDateTime(purchase_date);
                    result.supplier_name = supplier_name;
                    btContext.SaveChanges();
                }
            }
            catch (DbUpdateException e)
            {
                return RedirectToAction("Error", "Shared", new { message = "供货商不存在" });
            }
            catch (FormatException e)
            {
                return RedirectToAction("Error", "Shared", new { message = "日期格式不对" });
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult DeletePurchase(string purchase_id)
        {
            using (var btContext = new BestradeContext())
            {
                var delete = btContext.Purchases.SingleOrDefault(p => p.purchase_id == purchase_id);
                btContext.Purchases.Remove(delete);
                btContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
