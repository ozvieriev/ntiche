using System.Net.Mail;
using System.Text;

namespace Email.Templates
{
    public class EmailSender
    {
        public static void Send(string subject, string to, EmailTemplate emailTemplate, Notification notification, Attachment attachement = null)
        {
            var razorEmailTemplate = new RazorTemplate(emailTemplate);
            var template = razorEmailTemplate.Compile(notification);

            var razorLayout = new RazorTemplate(EmailTemplate.Layout);
            var layout = razorLayout.Compile(notification);

            using (var message = new MailMessage())
            {
                message.To.Add(to);
                message.Subject = subject;
                message.SubjectEncoding = Encoding.UTF8;
                message.Body = layout.Replace("**Body**", template);
                message.IsBodyHtml = true;
                message.BodyEncoding = Encoding.UTF8;

                if (!object.Equals(attachement, null))
                    message.Attachments.Add(attachement);

                using (var client = new SmtpClient())
                    client.Send(message);
            }
        }
    }
}