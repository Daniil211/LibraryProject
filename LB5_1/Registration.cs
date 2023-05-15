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
using System.Drawing.Imaging;

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
        using (DataContext db = new DataContext())
        {
            if (!string.IsNullOrEmpty(textBoxLog.Text) && !string.IsNullOrEmpty(textBoxPas.Text) && !string.IsNullOrEmpty(textBoxEmail.Text))
            {
                User user = new User
                {
                    Login = textBoxLog.Text,
                    Password = GetHashString(textBoxPas.Text),
                    Email = textBoxEmail.Text,
                    Role = "User",
                    Photo = GetImageBytes(pictureBoxPhoto.Image)
                };
                db.Users.Add(user);
                db.SaveChanges();
                MessageBox.Show("Аккаунт " + textBoxLog.Text + " зарегистрирован");
                textBoxLog.Clear();
                textBoxPas.Clear();
                textBoxEmail.Clear();
                pictureBoxPhoto.Image = null;
                Authorization form = new Authorization();
                this.Hide();
                form.Show();
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }
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

        private byte[] GetImageBytes(Image image)
        {
            if (image == null)
            {
                return null;
            }
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, ImageFormat.Jpeg);
                return stream.ToArray();
            }
        }


        private void buttonUploadPhoto_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pictureBoxPhoto.Image = Image.FromFile(dialog.FileName);
            }
        }
    }
}

