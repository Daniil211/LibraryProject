using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using System.Data.Entity;
using LB5_1._Database;

namespace LB5_1
{
    public partial class RecoverAcc : Form
    {
        public RecoverAcc()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxEmail != null)
            {
                try
                {
                    MailAddress from = new MailAddress("", "");
                    MailAddress to = new MailAddress(textBoxEmail.Text);
                    MailMessage m = new MailMessage(from, to);
                    m.Subject = "Тест";
                    using (DataContext db = new DataContext())
                    {
                        foreach (User user in db.Users)
                        {
                            if (textBoxEmail.Text == user.Email)
                            {
                                m.Body = "<h1>Пароль: " + user.Password + "</h1>";
                            }
                        }
                    }
                    m.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
                    smtp.Credentials = new NetworkCredential("", "");
                    smtp.EnableSsl = true;
                    smtp.Send(m);
                }
                catch
                {
                    MessageBox.Show("Ошибка");
                }
            }
            else
                MessageBox.Show("Поле не заполнено");
        }

    }
}
