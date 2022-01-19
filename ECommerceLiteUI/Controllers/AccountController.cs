using ECommerceLiteBLL.Account;
using ECommerceLiteBLL.Repository;
using ECommerceLiteBLL.Settings;
using ECommerceLiteEntity.Enums;
using ECommerceLiteEntity.IdentityModels;
using ECommerceLiteEntity.Models;
using ECommerceLiteEntity.ViewModels;
using ECommerceLiteUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ECommerceLiteUI.Controllers
{
    public class AccountController : BaseController
    {
        // GLOBAL ALAN
        CustomerRepo myCustomerRepo = new CustomerRepo();
        PassiveUserRepo myPassiveUserRepo = new PassiveUserRepo();
        UserManager<ApplicationUser> myUserManager = MembershipTools.NewUserManager();
        UserStore<ApplicationUser> myUserStore = MembershipTools.NewUserStore();
        RoleManager<ApplicationRole> myRoleManager = MembershipTools.NewRoleManager();


        #region Register - HttpGet | HttpPost
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var myUserManager = MembershipTools.NewUserManager();
                var myUserStore = MembershipTools.NewUserStore();

                var checkUserTC = myUserStore.Context.Set<Customer>().FirstOrDefault(x => x.TCNumber == model.TCNumber)?.TCNumber;
                if (checkUserTC != null)
                {
                    ModelState.AddModelError("", "Bu TC Kimlik Numarası sistemde zaten kayıtlı.");
                    return View(model);
                }

                var checkUserEmail = myUserStore.Context.Set<ApplicationUser>().FirstOrDefault(x => x.Email == model.Email)?.Email;
                if (checkUserEmail != null)
                {
                    ModelState.AddModelError("", "Bu e-mail adresi sistemde zaten kayıtlı. Parolanızını unuttuysanız, \"Parolamı Unuttum\" ile yeni bir parola oluşturabilirsiniz.");
                    return View(model);
                }

                var theActivationCode = Guid.NewGuid().ToString().Replace("-", "");

                var newUser = new ApplicationUser()
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    ActivationCode = theActivationCode
                };

                var theResult = myUserManager.CreateAsync(newUser, model.Password);

                if (theResult.Result.Succeeded)
                {
                    // ASPNETUsers tablosuna kayıt gerçekleşirse yeni kayıt olmuş bu kişiyi pasif tablosuna ekleyeceğiz.

                    // Kişi kendisine gelen aktifleştirme işlemini yaparsa, PasifKullanıcılar tablasondan bu kullanıcıyı silip olması gerkeen roldeki tabloya ekleyeceğiz.

                    await myUserManager.AddToRoleAsync(newUser.Id, TheIdentityRoles.Passive.ToString());

                    PassiveUser newPassiveUser = new PassiveUser()
                    {
                        TCNumber = model.TCNumber,
                        UserId = newUser.Id,
                        TargetRole = TheIdentityRoles.Customer
                    };

                    myPassiveUserRepo.Insert(newPassiveUser);
                    string siteUrl = Request.Url.Scheme + Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port);
                    await SiteSettings.SendMail(new MailModel()
                    {
                        To = newUser.Email,
                        Subject = "ECommerceLite Site Aktivasyon",
                        Message = $"Merhaba {newUser.Name} {newUser.Surname}, <br/>Hesabınızı aktifleştirmek için <b><a href='{siteUrl}/Account/Activation?code={theActivationCode}'> Aktivasyon Linki</a></b>'ne tıklayınız."
                    });

                    return RedirectToAction("Login", "Account", new { email = $"{newUser.Email}" });
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı kayıt işleminde hata oluştu!");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                // TODO: ex Loglama
                ModelState.AddModelError("", "Beklenmedik bir hata oluştu!");
                return View(model);
            }
        }
        #endregion

        #region Activation - HttpGet | HttpPost
        [HttpGet]
        public async Task<ActionResult> Activation(string code)
        {
            try
            {
                var theUser = myUserStore.Context.Set<ApplicationUser>().FirstOrDefault(x => x.ActivationCode == code);
                if (theUser == null)
                {
                    ViewBag.ActivationResult = "Aktivasyon işlemi  başarısız.";
                    return View();
                }

                if (theUser.EmailConfirmed)
                {
                    ViewBag.ActivationResult = "E-Posta adresiniz zaten onaylı.";
                    return View();
                }
                theUser.EmailConfirmed = true;
                await myUserStore.UpdateAsync(theUser);
                await myUserStore.Context.SaveChangesAsync();

                // Kullanıcıyı PassiveUser tablosundan bulalım.
                PassiveUser thePassiveUser = myPassiveUserRepo.Queryable().FirstOrDefault(x => x.UserId == theUser.Id);
                if (thePassiveUser != null)
                {
                    if (thePassiveUser.TargetRole == TheIdentityRoles.Customer)
                    {
                        // Yeni Customer oluşturulacak ve kaydedilecek.
                        Customer newCustomer = new Customer()
                        {
                            TCNumber = thePassiveUser.TCNumber,
                            UserId = thePassiveUser.UserId,
                        };
                        myCustomerRepo.Insert(newCustomer);
                        // Passive tablosundan bu kayıt silinsin.
                        myPassiveUserRepo.Delete(thePassiveUser);
                        // User'daki Passive role silinip Customer role eklenecek.
                        myUserManager.RemoveFromRole(theUser.Id, TheIdentityRoles.Passive.ToString());
                        myUserManager.AddToRole(theUser.Id, TheIdentityRoles.Customer.ToString());
                        ViewBag.ActivationResult = $"Merhaba {theUser.Name} {theUser.Surname}, aktivasyon işleminiz başarılıdır.";
                        return View();

                    }
                }

                return View();

            }
            catch (Exception ex)
            {
                //TODO: ex Loglama
                ModelState.AddModelError("", "Beklenmedik bir hata oluştu!");
                return View();
            }


        }
        #endregion

        #region Login - HttpGet | HttpPost
        [HttpGet]
        public ActionResult Login(string ReturnUrl, string email)
        {
            try
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var url = ReturnUrl.Split('/');
                    // TODO: Burası devam edebilir.
                }
                var model = new LoginViewModel()
                {
                    ReturnUrl = ReturnUrl
                };
                return View(model);
            }
            catch (Exception ex)
            {
                // ex loglanacak.
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var theUser = await myUserManager.FindAsync(model.Email, model.Password);
                if (theUser == null)
                {
                    ModelState.AddModelError(string.Empty, "Emailinizi veya şifrenizi doğru girdiğinizden emin olunuz!");
                    return View(model);
                }
                if (theUser.Roles.FirstOrDefault().RoleId == myRoleManager.FindByName(Enum.GetName(typeof(TheIdentityRoles), TheIdentityRoles.Passive)).Id)
                {
                    ViewBag.TheResult = "Sistemi kullanabilmeniz için üyeliğinizi aktifleştirmeniz gerekmektedir. e-Mail adresinize gönderilen aktivasyon linkine tıklayarak aktifleştirme işlemini yapabilirsiniz!";
                    return View(model);
                }
                var authManager = HttpContext.GetOwinContext().Authentication;
                var userIdentity = await myUserManager.CreateIdentityAsync(theUser, DefaultAuthenticationTypes.ApplicationCookie);
                authManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe
                }, userIdentity);
                if (theUser.Roles.FirstOrDefault().RoleId == myRoleManager.FindByName(Enum.GetName(typeof(TheIdentityRoles), TheIdentityRoles.Admin)).Id)
                {
                    return RedirectToAction("Index", "Admin");

                }
                if (theUser.Roles.FirstOrDefault().RoleId == myRoleManager.FindByName(Enum.GetName(typeof(TheIdentityRoles), TheIdentityRoles.Customer)).Id)
                {
                    return RedirectToAction("Index", "Home");

                }

                if (string.IsNullOrEmpty(model.ReturnUrl))
                    return RedirectToAction("Index", "Home");

                var url = model.ReturnUrl.Split('/');
                if (url.Length == 4)
                {
                    return RedirectToAction(url[2], url[1], new { id = url[3] });
                }
                else
                {
                    return RedirectToAction(url[2], url[1]);
                }
            }
            catch (Exception ex)
            {
                //TODO ex loglanacak
                ModelState.AddModelError("", "Beklenmedik hata oluştu!");
                return View(model);

            }
        }


        #endregion

        #region UpdatePassword - HttpGet | HttpPost
        [HttpGet]
        public ActionResult UpdatePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdatePassword(ProfileViewModel model)
        {
            try
            {
                if (model.NewPassword != model.ConfirmNewPassword)
                {
                    ModelState.AddModelError("", "Şifrler uyuşmuyor.");
                    // TODO: Profile göndermişiz ???
                    return View(model);
                }

                var theUser = myUserManager.FindById(HttpContext.User.Identity.GetUserId());
                var theCheckUser = myUserManager.Find(theUser.UserName, model.OldPassword);

                if (theCheckUser == null)
                {
                    ModelState.AddModelError("", "Mevcut şifreniz yanlış.");
                    // TODO: Profile göndermişiz ???
                    return View();
                }

                await myUserStore.SetPasswordHashAsync(theUser, myUserManager.PasswordHasher.HashPassword(model.NewPassword));
                await myUserStore.UpdateAsync(theUser);
                await myUserStore.Context.SaveChangesAsync();
                TempData["PasswordUpdated"] = "Şifreniz başarıyla güncellenmiştir.";
                HttpContext.GetOwinContext().Authentication.SignOut();
                return RedirectToAction("Login", "Account", new { email = theUser.Email });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Beklenmedik bir hata oluştur.");
                return View(model);
            }
        }
        #endregion

        #region UserProfile - HttpGet | HttpPost
        [HttpGet]
        [Authorize]
        public ActionResult UserProfile()
        {

            var theUser = myUserManager.FindById(HttpContext.User.Identity.GetUserId());
            var model = new ProfileViewModel()
            {
                Email = theUser.Email,
                Name = theUser.Name,
                Surname = theUser.Surname,
                Username = theUser.UserName
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> UserProfile(ProfileViewModel model)
        {
            try
            {
                var theUser = myUserManager.FindById(HttpContext.User.Identity.GetUserId());
                if (theUser == null)
                {
                    ModelState.AddModelError("", "Kullanıcı bulunamadığı için işlem bulunamıyor.");
                    return View(model);
                }
                theUser.Name = model.Name;
                theUser.Surname = model.Surname;
                // TODO: Telefon numarası eklenebilir.;
                await myUserStore.UpdateAsync(theUser);
                await myUserStore.Context.SaveChangesAsync();
                ViewBag.TheResult = "Bilgileriniz güncelleşmiştir";
                var newModel = new ProfileViewModel()
                {
                    Email = theUser.Email,
                    Name = theUser.Name,
                    Surname = theUser.Surname,
                    Username = theUser.UserName
                };
                return View(newModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Beklenmedik bir hata oluştu!. HATA: " + ex.Message);
                return View(model);

            }

        }
        #endregion

        #region RecoverPassword - HttpGet | HttpPost
        [HttpGet]
        public ActionResult RecoverPassword()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RecoverPassword(ProfileViewModel model)
        {
            try
            {
                var theUser =
                    myUserStore.Context.Set<ApplicationUser>()
                    .FirstOrDefault(x => x.Email == model.Email);
                if (theUser == null)
                {
                    ViewBag.TheResult = "Sistemde böyle bir kullanıcı olmadığı için şifre yenileyemiyoruz. Lütfen önce sisteme kayıt olunuz!";
                    return View(model);
                }
                var randomPassword = CreateRandomNewPassword();
                await myUserStore.SetPasswordHashAsync(theUser, myUserManager.PasswordHasher.HashPassword(randomPassword));
                await myUserStore.UpdateAsync(theUser);
                string siteUrl = Request.Url.Scheme + Uri.SchemeDelimiter + Request.Url.Host +
                    (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port);
                await SiteSettings.SendMail(new MailModel()
                {
                    To = theUser.Email,
                    Subject = "ECommerceLite Site - Şifreniz Yenilendi",
                    Message = $"Merhaba {theUser.Name} {theUser.Surname} <br/>Yeni Şifreniz :<b>{randomPassword}</b>" +
                    $"Sisteme giriş yapmak için<b><a href='{siteUrl}/Account/Login?email={theUser.Email}'>BURAYA</a></b> tıklayınız."
                });
                ViewBag.TheResult = "Email adresinize yeni şifreniz gönderilmiştir";
                return View();

            }
            catch (Exception ex)
            {
                ViewBag.TheResult = "Sistemsel bir hata oluştu. Tekrar deneyiniz.";
                return View(model);
                //TODO: ex loglanacak
            }
        }

        #endregion

        #region Logout
        [Authorize]
        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
        #endregion

    }
}