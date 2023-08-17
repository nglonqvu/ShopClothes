using Microsoft.EntityFrameworkCore;

namespace OnlineShop.DataAccess.DAO
{
    public class UserDAO
    {
        public User GetUser(string email)
        {
            User user = null;
            try
            {
                using var context = new PRN211_BL5Context();
                user = context.Users
                        .Include(u => u.RoleNavigation)
                        .SingleOrDefault(u => u.Email.Equals(email));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            return user;
        }


        public User GetUserById(int Id)
        {
            User user;
            try
            {
                using var context = new PRN211_BL5Context();
                user = context.Users.Include(u => u.RoleNavigation).FirstOrDefault(u => u.UserId == Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }


        public void Update(User user)
        {
            try

            {
                using var context = new PRN211_BL5Context();
                context.Users.Update(user);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void SignUp(User user)
        {
            try
            {
                using var context = new PRN211_BL5Context();
                User _user = context.Users.SingleOrDefault(u => u.Email.ToLower().Equals(user.Email.ToLower()));

                if (_user == null)
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Email is existed! Please try again or login with your email!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(int userId)
        {
            try
            {
                User user = GetUserById(userId);
                if (user != null)
                {
                    using var context = new PRN211_BL5Context();
                    context.Users.Remove(user);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The product not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

