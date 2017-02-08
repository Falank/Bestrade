using Bestrade.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bestrade.Controllers
{
    public class FBAController : Controller
    {
        //
        // GET: /FBA/

        public ActionResult Index()
        {
            return View(FBA.All());
        }
        public ActionResult FbaFromMod(string mod_num)
        {
            BestradeContext btContext = new BestradeContext();
            List<FBA> fba = btContext.FBA.Where(f => f.mod_num == mod_num).ToList();
            ViewData["mod_num"] = btContext.Mods.Single(m => m.mod_num == mod_num).mod_num;
            return View(fba);
        }
        public ActionResult UpdateView(string sku)
        {
            return View(FBA.Single(sku));
        }
        public ActionResult RemarkView(string sku)
        {
            return View(FBA.Single(sku));
        }
        [HttpPost]
        public ActionResult AddFBA(string sku, string condition, string mod_num, string remark)
        {
            if (sku.Length == 0)
            {
                return RedirectToAction("Error", "Shared", new { message = "FBA不能为空" });
            }
            try
            {
                using (var btContext = new BestradeContext())
                {
                    btContext.FBA.Add(new FBA
                    {
                        sku = sku,
                        condition = condition,
                        mod_num = mod_num,
                        remark = remark
                    });
                    btContext.SaveChanges();
                }
            }
            catch (DbUpdateException e)
            {
                return RedirectToAction("Error", "Shared", new { message = "FBA已存在，请创建新的" });
            }
            return RedirectToAction("FbaFromMod", "FBA", new { mod_num = mod_num });
        }
        public ActionResult UpdateFBA(string sku)
        {
            try
            {
                using (var btContext = new BestradeContext())
                {
                    var result = btContext.FBA.SingleOrDefault(r => r.sku == sku);
                    result.condition = Request.Form["condition"];
                    result.mod_num = Request.Form["mod_num"];
                    result.remark = Request.Form["remark"];
                    btContext.SaveChanges();
                }
            }
            catch(DbUpdateException e)
            {
                return RedirectToAction("Error", "Shared", new { message = "型号不存在，请创建型号后添加FBA" });
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult DeleteFBA(string sku)
        {
            using (var btContext = new BestradeContext())
            {
                var delete = btContext.FBA.SingleOrDefault(m => m.sku == sku);
                btContext.FBA.Remove(delete);
                btContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
