using MailKit.Security;
using MimeKit;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace WebClothes.ultils
{
    public class Mailer
    {
        public bool Send(string to, string sub, string msg)
        {
            string user = "tiendat288966@gmail.com";
            string pass = "hfqcaubtteeyrmef";

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(user, pass),
                EnableSsl = true,
                Timeout = 10000
            };

            try
            {
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(user, "Clothing Online Shop"),
                    Subject = sub,
                    Body = msg,
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8,
                    SubjectEncoding = Encoding.UTF8
                };

                mailMessage.To.Add(new MailAddress(to));

                smtpClient.Send(mailMessage);
                Console.WriteLine("Gửi Email thành công");
            }
            catch (SmtpFailedRecipientsException e)
            {
                foreach (var address in e.InnerExceptions)
                {
                    if (address is SmtpFailedRecipientException failedRecipient)
                    {
                        Console.WriteLine("Địa chỉ không hợp lệ hoặc không tồn tại: " + failedRecipient.FailedRecipient);
                    }
                }
                return false;
            }
            catch (SmtpException e)
            {
                Console.WriteLine("Lỗi: Mất kết nối mạng");
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Lỗi: " + e.Message);
                return false;
            }
            return true;
        }
    }
}
