using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;

namespace SenderEmail
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SmtpClient mySmtpClient = new SmtpClient("smtp.mail.ru"); //СМТП СЕВРЕР ПРОПИСЫВАЮ

                // set smtp-client with basicAuthentication
                mySmtpClient.UseDefaultCredentials = false;
                mySmtpClient.EnableSsl = true;

                System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential("ivanovaleksa2228@mail.ru", "0Gu6J4WwYzZenxzZ74B7");
                mySmtpClient.Credentials = basicAuthenticationInfo;

                string[] test = File.ReadAllLines("text.txt"); //беру из файла все строки с емаийлами
                label1.Text = File.ReadAllText("text.txt");

                // add from,to mailaddresses

                foreach (string address in test)
                {
                    MailAddress from = new MailAddress("ivanovaleksa2228@mail.ru", "Александр");

                    MailAddress to = new MailAddress(address, "Александру");

                    MailMessage myMail = new System.Net.Mail.MailMessage(from, to);

                    // add ReplyTo Ответ перенаправлять
                    MailAddress replyTo = new MailAddress("ivanovaleksa2228@mail.ru");

                    myMail.ReplyToList.Add(replyTo);

                    // set subject and encoding
                    myMail.Subject = "Test message!";
                    myMail.SubjectEncoding = System.Text.Encoding.UTF8;

                    // set body-message and encoding
                    myMail.Body = "<b>Test Mail</b><br>using <b>HTML</b>.";
                    myMail.BodyEncoding = System.Text.Encoding.UTF8;
                    // text or html
                    myMail.IsBodyHtml = true; //Если хтмл тру, если нет то фолс
                    mySmtpClient.Send(myMail); //отправка письма
                }

            }


            catch (SmtpException ex)
            {
                throw new ApplicationException
                  ("SmtpException has occured: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            label1.Text = File.ReadAllText("text.txt");
        }
    }
}
