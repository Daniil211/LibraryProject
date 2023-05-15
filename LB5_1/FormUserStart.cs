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
    public partial class FormUserStart : Form
    {
        private User currentUser;
        public FormUserStart(User user)
        {
            InitializeComponent();
            this.currentUser = user;
        }
    }
}
