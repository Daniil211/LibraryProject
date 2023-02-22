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
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (UserContext db = new UserContext())
            {
                if (textBoxLog.Text != null && textBoxPas.Text != null && textBoxEmail != null)
                {
                    User user = new User(textBoxLog.Text,
                this.GetHashString(textBoxPas.Text),
                textBoxEmail.Text, "User");
                    db.Users.Add(user);
                    db.SaveChanges();
                    MessageBox.Show("Аккаунт " + textBoxLog.Text + " зарегистрирован");
                    textBoxLog.Clear();
                    textBoxEmail.Clear();
                    textBoxPas.Clear();
                    Authorization form = new Authorization();
                    this.Hide();
                    form.Show();
                }
                else
                    MessageBox.Show("Заполните все поля");
            }
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
    }
}
