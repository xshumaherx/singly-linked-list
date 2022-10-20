using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace List
{
    public partial class password : Form
    {
        public password()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "0000")
            {
                label1.Text = "Правильный пароль";
                Admin Admin = new Admin();
                Admin.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Неправильный пароль");
                textBox1.Clear();
                textBox1.Focus();
            }
        }
    }
}
