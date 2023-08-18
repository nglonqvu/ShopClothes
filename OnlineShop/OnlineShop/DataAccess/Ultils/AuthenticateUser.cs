
using OnlineShop.DataAccess;
using OnlineShop.DataAccess.DAO;

namespace WebClothes.ultils
{
    public class AuthenticateUser
    {
        UserDAO userDAO = new UserDAO();

        internal bool checkLogin(string email, string password)
        {
            User user = userDAO.GetUser(email);
            if (user != null)
            {
                Md5 md5 = new Md5();
                return md5.MD5Create(password) == user.Password;
            }
            return false;
        }

        internal bool checkResetPass(string oldPassword, string newPassword)
        {
            Md5 md5 = new Md5();
            return md5.MD5Create(newPassword) == oldPassword;
        }
    }
}
