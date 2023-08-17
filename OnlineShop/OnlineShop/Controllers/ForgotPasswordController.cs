using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using OnlineShop.DataAccess.DAO;
using OnlineShop.Models;
using System.Linq;
using System.Text;
using WebClothes.ultils;

namespace OnlineShop.Controllers
{
    public class ForgotPasswordController : Controller
    {
        private readonly UserDAO userDAO;
        private readonly Mailer mailer;
        private readonly Md5 md5;

        public ForgotPasswordController()
        {
            userDAO = new UserDAO();
            mailer = new Mailer();
            md5 = new Md5();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoginModel model)
        {
            var check = userDAO.GetUser(model.Email);
            if (check == null)
            {
                TempData["msgNotExist"] = "Email của bạn không tồn tại trong hệ thống !";
                return View(model);
            }

            var subject = "http://localhost:5057/ForgotPassword/ResetPassword/" + check.UserId;
            bool _check = mailer.Send(check.Email, "Yêu cầu thay đổi mật khẩu mới", "Hãy truy cập đường dẫn sau để thay đổi mật khẩu mới: " + subject);
            if (_check == true)
            {
                TempData["SuccessMessage"] = "Liên kết đặt lại mật khẩu đã được gửi đến địa chỉ email : " + check.Email + ".";
            }
            else
            {
                TempData["FailMessage"] = "Hãy kiểm tra lại kết nối mạng của bạn hoặc các vấn đề khác.";
            }

            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string id)
        {
            ChangePasswordModel changePasswordModel = new ChangePasswordModel()
            {
                userID = id
            };

            return View(changePasswordModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResetPassword(ChangePasswordModel model)
        {
            var user = userDAO.GetUserById(int.Parse(model.userID));
            if (model.newPassword != model.conFirmPassword)
            {
                TempData["FailMessage"] = "Mật khẩu mới phải khớp với mật khẩu nhập lại !";
            }
            else if (model.newPassword.Contains(" ") || model.newPassword.Length <= 6)
            {
                TempData["FailMessage"] = "Yêu cầu mật khẩu lớn hơn 6 và không chứa khoảng trắng !";
            }
            else
            {
                user.Password = md5.MD5Create(model.newPassword);
                TempData["SuccessMessage"] = "Bạn đã thay đổi mật khẩu thành công !";
                userDAO.Update(user);
            }
            return RedirectToAction("ResetPassword", "ForgotPassword");
        }
    }
}
