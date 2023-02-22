using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
namespace LB5_1
{
    public partial class Authorization : Form
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxLog.Text != null && textBoxPas.Text != null)
            {
                using (UserContext db = new UserContext())
                {

                    foreach (User user in db.Users)
                    {

                        if (textBoxLog.Text == user.Login &&
                        this.GetHashString(textBoxPas.Text) == user.Password)
                        {
                            MessageBox.Show("успешно!");
                            UserForm userForm = new UserForm();
                            this.Hide();
                            MessageBox.Show("вошли " + user.Login);
                            userForm.Show();
                            return;
                        }
                    }
                    MessageBox.Show("Логин или пароль указан неверно!");
                }
            }
            else
                MessageBox.Show("Введите все поля");
        }
        private string GetHashString(string s)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(s);

            MD5CryptoServiceProvider CSP = new
            MD5CryptoServiceProvider();
            byte[] byteHash = CSP.ComputeHash(bytes);
            string hash = "";
            foreach (byte b in byteHash)
            {
                hash += string.Format("{0:x2}", b);
            }
            return hash;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Registration form = new Registration();
            this.Hide();
            form.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            RecoverAcc form = new RecoverAcc();
            this.Hide();
            form.Show();
        }
    }
}
