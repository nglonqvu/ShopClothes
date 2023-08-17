using Microsoft.AspNetCore.Mvc;
using OnlineShop.DataAccess;
using OnlineShop.DataAccess.DAO;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    public class VerifyController : Controller
    {
        private readonly UserDAO userDAO;
        public VerifyController()
        {
            userDAO = new UserDAO();
        }

        [HttpGet]
        public IActionResult Index(string email, string action)
        {
            var viewModel = new VerifyModel { Email = email, Action = action };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Index(VerifyModel model)
        {
            User user = userDAO.GetUser(model.Email);

            if (model.Action.Equals("VerifyRegister", StringComparison.OrdinalIgnoreCase))
            {
                if (user != null && user.CodeVerify == model.VerificationCode)
                {
                    user.Status = true;
                    user.CodeVerify = null;
                    userDAO.Update(user);
                    HttpContext.Session.SetString("Email", model.Email);
                    TempData["SuccessRegister"] = "Chúc mừng! Bạn đã kích hoạt tài khoản thành công.";
                }
                else
                {
                    TempData["Fail"] = "Mã xác thực không hợp lệ! Vui lòng kiểm tra lại.";
                }

            }
            else
            {
                if (user != null && user.CodeVerify == model.VerificationCode)
                {
                    user.Status = true;
                    user.CodeVerify = null;
                    userDAO.Update(user);
                    HttpContext.Session.SetString("Email", model.Email);
                    TempData["SuccessLogin"] = "Chúc mừng! Bạn đã kích hoạt tài khoản thành công.";
                }
                else
                {
                    TempData["Fail"] = "Mã xác thực không hợp lệ! Vui lòng kiểm tra lại.";
                }
            }
            return View(model);
        }
    }
}
