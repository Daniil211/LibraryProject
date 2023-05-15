using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LB5_1._Database;
using static System.Reflection.Metadata.BlobBuilder;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace LB5_1
{
    public partial class FormAdminStart : Form
    {
        private User currentUser;
        private List<Book> books;
        public FormAdminStart(User user)
        {
            InitializeComponent();
            this.currentUser = user;
            LoadBooks();

            // Создаем таймер
            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 5000; // Интервал обновления в миллисекундах
            timer.Tick += timer1_Tick;
            timer.Start();

            if (currentUser.Photo != null)
            {
                using (var ms = new MemoryStream(currentUser.Photo))
                {
                    pictureBox1.Image = System.Drawing.Image.FromStream(ms);
                }
            }
            if (currentUser.Photo != null)
            {
                using (var ms = new MemoryStream(currentUser.Photo))
                {
                    var image = System.Drawing.Image.FromStream(ms);
                    var thumbnail = image.GetThumbnailImage(100, 100, null, IntPtr.Zero);
                    pictureBox1.Image = thumbnail;
                }
            }
            else
            {
                var testImage = System.Drawing.Image.FromFile("C:\\Users\\id202\\Desktop\\Важные репозитории\\LB5_1\\LB5_1\\_Image\\profiles.png");
                var thumbnail = testImage.GetThumbnailImage(100, 100, null, IntPtr.Zero);
                pictureBox1.Image = thumbnail;
            }

            pictureBox1.Click += (sender, e) =>
            {
                // Создаем новую форму для отображения данных о пользователе
                var userForm = new FormUserInformation(currentUser);
                userForm.ShowDialog();
            };
        }
        private void buttonPanel_Click(object sender, EventArgs e)
        {
            FormAdminPanel form = new FormAdminPanel();
            form.ShowDialog();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
        private void LoadBooks()
        {
            using (var db = new DataContext())
            {
                books = db.Books.ToList();
            }

            // Очищаем flowLayoutPanel1
            flowLayoutPanel1.Controls.Clear();

            // Создание карточек для каждой книги
            foreach (var book in books)
            {
                var card = new Panel();
                card.BorderStyle = BorderStyle.FixedSingle;
                card.Margin = new Padding(10);
                card.Width = 200;
                card.Height = 300;

                var image = new PictureBox();
                image.SizeMode = PictureBoxSizeMode.StretchImage;
                image.Width = 180;
                image.Height = 180;
                image.Top = 10;
                image.Left = 10;
                if (book.Image != null)
                {
                    using (var ms = new MemoryStream(book.Image))
                    {
                        image.Image = System.Drawing.Image.FromStream(ms);
                    }
                }
                card.Controls.Add(image);

                var title = new Label();
                title.Text = book.Title;
                title.Width = 180;
                title.Top = 200;
                title.Left = 10;
                card.Controls.Add(title);

                var author = new Label();
                author.Text = book.Author;
                author.Width = 180;
                author.Top = 220;
                author.Left = 10;
                card.Controls.Add(author);

                card.Click += (sender, e) =>
                {
                    // Создание новой формы для отображения информации о книге
                    var bookForm = new FormBookInformations(book);
                    bookForm.ShowDialog();
                };

                flowLayoutPanel1.Controls.Add(card);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormBooks form = new FormBooks();
            form.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LoadBooks();
        }
    }
}
