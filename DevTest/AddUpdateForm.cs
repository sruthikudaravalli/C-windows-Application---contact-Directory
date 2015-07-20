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
    public partial class AddUpdateForm : Form
    {
     

        public AddUpdateForm()
        {
            InitializeComponent();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
            UserLogin l = new UserLogin();
            l.Show();


        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {


            UserLogin f = new UserLogin();
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
               try
   {
            
            string MyConnection = "datasource=localhost;port=3306;username=root;password=root";

            string Query = "insert into database.users(Username, Password, Name, Email) values('" + this.textBox2.Text + "','" + this.textBox3.Text + "', '" + this.textBox1.Text + "','" + this.textBox4.Text + "')";
           
            MySqlConnection MyConn = new MySqlConnection(MyConnection);
           
            MySqlCommand MyCommand = new MySqlCommand(Query, MyConn);
            MySqlDataReader MyReader;
            MyConn.Open();
            MyReader = MyCommand.ExecuteReader();
              MessageBox.Show("Registered successfully");
            this.Hide();
            MyReader.Close();
            MySqlDataReader MyReader2;


            string query2 = "Select * from database.users where Username='" + textBox2.Text + "'and Password='" + textBox3.Text + "';";
            MySqlCommand MyCommand2 = new MySqlCommand(query2, MyConn);
                   MyReader2 = MyCommand2.ExecuteReader();
                   int count = 0;
            while (MyReader2.Read())
            {
                count = count + 1;   
            }
                if (count==1)
                {

                    MainForm m=new MainForm(MyReader2.GetString("Name"));
                    m.Show();

                }

        }
        catch (Exception ex)
        { 
            MessageBox.Show(ex.Message);
      }
}

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void CreateAccount_Load(object sender, EventArgs e)
        {
            this.AcceptButton = button1;
            this.CancelButton = button2;
            label5.Hide();
        }

        private void ValidateTextboxes(object sender, EventArgs e)
        {

            TextBox tb = (TextBox)sender;
            if (tb.Text.Length == 0)
            {
                tb.BackColor = Color.PowderBlue;
                label5.Show();
            }
            else
                tb.BackColor = System.Drawing.SystemColors.Window;
            ValidateOK();
        }
        private void ValidateOK()
        {
            button1.Enabled = (textBox1.BackColor != Color.PowderBlue && textBox2.BackColor != Color.PowderBlue && textBox3.BackColor != Color.PowderBlue && textBox3.BackColor != Color.PowderBlue);
            this.AcceptButton = button1;
        }
        

        private void ValidateTextbox4(object sender, EventArgs e)
        {

            TextBox tb = (TextBox)sender;
            if (tb.Text.Length == 0)
            {
                tb.BackColor = Color.PowderBlue;
                label5.Show();
            }
            else
                tb.BackColor = System.Drawing.SystemColors.Window;
            ValidateOK4();
        }
        private void ValidateOK4()
        {
            button1.Enabled = (textBox1.BackColor != Color.PowderBlue && textBox2.BackColor != Color.PowderBlue && textBox3.BackColor != Color.PowderBlue && textBox4.BackColor != Color.PowderBlue);
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        
            
        }
    }

