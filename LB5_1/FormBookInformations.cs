using LB5_1._Database;
using System.Diagnostics;

namespace LB5_1
{
    public partial class FormBookInformations : Form
    {
        private Book book;

        public FormBookInformations(Book book)
        {
            InitializeComponent();
            this.book = book;
            LoadBook();
        }

        private void LoadBook()
        {
            // Отображение информации о книге на форме
            titleLabel.Text = book.Title;
            authorLabel.Text = book.Author;
            yearLabel.Text = book.Year.ToString();
            if (book.Image != null)
            {
                using (var ms = new MemoryStream(book.Image))
                {
                    imagePictureBox.Image = Image.FromStream(ms);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = book.Text; // путь к файлу
                Process.Start(new ProcessStartInfo { FileName = filePath, UseShellExecute = true });
                MessageBox.Show("Происходит открытие");
            }
            catch
            {
                MessageBox.Show("Книга не добавлена или добавлена некоректно");
            }
        }
    }
}
