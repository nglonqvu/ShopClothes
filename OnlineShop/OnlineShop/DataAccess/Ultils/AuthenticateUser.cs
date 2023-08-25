
using OnlineShop.DataAccess;
using OnlineShop.DataAccess.DAO;

namespace WebClothes.ultils
{
    public class AuthenticateUser
    {
        UserDAO userDAO = new UserDAO();

        internal async Task<bool> checkLogin(string email, string password)
        {
            User user = await userDAO.GetUser(email);
            if (user != null)
            {
                Md5 md5 = new Md5();
                return await md5.MD5Create(password) == user.Password;
            }
            return false;
        }

        internal async Task<bool> checkResetPass(string oldPassword, string newPassword)
        {
            Md5 md5 = new Md5();
            return await md5.MD5Create(newPassword) == oldPassword;
        }
    }
}
