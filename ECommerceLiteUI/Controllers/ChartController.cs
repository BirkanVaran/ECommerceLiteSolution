using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerceLiteBLL.Repository;


namespace ECommerceLiteUI.Controllers
{
    public class ChartController : Controller
    {
        // GLOBAL ALAN
        CategoryRepo myCategoryRepo = new CategoryRepo();

        // GET: Chart

        public ActionResult VisualizePieChartResult()
        {
            // PieChart'ta göstermek istediğimiz datayı alacağız.
            // Bu datayı Dasboard'daki Ajax işlemine gönderebilmek için,
            // Return Json ile işlem yapacağız.

            var data = myCategoryRepo.GetBaseCategoriesProductCount();

            return Json(data,JsonRequestBehavior.AllowGet);
        }
        
    }
}