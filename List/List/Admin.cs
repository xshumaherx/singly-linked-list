using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using System.Reflection;
using List; 
using System.Xml.Serialization;


namespace List
{
    public partial class Admin : Form
    {      
        string ans;
        public Admin()
        {

            InitializeComponent();
        }
        private void richTextBox3_Enter(object sender, EventArgs e)
        {
            if (richTextBox3.Text == "Тут записать вопрос")
            {
                richTextBox3.Text = "";
                richTextBox3.ForeColor = Color.Black;
            }
        }
        private void richTextBox3_Leave(object sender, EventArgs e)
        {
            if (richTextBox3.Text == "")
            {
                richTextBox3.Text = "Тут записать вопрос";
                richTextBox3.ForeColor = Color.Silver;
            }
        }
        private void button11_Click(object sender, EventArgs e)
        {
            if (richTextBox3.Text == "" | richTextBox3.Text == "Тут записать вопрос")
            {
                errorProvider1.SetError(richTextBox3, "Не должны быть пустыми");
                richTextBox3.Focus();
                return;
            }
            if (textBox8.Text == "")
            {
                errorProvider1.SetError(textBox8, "Не должны быть пустыми");
                textBox8.Focus();
                return;
            }
            if (textBox9.Text == "")
            {
                errorProvider1.SetError(textBox9, "Не должны быть пустыми");
                textBox9.Focus();
                return;
            }
            if (textBox10.Text == "")
            {
                errorProvider1.SetError(textBox10, "Не должны быть пустыми");
                textBox10.Focus();
                return;
            }
            if (textBox11.Text == "")
            {
                errorProvider1.SetError(textBox11, "Не должны быть пустыми");
                textBox11.Focus();
                return;
            }
            if (comboBox1.Text == "")
            {
                errorProvider1.SetError(comboBox1, "Не должны быть пустыми");
                comboBox1.Focus();
                return;
            }           
            if (comboBox1.SelectedIndex == 0)
            {
                ans = "OptionA";
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                ans = "OptionB";
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                ans = "OptionC";
            }
            else if (comboBox1.SelectedIndex == 3)
            {
                ans = "OptionD";
            }
            else
            {
                MessageBox.Show("Выберите ответ");
            }
            //Добвление в лист.
            QuestionsList.list.Add(new XmlData(richTextBox3.Text, ans, textBox8.Text, textBox9.Text, textBox10.Text, textBox11.Text));
            //Сериализация
            XmlSerializer formatter = new XmlSerializer(typeof(List<XmlData>));

            using (FileStream fs = new FileStream("ques.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, QuestionsList.list);
            }
            MessageBox.Show("Вопрос добавлен");
            richTextBox3.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox11.Clear();          
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                panel6.Enabled = true;
                panel6.Visible = true;
                panel3.Enabled = false;
                panel3.Visible = false;
                panel8.Enabled = false;
                panel8.Visible = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                panel6.Enabled = false;
                panel6.Visible = false;
                panel8.Enabled = false;
                panel8.Visible = false;
                panel3.Enabled = true;
                panel3.Visible = true;
                panel5.Visible = true;
                var list = new BindingList<XmlData>(QuestionsList.list);
                dataGridView1.DataSource = list;
                radioButton3.Location = new Point(radioButton3.Location.X, radioButton3.Location.Y + 72);
                button1.Location = new Point(button1.Location.X, button1.Location.Y + 72);
            }
            else
            {
                radioButton3.Location = new Point(radioButton3.Location.X, radioButton3.Location.Y - 72);
                button1.Location = new Point(button1.Location.X, button1.Location.Y - 72);
                panel5.Visible = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.Rows.RemoveAt(rowIndex);

            File.Delete("ques.xml");
            XmlSerializer formatter = new XmlSerializer(typeof(List<XmlData>));

            using (FileStream fs = new FileStream("ques.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, QuestionsList.list);
            }
        }
        
        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && (Char.IsNumber(e.KeyChar)) | e.KeyChar == '\b') return;
            else
                errorProvider1.SetError(comboBox1, "Выберите только то что есть в списке");
            e.Handled = true;
        }

        private void Admin_Load(object sender, EventArgs e)
        {
           var list = new BindingList<XmlData>(QuestionsList.list);
            dataGridView1.DataSource = list;
            radioButton3.Location = new Point (3,147);
            button1.Location = new Point(3, 219);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e) //результат студентов
        {
            if (radioButton3.Checked == true)
            {
                if (File.Exists("TestFile.txt"))
                {
                    panel6.Enabled = false;
                    panel6.Visible = false;
                    panel3.Enabled = false;
                    panel3.Visible = false;
                    panel8.Enabled = true;
                    panel8.Visible = true;
                    dataGridView2.Rows.Clear();
                    foreach (var line in File.ReadLines("TestFile.txt"))
                    {
                        var array = line.Split();
                        dataGridView2.Rows.Add(array);
                    }
                }
                else
                {
                    MessageBox.Show("Файла нет или студенты не тестировали");
                }
            }
        }
    }
}
