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
using List; //Добавил namespace

namespace List
{
    public partial class Form1 : Form
    {
        static private int number = 0;
        static private int score = 0;
        static private string userAnswer;
        static private List<XmlData> randomList = new List<XmlData>();
        public Form1()
        {
            InitializeComponent();           
            loadQuestions();
            loadText();
        }
        public void loadQuestions()
        {
            if (File.Exists("ques.xml"))
            {
                var xDoc = XDocument.Load("ques.xml");
                foreach (var item in xDoc.Element("ArrayOfProblem").Elements("Problem"))
                {
                    QuestionsList.list.Add(new XmlData() 
                    {
                        question = item.Element("question").Value,
                        trueAnswer = item.Element("trueAnswer").Value,
                        answer1 = item.Element("answer1").Value,
                        answer2 = item.Element("answer2").Value,
                        answer3 = item.Element("answer3").Value,
                        answer4 = item.Element("answer4").Value
                    });
                }
                Random rnd = new Random();
                randomList = QuestionsList.list.OrderBy(s => rnd.Next()).Take(5).ToList();
            }
            else
            {
                MessageBox.Show("Файл xml не существует");
                Application.Exit();
            }
        }

        private void loadText()
        {               
            richTextBox2.Text = randomList[number].question;
            radioButton1.Text = randomList[number].answer1;
            radioButton2.Text = randomList[number].answer2;
            radioButton3.Text = randomList[number].answer3;
            radioButton4.Text = randomList[number].answer4;
        }

        // Создать односвязный список
        List<LL> link = new List<LL>();
        LL milist = new LL();
        #region*кнопки*
        private void button2_Click(object sender, EventArgs e) //добавить элемент в начало
        {

            if (textBox1.Text == "")
            {
                errorProvider1.SetError(textBox1, "Не должны быть пустыми");
                textBox1.Focus();
                return;
            }
            errorProvider1.SetError(textBox1, "");
            LL milist = new LL();
            milist.listok = textBox1.Text;
            link.Insert(0, milist);
                      dataGridView1.DataSource = null;
                      dataGridView1.DataSource = link.ToList();
                      textBox1.Clear();
                      textBox1.Focus();
        }

        private void button4_Click(object sender, EventArgs e) //добавить элемент по индексу
        {
            if (link.Count == 0)
            {
                errorProvider1.SetError(textBox7, "Список пуст");               
                errorProvider1.SetError(textBox3, "список пуст");
                textBox3.Focus();
                return;
            }
            if (textBox3.Text == "Элемент" | textBox7.Text == "Индекс")
            {
                errorProvider1.SetError(textBox3, "Не должны быть пустыми");
                textBox3.Focus();
                errorProvider1.SetError(textBox7, "Не должны быть пустыми");               
                return;
            }
            int h = Convert.ToInt32(textBox7.Text);
            if (link.Count > 0)
                if (link.Count > h)
                {
                    errorProvider1.SetError(textBox7, "");
                    errorProvider1.SetError(textBox3, "");
                    LL milist = new LL();
                    int n = Convert.ToInt32(textBox7.Text);
                    milist.listok = textBox3.Text;
                    link.Insert(n, milist);
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = link.ToList();
                    textBox7.Clear();
                    textBox7.Focus();
                    textBox3.Clear();
                    textBox3.Focus();
                }else
                {
                    errorProvider1.SetError(textBox3, "За пределами");
                    textBox3.Clear();
                    textBox3.Focus();
                    errorProvider1.SetError(textBox7, "За пределами");
                    textBox7.Clear();
                    textBox7.Focus();
                }
        }

        private void button3_Click(object sender, EventArgs e) //добавить элемента в конец
        {
            if (textBox2.Text == "")
            {
                errorProvider1.SetError(textBox2, "Не должны быть пустыми");
                textBox2.Focus();
                return;
            }
            errorProvider1.SetError(textBox2, "");
            LL milist = new LL();
            milist.listok = textBox2.Text;
            link.Add(milist);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = link.ToList();
            textBox2.Clear();
            textBox2.Focus();

        }

        private void button5_Click(object sender, EventArgs e) //удалить первый элемент
        {
            if (link.Count != 0)
            {
                link.RemoveAt(0);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = link.ToList();
                dataGridView1_CellEnter(null,null);
            }

        }

        private void button6_Click(object sender, EventArgs e) //удалить последний элемент
        {
            if (link.Count != 0)
            {
                link.RemoveAt(link.Count - 1);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = link.ToList();
                dataGridView1_CellEnter(null, null);
            }
        }



        private void button7_Click(object sender, EventArgs e) //удалить элемент по индесу
        {
            if (link.Count == 0)
            {
                errorProvider1.SetError(textBox5, "Список пуст");
                textBox5.Clear();
                textBox5.Focus();
                return;
            }
            if (textBox5.Text == "")
            {
                errorProvider1.SetError(textBox5, "Не должны быть пустыми");
                textBox5.Clear();
                textBox5.Focus();
                return;
            }
            int h = Convert.ToInt32(textBox5.Text);
            if (link.Count > 0)
                if (link.Count > h)
                {
                    errorProvider1.SetError(textBox5, "");
                    LL milist = new LL();
                    int n = Convert.ToInt32(textBox5.Text);
                    link.RemoveAt(n);
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = link.ToList();
                    textBox5.Clear();
                    textBox5.Focus();
                }
                else
                {
                    errorProvider1.SetError(textBox5, "За пределами");
                    textBox5.Clear();
                    textBox5.Focus();
                }
        }

        private void textBox6_TextChanged(object sender, EventArgs e) //Поиск по индексу
        {
            if (link.Count == 0)
            {
                errorProvider1.SetError(textBox6, "Список пуст");
                textBox6.Focus();
                return;
            }
            if (textBox6.Text == "")
            {
                errorProvider1.SetError(textBox6, "Не должны быть пустыми");
                textBox6.Clear();
                textBox6.Focus();
                return;
            }
                int h = Convert.ToInt32(textBox6.Text);
                if (link.Count > 0)
                    if (link.Count > h)
                    {
                        label10.Visible = true;
                        errorProvider1.SetError(textBox6, "");
                        LL milist = new LL();
                        int n = Convert.ToInt32(textBox6.Text);
                        label10.Text = link.ElementAt(n).ToString();
                        textBox6.Focus();
                        return;
                    }
                    else
                    {
                        label10.Visible = false;
                        errorProvider1.SetError(textBox6, "За пределами");
                        textBox6.Focus();
                    }
                return;           
        }
       
            private void button8_Click(object sender, EventArgs e)//Сохранение результата пользователя
        {
            if (textBox4.Text == "")
            {
                MessageBox.Show("Введите фамилию");
            }else
            {
                if (File.Exists("TestFile.txt"))
                {
                    File.AppendAllText("TestFile.txt", "\r\n" + textBox4.Text + " " + score + " " + DateTime.Now.ToString());
                    MessageBox.Show("Данные записаны");
                }
                else
                {
                    StreamWriter file = new StreamWriter("TestFile.txt");
                    file.Write(textBox4.Text + " " + score + " " + DateTime.Now.ToString());
                    file.Close();
                    MessageBox.Show("Данные записаны");
                }
                richTextBox2.Clear();
                textBox4.Clear();
                button8.Enabled = false;
                textBox4.Enabled = false;
            }           
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (link.Count != 0)
            {
                label9.Visible = true;
                label9.Text = link.Count.ToString();
            }
            else
                label9.Text = "0";
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Элемент")
            {
                textBox3.Text = "";
                textBox3.ForeColor = Color.Black;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "Элемент";
                textBox3.ForeColor = Color.Silver;
            }
        }

        private void textBox7_Enter(object sender, EventArgs e)
        {
            if (textBox7.Text == "Индекс")
            {
                textBox7.Text = "";
                textBox7.ForeColor = Color.Black;
            }
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            if (textBox7.Text == "")
            {
                textBox7.Text = "Индекс";
                textBox7.ForeColor = Color.Silver;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
   
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) | e.KeyChar == '\b') return;
            else
                errorProvider1.SetError(textBox7, "Введите только число");
            e.Handled = true;
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) | e.KeyChar == '\b') return;
            else
                errorProvider1.SetError(textBox5, "Введите только число");
            e.Handled = true;
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) | e.KeyChar == '\b') return;
            else
                errorProvider1.SetError(textBox6, "Введите только число");
            e.Handled = true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) | e.KeyChar == '\b') return;
            else
                errorProvider1.SetError(textBox1, "Введите только число");
            e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) | e.KeyChar == '\b') return;
            else
                errorProvider1.SetError(textBox2, "Введите только число");
            e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) | e.KeyChar == '\b') return;
            else
                errorProvider1.SetError(textBox3, "Введите только число");
            e.Handled = true;
        }
        #endregion*qqq*
        private void button9_Click(object sender, EventArgs e)//Выбор ответа
        {           
            userAnswer = "";
        if (radioButton1.Checked == true)
        {
            userAnswer = "OptionA";
        }
        else if (radioButton2.Checked == true)
        {
            userAnswer = "OptionB";
        }
        else if (radioButton3.Checked == true)
        {
            userAnswer = "OptionC";
        }
        else if (radioButton4.Checked == true)
        {
            userAnswer = "OptionD";
        }
        else
        {
            MessageBox.Show("Выберите ответ");
        }
        if (userAnswer != "")
        {
            if (randomList[number].trueAnswer == userAnswer)
            {
                score += 1;//добавляем верный ответ
                number += 1;//добавляем вопросы
            }
            else
            {
                number += 1;//добавляем вопросы
            }

            if (randomList.Count == number)
            {
                MessageBox.Show("Тестирование окончено. Вы набрали " + score + " правильных ответа из 5"+" \nВведите фамилию для сохранения результата или начните сначала");
                button9.Enabled = false;
                button10.Visible = true;
                button8.Enabled = true;
                textBox4.Enabled = true;
            }
            else
            {
                loadText();
            }
        }
    }

        private void button10_Click(object sender, EventArgs e)//начать сначала тестирования
        {
            QuestionsList.list.Clear();
            score = 0;
            number = 0;
            loadQuestions();
            loadText();
            button9.Enabled = true;
            button8.Enabled = false;
            textBox4.Enabled = false;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) | e.KeyChar == '\b' && e.KeyChar != (int)Keys.Space | e.KeyChar == '\0') return;
            else
                errorProvider1.SetError(textBox4, "Введите фамилию без пробела");
            e.Handled = true;
        }

        private void администраторToolStripMenuItem_Click(object sender, EventArgs e)
        {
            password password = new password();
            password.Show();          
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            if (File.Exists("Теория о олс.rtf"))
            {
                richTextBox1.LoadFile("Теория о олс.rtf");
                richTextBox1.Find("Text", RichTextBoxFinds.MatchCase);
                richTextBox1.SelectionFont = new Font("Verdana", 12, FontStyle.Bold);
            }
            else MessageBox.Show("Файл, содержащий теории, не найден");
        }
    }
}
