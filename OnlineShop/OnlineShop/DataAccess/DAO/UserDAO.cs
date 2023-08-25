using Microsoft.EntityFrameworkCore;

namespace OnlineShop.DataAccess.DAO
{
    public class UserDAO
    {
        public async Task<User> GetUser(string email)
        {
            User user = null;
            try
            {
                using (var context = new PRN211_BL5Context())
                {
                    user = await context.Users
                            .Include(u => u.RoleNavigation)
                            .SingleOrDefaultAsync(u => u.Email.Equals(email));

                    return user;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }


        }


        public async Task<User> GetUserById(int Id)
        {
            User user;
            try
            {
                using (var context = new PRN211_BL5Context())
                {
                    user = await context.Users.Include(u => u.RoleNavigation).FirstOrDefaultAsync(u => u.UserId == Id);
                    return user;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task Update(User user)
        {
            try
            {
                using (var context = new PRN211_BL5Context())
                {
                    context.Users.Update(user);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task SignUp(User user)
        {
            try
            {
                using (var context = new PRN211_BL5Context())
                {
                    User _user = await context.Users.SingleOrDefaultAsync(u => u.Email.ToLower().Equals(user.Email.ToLower()));

                    if (_user == null)
                    {
                        context.Users.Add(user);
                        await context.SaveChangesAsync();
                    }
                    else
                    {
                        throw new Exception("Email is existed! Please try again or login with your email!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Remove(int userId)
        {
            try
            {
                User user = await GetUserById(userId);
                if (user != null)
                {
                    using (var context = new PRN211_BL5Context())
                    {
                        context.Users.Remove(user);
                        await context.SaveChangesAsync();
                    }
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

