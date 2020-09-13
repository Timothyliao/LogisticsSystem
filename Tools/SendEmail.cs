using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace LogisticsSystem.Tools
{
    public static class SendEmail
    {
        public static bool Send(string content,string tel, string receiverEmail)
        {
            SmtpClient client = new SmtpClient()
            {
                Host = "smtp.qq.com",
                Port = 25,//通过网络发送到SMTP服务器
                DeliveryMethod = SmtpDeliveryMethod.Network,
                //发件人登录邮箱的用户名和密码
                Credentials = new NetworkCredential("1692932745", "vsdnbckpqvljchhf")
            };
            MailAddress fromAddress = new MailAddress("1692932745@qq.com", "Maverick");
            MailAddress toAddress = new MailAddress(receiverEmail, "user");
            MailMessage mailMessage = new MailMessage(fromAddress, toAddress);
            mailMessage.Subject = "Maverick系统验证码";//邮件标题
            mailMessage.Body = string.Format("您的账号{0}正在进行修改密码操作,验证码为{1},若不是本人操作，请联系管理员.", tel, content);//邮件内容
            mailMessage.SubjectEncoding = Encoding.UTF8;
            mailMessage.BodyEncoding = Encoding.UTF8;// Encoding.GetEncoding("GB2312");
            mailMessage.Priority = MailPriority.High;
            try
            {
                client.Send(mailMessage);//发送邮件
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
