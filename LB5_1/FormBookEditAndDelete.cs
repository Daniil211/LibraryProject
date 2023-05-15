using LB5_1._Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LB5_1
{
    public partial class FormBookEditAndDelete : Form
    {
        DbContext db;
        public FormBookEditAndDelete()
        {
            using (DataContext db = new DataContext())
            {
                InitializeComponent();
                db.Books.Load();
                dataGridView1.DataSource = db.Books.Local.ToBindingList();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            using (DataContext db = new DataContext())
            {
                try
                {
                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                        int index = dataGridView1.SelectedRows[0].Index;
                        int id = 0;
                        bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                        if (converted == false)
                            return;
                        Book book = db.Books.Find(id);
                        db.Books.Remove(book);
                        db.SaveChanges();
                        db.Books.Load();
                        dataGridView1.DataSource = db.Books.Local.ToBindingList();
                        MessageBox.Show("Deleted");
                    }
                }
                catch
                {
                    MessageBox.Show("Error");
                }
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            using (DataContext db = new DataContext())
            {
                try
                {
                    int index = dataGridView1.SelectedRows[0].Index;
                    int id = 0;
                    bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                    if (converted == false)
                        return;
                    Book book = db.Books.Find(id);
                    FormEditBook ordForm = new FormEditBook();
                    ordForm.textBoxTitle.Text = book.Title;
                    ordForm.textBoxAuthor.Text = book.Author;
                    ordForm.textBoxYear.Text = book.Year.ToString();
                    ordForm.textBoxText.Text = book.Text;

                    DialogResult result = ordForm.ShowDialog(this);
                    if (result == DialogResult.Cancel)
                        return;
                    book.Title = ordForm.textBoxTitle.Text;
                    book.Author = ordForm.textBoxAuthor.Text;
                    book.Year = Convert.ToInt32(ordForm.textBoxYear.Text);
                    book.Text = ordForm.textBoxText.Text;
                    db.SaveChanges();
                    db.Books.Load();
                    dataGridView1.DataSource = db.Books.Local.ToBindingList();
                    dataGridView1.Refresh();
                    MessageBox.Show("DB is refreshed");
                }
                catch
                {
                    MessageBox.Show("Сhoose row");
                }
            }
        }

        private void buttonHide_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
