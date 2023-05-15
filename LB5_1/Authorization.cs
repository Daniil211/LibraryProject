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
using LB5_1._Database;

namespace LB5_1
{
    public partial class Authorization : Form
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private string GetHashString(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Registration form = new Registration();
            this.Hide();
            form.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            using (DataContext db = new DataContext())
            {
                try
                {
                    string login = textBoxLog.Text;
                    User user = db.Users.FirstOrDefault(u => u.Login == login);
                    RecoverAcc form = new RecoverAcc(user);
                    this.Hide();
                    form.Show();
                }
                catch
                {
                    MessageBox.Show("Error");
                }
            }
        }

        private void buttonAuth_Click_1(object sender, EventArgs e)
        {
            using (DataContext db = new DataContext())
            {
                string login = textBoxLog.Text;
                string password = GetHashString(textBoxPas.Text);
                User user = db.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
                if (user != null)
                {
                    MessageBox.Show("Добро пожаловать, " + user.Login);
                    UserForm form = new UserForm(user);
                    this.Hide();
                    form.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль");
                }
            }
        }
    }
}
