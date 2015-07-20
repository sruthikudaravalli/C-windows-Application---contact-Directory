using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DevTest
{
    public partial class UserLogin : Form
    {
        public UserLogin()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.AcceptButton = button1;
            this.CancelButton = button2;
            label4.Hide();

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {


            string MyConnection = "datasource=localhost;port=3306;username=root;password=root";
            MySqlConnection conn = new MySqlConnection(MyConnection);
            MySqlDataAdapter da = new MySqlDataAdapter("Select * from database.users where Username='" + textBox1.Text + "'and Password='" + textBox2.Text + "'", conn);
            DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                this.Hide();
                MainForm m = new MainForm(dt.Rows[0][0].ToString());
                m.Show();
                ((Form)m).Controls["label5"].Text = dt.Rows[0][0].ToString();
            }
            else
            {
                MessageBox.Show("Check Username/password and Please Try again");
                textBox2.Clear();
                textBox1.Clear();

            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            AddUpdateForm ca = new AddUpdateForm();
            ca.Show();

        }

        private void Textboxesvalidating(object sender, CancelEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text.Length == 0)
            {
                tb.BackColor = Color.PowderBlue;
                label4.Visible = true;
            }
            else
                tb.BackColor = System.Drawing.SystemColors.Window;
         
            validate();
        }

        private void validate()
        {
            if(textBox2.Text.Length!=0)
            button1.Enabled = (textBox1.BackColor != Color.PowderBlue && textBox2.BackColor != Color.PowderBlue);
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }




    }

}