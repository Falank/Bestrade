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
        public ActionResult PurchaseFromSupplier(string supplier)
        {
            BestradeContext btContext = new BestradeContext();
            List<Purchase> purchases = btContext.Purchases.Where(p=>p.supplier == supplier).ToList();
            ViewData["supplier"] = btContext.Suppliers.SingleOrDefault(s => s.supplier == supplier).supplier;
            return View(purchases);
        }
        public ActionResult UpdateView(string purchase, string return_view)
        {
            ViewData["return_view"] = return_view;
            return View(Purchase.Single(purchase));
        }
        [HttpPost]
        public ActionResult AddSupplier(string supplier)
        {
            if (supplier.Length == 0)
            {
                return RedirectToAction("Error", "Shared", new { message = "供货商名字不能为空" });
            }
            try
            {
                using (var btContext = new BestradeContext())
                {
                    btContext.Suppliers.Add(new Supplier
                    {
                        supplier = supplier
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
        public ActionResult AddPurchase(string purchase, string date, string supplier)
        {
            if (purchase.Length == 0 || date.Length == 0)
            {
                return RedirectToAction("Error", "Shared", new { message = "单号或者日期不能为空" });
            }
            try
            {
                using (var btContext = new BestradeContext())
                {
                    btContext.Purchases.Add(new Purchase
                    {
                        purchase = purchase,
                        date = Convert.ToDateTime(date),
                        supplier = supplier
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
            return RedirectToAction("PurchaseFromSupplier", "Purchase", new { supplier = supplier });
        }
        [HttpPost]
        public ActionResult UpdatePurchase(string purchase, string date, string supplier, string return_view)
        {
            if (date.Length == 0)
            {
                return RedirectToAction("Error", "Shared", new { message = "日期不能为空" });
            }
            try
            {
                using (var btContext = new BestradeContext())
                {
                    var result = btContext.Purchases.SingleOrDefault(r => r.purchase == purchase);
                    result.date = Convert.ToDateTime(date);
                    result.supplier = supplier;
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
            if(return_view.Length == 0)
            {
                return RedirectToAction("Index", "Purchase");
            }
            return RedirectToAction("PurchaseFromSupplier", "Purchase", new { supplier = supplier });
        }
        [HttpPost]
        public ActionResult DeletePurchase(string purchase, string return_url)
        {
            using (var btContext = new BestradeContext())
            {
                var delete = btContext.Purchases.SingleOrDefault(p => p.purchase == purchase);
                btContext.Purchases.Remove(delete);
                btContext.SaveChanges();
            }
            return Redirect(return_url);
        }
    }
}