using LB5_1._Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    }
}
