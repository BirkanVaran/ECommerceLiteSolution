﻿using ECommerceLiteBLL.Account;
using ECommerceLiteEntity.IdentityModels;
using ECommerceLiteUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerceLiteBLL.Repository;

namespace ECommerceLiteUI.Controllers
{
    public class PartialsController : BaseController
    {
        // GLOBAL ALAN
        CategoryRepo myCategoryRepo = new CategoryRepo();
        ProductRepo myProductRepo = new ProductRepo();


        public PartialViewResult AdminSideBarResult()
        {
            // TODO: Name Surname alınacak.
            TempData["NameSurname"] = "";
            return PartialView("_PartialAdminSideBar");
        }

        public PartialViewResult AdminSideBarMenuResult()
        {
            return PartialView("_PartialAdminSideBarMenu");
        }

        public PartialViewResult UserNameSurnanmeOnHomePage()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var loggedInUser = MembershipTools.GetUser();
                return PartialView("_PartialUserNameSurnanmeOnHomePage", loggedInUser);
            }
            return PartialView("_PartialUserNameSurnanmeOnHomePage", null);
        }

        public PartialViewResult ShoppingCart()
        {
            var shoppingCart = Session["ShoppingCart"] as List<CartViewModel>;
            if (shoppingCart == null)
            {
                return PartialView("_PartialShoppingCart", new List<CartViewModel>());
            }
            else
            {
                return PartialView("_PartialShoppingCart", shoppingCart);
            }
        }

        public PartialViewResult AdminSideBarCategories()
        {
            TempData["AllCategoriesCount"] = myCategoryRepo.Queryable().Where(x => x.BaseCategory == null).ToList().Count;
            return PartialView("_PartialAdminSideBarCategories");
        }

        public PartialViewResult AdminSideBarProducts()
        {
            TempData["CategoryProductCount"] = myProductRepo.GetAll().Count;
            return PartialView("_PartialAdminSideBarProducts");
        }
    }
}