using LB5_1._Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LB5_1
{
    public partial class FormAdminPanel : Form
    {
        DbContext db;
        public FormAdminPanel()
        {
            using (DataContext db = new DataContext())
            {
                InitializeComponent();
                db.Users.Load();
                dataGridView1.DataSource = db.Users.Local.ToBindingList();
            }
        }

        private void FormAdminPanel_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ConnectionStrings;Integrated Security=True");
            SqlCommand command = new SqlCommand("SELECT * FROM Users", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            connection.Close();
            dataGridView1.DataSource = dataTable;
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
        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var db = new DataContext())
            {
                try
                {
                    FormAdminPanelCRUD ordForm = new FormAdminPanelCRUD();
                    DialogResult result = ordForm.ShowDialog(this);
                    if (result == DialogResult.Cancel)
                        return;
                    User user = new User();
                    string Password = ordForm.textBoxPas.Text;
                    user.Login = ordForm.textBoxLog.Text;
                    user.Password = ordForm.GetHashString(Password);
                    user.Email = ordForm.textBoxEmail.Text;
                    user.Role = ordForm.comboBoxRole.SelectedItem.ToString();
                    db.Users.Add(user);
                    db.SaveChanges();
                    db.Users.Load();
                    dataGridView1.DataSource = db.Users.Local.ToBindingList();
                    MessageBox.Show("Record added");
                }
                catch
                {
                    MessageBox.Show("Check the data");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
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
                        User user = db.Users.Find(id);
                        db.Users.Remove(user);
                        db.SaveChanges();
                        db.Users.Load();
                        dataGridView1.DataSource = db.Users.Local.ToBindingList();
                        MessageBox.Show("Deleted");
                    }
                }
                catch
                {
                    MessageBox.Show("Error");
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
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
                    User user = db.Users.Find(id);
                    FormAdminPanelCRUD ordForm = new FormAdminPanelCRUD();
                    ordForm.textBoxLog.Text = user.Login;
                    ordForm.textBoxPas.Text = user.Password.ToString();
                    ordForm.textBoxEmail.Text = user.Email.ToString();

                    DialogResult result = ordForm.ShowDialog(this);
                    if (result == DialogResult.Cancel)
                        return;
                    string Password = ordForm.textBoxPas.Text;
                    user.Login = ordForm.textBoxLog.Text;
                    user.Password = ordForm.GetHashString(Password);
                    user.Email = ordForm.textBoxEmail.Text;
                    user.Role = ordForm.comboBoxRole.SelectedItem.ToString();
                    db.SaveChanges();
                    db.Users.Load();
                    dataGridView1.DataSource = db.Users.Local.ToBindingList();
                    dataGridView1.Refresh();
                    MessageBox.Show("DB is refreshed");
                }
                catch
                {
                    MessageBox.Show("Сhoose row");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
