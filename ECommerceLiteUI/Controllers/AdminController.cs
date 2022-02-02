using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerceLiteBLL.Repository;

namespace ECommerceLiteUI.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : BaseController
    {
        // GLOBAL ALAN
        OrderRepo myOrderRepo = new OrderRepo();


        // GET: Admin
        public ActionResult Dashboard()
        {
            var orderList = myOrderRepo.GetAll();

            // Son 30 günün sipariş sayısı
            var newOrderCount = orderList.Where(x => x.RegisterDate >= DateTime.Now.AddMonths(-1)).ToList().Count;
            ViewBag.NewOrderCount = newOrderCount;
            return View();
        }
    }
}