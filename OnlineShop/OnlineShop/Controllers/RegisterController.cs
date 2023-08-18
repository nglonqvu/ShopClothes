using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DataAccess;
using OnlineShop.DataAccess.DAO;
using OnlineShop.Models;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Text;
using WebClothes.ultils;

namespace OnlineShop.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserDAO userDAO;

        public RegisterController()
        {
            userDAO = new UserDAO();
        }
        public HttpContext GetHttpContext()
        {
            return HttpContext;
        }


        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.EmailExistsError = null;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(RegisterModel model)
        {
            if (string.IsNullOrEmpty(model.Email))
            {
                TempData["msgEmail"] = "Yêu cầu nhập email.";
            }
            else
            {
                if (userDAO.GetUser(model.Email) != null)
                {
                    TempData["msgEmail"] = "Email này đã được đăng ký.";
                    ModelState.AddModelError("Email", "Email này đã được đăng ký.");
                    ViewBag.EmailExistsError = "Email này đã được đăng ký.";
                    model.Email = "";
                }
            }

            if (string.IsNullOrEmpty(model.Password))
            {
                TempData["msgPass"] = "Yêu cầu nhập mật khẩu.";
            }
            else
            {
                if (model.Password.Contains(" ") || model.Password.Length <= 6)
                {
                    TempData["msgPass"] = "Yêu cầu mật khẩu lớn hơn 6 và không chứa khoảng trắng.";
                    model.Password = "";
                }
            }

            if (string.IsNullOrEmpty(model.FullName))
            {
                TempData["msgName"] = "Yêu cầu nhập họ và tên.";
            }

            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.FullName))
            {
                return View(model);
            }
            else
            {
                Md5 md5 = new Md5();
                string hashedPassword = md5.MD5Create(model.Password);
                string code = new Random().Next(10000).ToString();
                User user = new User()
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    Password = hashedPassword,
                    Status = false,
                    Role = 2,
                    Avatar = "UserID _1660f525d-d8f8-4557-9a6f-f053926892bf_Sample_User_Icon.png",
                    CodeVerify = code
                };
                userDAO.SignUp(user);
                Mailer mailer = new Mailer();
                bool _check = mailer.Send(model.Email, "Yêu cầu xác thực tài khoản", "Mã xác thực là: " + code);
                if (_check)
                {
                    TempData["email"] = model.Email;
                    TempData["msgSuccess"] = "Bạn đã đăng ký thành công! Hãy kiểm tra email, lấy mã xác thực để kích hoạt tài khoản.";
                }
                else
                {
                    TempData["msgSendFail"] = "Hãy kiểm tra lại kết nối mạng của bạn hoặc các vấn đề khác.";
                    TempData["msgSuccessSendFail"] = "Bạn đã đăng ký thành công! Hãy kiểm tra email, lấy mã xác thực để kích hoạt tài khoản.";
                }
            }
            return View(model);
        }
    }
}
