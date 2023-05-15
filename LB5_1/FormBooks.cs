using LB5_1._Database;
using System.Drawing.Imaging;

namespace LB5_1
{
    public partial class FormBooks : Form
    {
        public FormBooks()
        {
            InitializeComponent();
        }

        private void btnPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.Image = Image.FromFile(dialog.FileName);
                    pictureBox1.Size = new Size(100, 140);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }

        private byte[] GetImageBytes(Image image)
        {
            if (image == null)
            {
                return null;
            }
            using (Bitmap bitmap = new Bitmap(image, new Size(300, 400)))
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    bitmap.Save(stream, ImageFormat.Jpeg);
                    return stream.ToArray();
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            using (DataContext db = new DataContext())
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(textBoxTitle.Text) || string.IsNullOrWhiteSpace(textBoxAuthor.Text) || string.IsNullOrWhiteSpace(textBoxYear.Text) || string.IsNullOrWhiteSpace(textBoxText.Text))
                    {
                        MessageBox.Show("Заполните все поля");
                        return;
                    }

                    Book book = new Book
                    {
                        Title = textBoxTitle.Text,
                        Author = textBoxAuthor.Text,
                        Year = Convert.ToInt32(textBoxYear.Text),
                        Text = textBoxText.Text,
                        Image = GetImageBytes(pictureBox1.Image)
                    };
                    db.Books.Add(book);
                    db.SaveChanges();
                    MessageBox.Show($"Книга {textBoxTitle.Text} добавлена");
                    textBoxTitle.Clear();
                    textBoxAuthor.Clear();
                    textBoxYear.Clear();
                    textBoxText.Clear();
                    pictureBox1.Image = null;
                    Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
