using Bestrade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bestrade.Controllers
{
    public class OverviewController : Controller
    {
        //
        // GET: /Overview/

        public ActionResult Index()
        {
            return View(Overview.view());
        }

    }
}
