using Microsoft.AspNetCore.Mvc;
using OnlineShop.DataAccess;
using OnlineShop.DataAccess.DAO;
using OnlineShop.Models;
using System.Security.Cryptography;
using WebClothes.ultils;

namespace OnlineShop.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserDAO userDAO;
        private readonly Mailer mailer;
        public LoginController()
        {
            userDAO = new UserDAO();
            mailer = new Mailer();
        }
        public IActionResult Index()
        {
            return View();
        }

        public HttpContext GetHttpContext()
        {
            return HttpContext;
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                AuthenticateUser auth = new AuthenticateUser();
                if (await auth.checkLogin(model.Email, model.Password))
                {
                    User user = await userDAO.GetUser(model.Email);
                    if (user != null)
                    {
                        if ((bool)user.Status)
                        {
                            HttpContext.Session.SetString("Email", model.Email);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            string code = new Random().Next(10000).ToString();
                            user.CodeVerify = code;
                            await userDAO.Update(user);
                            bool _check = await mailer.Send(user.Email, "Yêu cầu xác thực tài khoản", "Mã xác thực là: " + code);
                            if (_check)
                            {
                                TempData["email"] = user.Email;
                                TempData["msgNonVerify"] = "Tài khoản của bạn chưa được kích hoạt! Hãy xác thực email để kích hoạt trước khi đăng nhập.";
                            }
                            else
                            {
                                TempData["msgNonVerifyFail"] = "Tài khoản của bạn chưa được kích hoạt! Hãy xác thực email để kích hoạt trước khi đăng nhập.";
                                TempData["msgSendFail"] = "Hãy kiểm tra lại kết nối mạng của bạn hoặc các vấn đề khác.";
                            }
                        }
                    }
                }
                else
                {
                    TempData["msgFail"] = "Email hoặc mật khẩu không chính xác !";
                    ModelState.AddModelError(string.Empty, "Email hoặc mật khẩu không chính xác !");
                }
            }
            return View(model);
        }
    }
}
