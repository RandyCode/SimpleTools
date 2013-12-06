using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Pkcs;
using System.Net.Mail;
using System.IO;


namespace SendEncryptedEmail
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program(); try
            {
                string[] attachments = new string[] { "attachment.txt" };
                //MailMessage mail = p.GetHtmlMessage("randy.lai@egoal-tech.com", "361703739@qq.com", "TEST MAIL", "<a href='www.baidu.com'>Baidu</a>", attachments);
                MailMessage mail = p.GenerateHtmlMessage("randy.lai@egoal-tech.com", "361703739@qq.com", "TEST MAIL", "<a href='www.baidu.com'>Baidu</a>", attachments);
            
                SmtpClient client = new SmtpClient("smtp.live.com");
                client.Port = 25;
                client.Credentials = new System.Net.NetworkCredential("randy.lai@egoal-tech.com", "like6522626");
                client.EnableSsl = true;

                client.Send(mail);

                Console.WriteLine("SEND");
                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Read();
            }
        }

        #region normal email
        private MailMessage GetHtmlMessage(string form, string to, string subject, string content, string[] attachementFilepaths)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(form);
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = content;
            mail.IsBodyHtml = true;
            foreach (string file in attachementFilepaths)
            {
                FileInfo fileInfo = new FileInfo(file);
                mail.Attachments.Add(GenerateAttachment(fileInfo.Name, File.ReadAllBytes(file)));
            }
            return mail;
        }

        private Attachment GenerateAttachment(string filename, byte[] data)
        {
            return new Attachment(new MemoryStream(data), filename);
        } 
        #endregion


        private MailMessage GenerateHtmlMessage(string from, string to, string subject, string content, string[] attachmentFilepaths)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(to);
            mail.Subject = subject;
            string body = null;
            if (attachmentFilepaths != null && attachmentFilepaths.Length > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("MIME-Version: 1.0\r\n");
                sb.Append("Content-Type: multipart/mixed; boundary=unique-boundary-1\r\n");
                sb.Append("\r\n");
                sb.Append("This is a multi-part message in MIME format.\r\n");
                sb.Append("--unique-boundary-1\r\n");
                sb.Append("Content-Type: text/html\r\n");  //could use text/plain as well here if you want a plaintext message
                sb.Append("Content-Transfer-Encoding: 7Bit\r\n\r\n");
                sb.Append(content);
                if (!content.EndsWith("\r\n"))
                    sb.Append("\r\n");
                sb.Append("\r\n\r\n");
                foreach (string filepath in attachmentFilepaths)
                {
                    sb.Append(GenerateRawAttachement(filepath));
                }
                body = sb.ToString();
            }
            else
            {
                body = "Content-Type: text/html\r\nContent-Transfer-Encoding: 7Bit\r\n\r\n" + content;
            }
            //input your certification and private key.
            X509Certificate2 cert = new X509Certificate2("emailcertification.pfx", "6522626", X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet);
            ContentInfo contentInfo = new ContentInfo(Encoding.UTF8.GetBytes(body));
            SignedCms signedCms = new SignedCms(contentInfo, false);
            CmsSigner Signer = new CmsSigner(SubjectIdentifierType.IssuerAndSerialNumber, cert);
            signedCms.ComputeSignature(Signer);
            byte[] signedBytes = signedCms.Encode();
            MemoryStream stream = new MemoryStream(signedBytes);
            AlternateView view = new AlternateView(stream, "application/pkcs7-mime; smime-type=signed-data;name=smime.p7m");
            mail.AlternateViews.Add(view);

            return mail;
        }

        private string GenerateRawAttachement(string filepath)
        {
            StringBuilder sb = new StringBuilder();
            FileInfo fileInfo = new FileInfo(filepath);
            sb.Append("--unique-boundary-1\r\n");
            sb.Append("Content-Type: application/octet-stream; file=" + fileInfo.Name + "\r\n");
            sb.Append("Content-Transfer-Encoding: base64\r\n");
            sb.Append("Content-Disposition: attachment; filename=" + fileInfo.Name + "\r\n");
            sb.Append("\r\n");
            byte[] binaryData = File.ReadAllBytes(filepath);
            string base64Value = Convert.ToBase64String(binaryData, 0, binaryData.Length);
            int position = 0;
            while (position < base64Value.Length)
            {
                int size = 100;
                if (base64Value.Length - (position + size) < 0)
                    size = base64Value.Length - position;
                sb.Append(base64Value.Substring(position, size));
                sb.Append("\r\n");
                position += size;
            }
            sb.Append("\r\n");
            return sb.ToString();
        }
    }
}
