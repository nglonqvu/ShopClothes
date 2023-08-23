﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.DataAccess;
using OnlineShop.DataAccess.DAO;
using System.Collections.Generic;
using WebClothes.ultils;

namespace OnlineShop.Controllers
{
    public class ManagerUserController : Controller
    {

        // GET: ManagerUserController
        private readonly UserDAO _userDAO;
       
        public ManagerUserController(IWebHostEnvironment webHostEnvironment)
        {
            
            _userDAO = new UserDAO();
            
        }
        public async Task<ActionResult> Index()
        {
            User user = await GetCurrentLoggedInUser();
            bool isLoggedIn = (user != null);
            int isLoggedIn1 = (user != null && user.Role != null && user.Role != 3) ? 3 : 0;
            ViewBag.IsLoggedIn1 = isLoggedIn1;
            ViewBag.IsLoggedIn = isLoggedIn;
            int? userRole = user?.Role;


            if (!isLoggedIn || (userRole.HasValue && userRole.Value == 3))
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                using (PRN211_BL5Context context = new PRN211_BL5Context())
                {
                    var users = await context.Users
                    .Include(users => users.RoleNavigation)
                    .ToListAsync();
                    return View(users);
                }
            }
        }
        private async Task<User> GetCurrentLoggedInUser()
        {
            string email = HttpContext.Session.GetString("Email");
            if (!string.IsNullOrEmpty(email))
            {
                return await _userDAO.GetUser(email);
            }
            return null;
        }
        // GET: ManagerUserController/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            using (PRN211_BL5Context context = new PRN211_BL5Context())
            {
                var user = await context.Users
                .Include(users => users.RoleNavigation)
                .FirstOrDefaultAsync(u => u.UserId == id);

                if (user == null)
                {
                    return NotFound();
                }

                return View(user);
            }
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

                var roles = context.Roles.ToList();
                ViewBag.Role = new SelectList(roles, "RoleId", "RoleName"); 

                return View(item);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User updatedItem , int id )
        {
            
                using (var context = new PRN211_BL5Context())
                {
                    var existingItem = context.Users.FirstOrDefault(i => i.UserId == id);

                    if (existingItem == null)
                    {
                        return NotFound();
                    }
                    
                    existingItem.Role = updatedItem.Role;

                    context.SaveChanges();
                    return RedirectToAction("Index" , "ManagerUser");
                }
            

            // Nếu ModelState không hợp lệ, hiển thị lại form với thông tin đã nhập
            return View(updatedItem);
        }


        // GET: ManagerUserController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                using (PRN211_BL5Context context = new PRN211_BL5Context())
                {
                    var user = context.Users.FirstOrDefault(p => p.UserId == id);

                    if (user == null)
                    {
                        return NotFound();
                    }

                    context.Users.Remove(user);
                    context.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: ManagerUserController/Delete/5
        
    }
}
