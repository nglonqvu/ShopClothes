using Microsoft.AspNetCore.Mvc;
using OnlineShop.DataAccess;
using OnlineShop.DataAccess.DAO;
using OnlineShop.Models;
using WebClothes.ultils;

namespace OnlineShop.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserDAO _userDAO;
        private readonly Mailer mailer;
        private readonly Md5 md5;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProfileController(IWebHostEnvironment webHostEnvironment)
        {
            md5 = new Md5();
            mailer = new Mailer();
            _userDAO = new UserDAO();
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            User user = GetCurrentLoggedInUser();
            bool isLoggedIn = (user != null);
            ViewBag.IsLoggedIn = isLoggedIn;
            if (!isLoggedIn)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ProfileModel model = new ProfileModel
                {
                    FullName = user.FullName,
                    Email = user.Email,
                    Avatar = user.Avatar,
                    Address = user.Address,
                    Birthday = user.Dob,
                    Gender = user.Gender,
                    PhoneNumber = user.Phone,
                    RoleName = user.RoleNavigation?.RoleName
                };
                int totalFields = 7;
                int completedFields = 0;

                if (!string.IsNullOrEmpty(model.FullName))
                    completedFields++;
                if (!string.IsNullOrEmpty(model.Email))
                    completedFields++;
                if (!string.IsNullOrEmpty(model.Avatar))
                    completedFields++;
                if (!string.IsNullOrEmpty(model.Address))
                    completedFields++;
                if (model.Birthday.HasValue)
                    completedFields++;
                if (model.Gender.HasValue)
                    completedFields++;
                if (!string.IsNullOrEmpty(model.PhoneNumber))
                    completedFields++;

                int completionPercentage = (int)((float)completedFields / totalFields * 100);

                model.CompletionPercentage = completionPercentage;
                return View(model);
            }

        }

        [HttpPost]
        public IActionResult UpdateProfile(ProfileModel model, IFormFile avatarFile)
        {


            var check = _userDAO.GetUser(model.Email);
            if (check != null && check.Email != model.Email)
            {
                ModelState.AddModelError("Email", "Email đã tồn tại.");
                ViewBag.EmailExistsError = "Email đã tồn tại.";
                return View(model);
            }

            User user = GetCurrentLoggedInUser();

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (avatarFile != null && avatarFile.Length > 0)
            {
                string fileName = avatarFile.FileName;
                string uniqueFileName = "UserID _" + user.UserId + Guid.NewGuid().ToString() + "_" + fileName;

                string webRootPath = _webHostEnvironment.WebRootPath;
                string contentRootPath = _webHostEnvironment.ContentRootPath;
                string path = "";

                string filePath = Path.Combine(webRootPath + "\\Avatars", uniqueFileName);

                Directory.CreateDirectory(webRootPath);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    avatarFile.CopyTo(fileStream);
                }

                model.Avatar = uniqueFileName;
            }
            else
            {
                model.Avatar = user.Avatar;
            }

            user.FullName = model.FullName;
            user.Phone = model.PhoneNumber;
            user.Email = model.Email;
            user.Address = model.Address;
            user.Gender = model.Gender;
            user.Dob = model.Birthday;
            user.Avatar = model.Avatar;

            _userDAO.Update(user);

            TempData["SuccessMessage"] = "Bạn đã cập nhật thành công thông tin cá nhân.";


            return RedirectToAction("Index", "Profile");
        }


        [HttpPost]
        public IActionResult UpdatePassword(ChangePasswordModel model)
        {
            User user = GetCurrentLoggedInUser();
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }

            AuthenticateUser auth = new AuthenticateUser();
            if (!auth.checkResetPass(user.Password, model.oldPassword))
            {
                ModelState.AddModelError("CurrentPassword", "Mật khẩu hiện tại không đúng.");
                ViewBag.EmailExistsError = "Mật khẩu hiện tại không đúng.";
                TempData["FailMessage"] = "Mật khẩu cũ không chính xác. Vui lòng kiểm tra lại.";
                return RedirectToAction("Index", "Profile");
            }
            else if (model.newPassword.Contains(" ") || model.newPassword.Length <= 6)
            {
                TempData["FailMessage"] = "Yêu cầu mật khẩu lớn hơn 6 và không chứa khoảng trắng. Vui lòng kiểm tra lại.";
                return RedirectToAction("Index", "Profile");
            }
            else
            {
                if (model.newPassword != model.conFirmPassword)
                {
                    TempData["FailMessage"] = "Mật khẩu mới và xác nhận không khớp. Vui lòng kiểm tra lại.";
                    return RedirectToAction("Index", "Profile");
                }
            }

            Md5 md5 = new Md5();
            user.Password = md5.MD5Create(model.newPassword);

            _userDAO.Update(user);

            TempData["SuccessMessage"] = "Bạn đã thay đổi mật khẩu thành công.";


            return RedirectToAction("Index", "Profile");
        }



        [HttpPost]
        public IActionResult DeleteAccount()
        {
            try
            {
                User currentUser = GetCurrentLoggedInUser();
                if (currentUser != null)
                {
                    if (currentUser.Role == 2)
                    {
                        _userDAO.Remove(currentUser.UserId);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Error");
        }

        public IActionResult SendPass(string email)
        {
            User user = GetCurrentLoggedInUser();
            var subject = "http://localhost:5057/ForgotPassword/ResetPassword/" + user.UserId;
            bool check = mailer.Send(email, "Yêu cầu khôi phục mật khẩu hiện tại", "Mật khẩu hiện tại của bạn là: Không cóa đâu. Mơ đi :))" + "Vào đây thay đổi nè: " + subject);
            if (check)
            {
                TempData["SuccessSendPass"] = "Mật khẩu hiện tại của bạn đã được gửi đến địa chỉ email : " + email + ".";
            }
            else
            {
                TempData["FailSendPass"] = "Hãy kiểm tra lại kết nối mạng của bạn hoặc các vấn đề khác.";
            }
            return RedirectToAction("Index", "Profile");
        }

        private User GetCurrentLoggedInUser()
        {

            string email = HttpContext.Session.GetString("Email");
            if (!string.IsNullOrEmpty(email))
            {
                return _userDAO.GetUser(email);
            }

            return null;
        }
    }
}
