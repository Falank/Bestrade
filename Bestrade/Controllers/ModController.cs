using Bestrade.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bestrade.Controllers
{
    public class ModController : Controller
    {
        //
        // GET: /Mod/

        //public ActionResult Index()
        //{
        //    return View(Mod.All());
        //}
        public ActionResult UpdateView(string mod_num)
        {
            return View(Mod.Single(mod_num));
        }
        public ActionResult RemarkView(string mod_num)
        {
            return View(Mod.Single(mod_num));
        }
        [HttpPost]
        public ActionResult AddMod(string num, string asin, string title, string remark)
        {
            try
            {
                using (var btContext = new BestradeContext())
                {
                    btContext.Mods.Add(new Mod
                    {
                        mod_num = num,
                        asin = asin,
                        title = title,
                        remark = remark
                    });
                    btContext.SaveChanges();
                }
            }
            catch(DbUpdateException e)
            {
                return RedirectToAction("Error", "Shared", new { message = "该型号已存在，请创建新的" });
            }
            return RedirectToAction("Index", "Mod");
        }
        [HttpPost]
        public ActionResult UpdateMod(string mod_num, string asin, string title, string remark)
        {
            using (var btContext = new BestradeContext())
            {
                var result = btContext.Mods.SingleOrDefault(m => m.mod_num == mod_num);
                result.mod_num = mod_num;
                result.asin = asin;
                result.title = title;
                result.remark = remark;
                btContext.SaveChanges();
            }
            return RedirectToAction("Index", "FBA");
        }
        [HttpPost]
        public ActionResult DeleteMod(string mod_num)
        {
            using (var btContext = new BestradeContext())
            {
                var delete = btContext.Mods.SingleOrDefault(m => m.mod_num == mod_num);
                btContext.Mods.Remove(delete);
                try
                {
                    btContext.SaveChanges();
                }
                catch(DbUpdateException e)
                {
                    return RedirectToAction("Error", "Shared", new { message = "该型号下存在一个或多个SKU，请删除SKU后再删除此型号" });
                }
                
            }
            return RedirectToAction("Index", "FBA");
        }
    }
}
