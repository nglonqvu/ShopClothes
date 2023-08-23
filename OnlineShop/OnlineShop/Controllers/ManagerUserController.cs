using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.DataAccess;
using System.Collections.Generic;

namespace OnlineShop.Controllers
{
    public class ManagerUserController : Controller
    {

        // GET: ManagerUserController
        private readonly PRN211_BL5Context _context;
        public ManagerUserController(PRN211_BL5Context context)
        {
            _context = context;
        }
        public async Task<ActionResult> Index()
        {
            var users = await _context.Users
                .Include(users => users.RoleNavigation)
                .ToListAsync(); 
            return View(users);
        }

        // GET: ManagerUserController/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include (users => users.RoleNavigation)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: ManagerUserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManagerUserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (var context = new PRN211_BL5Context())
            {
                var item = context.Users.FirstOrDefault(i => i.UserId == id);

                if (item == null)
                {
                    return NotFound();
                }

                // Lấy danh sách các vai trò từ cơ sở dữ liệu
                var roles = context.Roles.ToList();
                ViewBag.Role = new SelectList(roles, "RoleId", "RoleName"); // RoleId và RoleName phải trùng với tên trong model Role

                return View(item);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User updatedItem)
        {
            if (ModelState.IsValid)
            {
                using (var context = new PRN211_BL5Context())
                {
                    var existingItem = context.Users.FirstOrDefault(i => i.UserId == updatedItem.UserId);

                    if (existingItem == null)
                    {
                        return NotFound();
                    }

                    // Cập nhật các thông tin cần thiết của updatedItem
                    existingItem.Role = updatedItem.Role;
                    // Cập nhật các thông tin khác

                    context.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            // Nếu ModelState không hợp lệ, hiển thị lại form với thông tin đã nhập
            return View(updatedItem);
        }


        // GET: ManagerUserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ManagerUserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
