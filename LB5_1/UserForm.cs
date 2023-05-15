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

namespace LB5_1
{
    public partial class UserForm : Form
    {
        private User currentUser;
        public UserForm(User user)
        {
            InitializeComponent();
            this.currentUser = user;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Authorization form = new Authorization();
            this.Hide();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Registration form = new Registration();
            this.Hide();
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RecoverAcc form = new RecoverAcc();
            this.Hide();
            form.Show();
        }
    }
}
