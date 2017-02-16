using Bestrade.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bestrade.Controllers
{
    public class ShipmentController : Controller
    {
        //
        // GET: /Shipment/

        public ActionResult Index()
        {
            BestradeContext btContext = new BestradeContext();
            List<Company> companies = btContext.Companies.ToList();
            ViewData["companies"] = companies;
            return View(Shipment.All());
        }
        public ActionResult UpdateView(string shipment_id)
        {
            BestradeContext btContext = new BestradeContext();
            Shipment shipments = btContext.Shipments.SingleOrDefault(s => s.shipment_id == shipment_id);
            return View(shipments);
        }
        public ActionResult ShipmentFromCompany(string shipping_company)
        {
            BestradeContext btContext = new BestradeContext();
            List<Shipment> shipments = btContext.Shipments.Where(s => s.shipping_company == shipping_company).ToList();
            ViewData["company"] = btContext.Companies.SingleOrDefault(c => c.shipping_company == shipping_company).shipping_company;
            return View(shipments);
        }
        [HttpPost]
        public ActionResult AddShipment(string id, string date, string company, string cost, string remark)
        {
            if (id.Length == 0 || date.Length == 0)
            {
                return RedirectToAction("Error", "Shared", new { message = "运单号或者日期不能为空" });
            }
            try
            {
                using (var btContext = new BestradeContext())
                {
                    btContext.Shipments.Add(new Shipment
                    {
                        shipment_id = id,
                        shipping_date = Convert.ToDateTime(date),
                        shipping_company = company,
                        shipment_cost = Convert.ToDouble(cost),
                        shipment_remark = remark
                    });
                    btContext.SaveChanges();
                }
            }
            catch (DbUpdateException e)
            {
                return RedirectToAction("Error", "Shared", new { message = "运单号已存在，请创建新的" });
            }
            catch (FormatException e)
            {
                return RedirectToAction("Error", "Shared", new { message = "日期格式不对" });
            }
            return RedirectToAction("ShipmentFromCompany", "Shipment", new { @shipping_company = company });
        }
        [HttpPost]
        public ActionResult UpdateShipment(string id, string date, string company, string cost, string remark, bool complete)
        {
            if(date.Length == 0)
            {
                return RedirectToAction("Error", "Shared", new { message = "日期不能为空" });
            }
            try
            {
                using (var btContext = new BestradeContext())
                {
                    var result = btContext.Shipments.SingleOrDefault(s => s.shipment_id == id);
                    result.shipping_date = Convert.ToDateTime(date);
                    result.shipping_company = company;
                    result.shipment_cost = Convert.ToDouble(cost);
                    result.complete = complete;
                    result.shipment_remark = remark;
                    btContext.SaveChanges();
                }
            }
            catch (DbUpdateException e)
            {
                return RedirectToAction("Error", "Shared", new { message = "物流公司不存在" });
            }
            catch (FormatException e)
            {
                return RedirectToAction("Error", "Shared", new { message = "请检查日期或数量是否为正确格式" });
            }
            return RedirectToAction("Index", "Shipment");
        }
        [HttpPost]
        public ActionResult AddCompany(string shipping_company)
        {
            if (shipping_company.Length == 0)
            {
                return RedirectToAction("Error", "Shared", new { message = "物流公司名字不能为空" });
            }
            try
            {
                using (var btContext = new BestradeContext())
                {
                    btContext.Companies.Add(new Company
                    {
                        shipping_company = shipping_company
                    });
                    btContext.SaveChanges();
                }
            }
            catch(DbUpdateException e)
            {
                return RedirectToAction("Error", "Shared", new { message = "物流公司已存在，请创建新的" });
            }
            return RedirectToAction("Index", "Shipment");
        }
        [HttpPost]
        public ActionResult DeleteShipment(string shipment_id)
        {
            using (var btContext = new BestradeContext())
            {
                var delete = btContext.Shipments.SingleOrDefault(p => p.shipment_id == shipment_id);
                btContext.Shipments.Remove(delete);
                btContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult RemarkView(string shipment_id)
        {
            return View(Shipment.Single(shipment_id));
        }
    }
}
