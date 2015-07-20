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
    public partial class AddNewContact : Form
    {
        public static int Contact_ID = 0;
        public AddNewContact(int ID)
        {
            InitializeComponent();
            Contact_ID = ID;
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void AddNewContact_Load(object sender, EventArgs e)
        {
            if (Contact_ID != 0)
            {
                string MyConnection = "datasource=localhost;port=3306;username=root;password=root";
                MySqlConnection conn = new MySqlConnection(MyConnection);

                MySqlDataAdapter da = new MySqlDataAdapter("select CI.Contact_ID, CI.FirstName,CI.LastName,CI.Address1,CI.Address2,CI.city,CI.state,CI.zip,CI.PhoneNumber,PI.NPI_Number, PI.DEA_Number, NULL AS Employee_ID from `database`.contact_info CI INNER JOIN `database`.provider_info PI on CI.contact_ID = PI.contact_ID where CI.Contact_ID=" + Contact_ID + " union all select CI.Contact_ID, CI.FirstName,CI.LastName,CI.Address1,CI.Address2,CI.city,CI.state,CI.zip,CI.PhoneNumber,NULL,NULL, SI.Employee_ID from `database`.contact_info CI INNER JOIN `database`.staff_info SI on CI.contact_ID = SI.contact_ID where CI.Contact_ID=" + Contact_ID + "", conn);

                DataTable dt = new System.Data.DataTable();
                da.Fill(dt);

                //Firstname
                this.textBox1.Text = dt.Rows[0]["FirstName"].ToString();
                // Lastname
                this.textBox2.Text = dt.Rows[0]["LastName"].ToString();
                //Address1
                textBox3.Text = dt.Rows[0]["Address1"].ToString();
                //Address2
                textBox4.Text = dt.Rows[0]["Address2"].ToString();
                //city
                textBox5.Text = dt.Rows[0]["city"].ToString();
                //state
                comboBox1.SelectedIndex = comboBox1.FindString(dt.Rows[0]["state"].ToString());
                //zip
                textBox6.Text = dt.Rows[0]["zip"].ToString();
                //phonenumber
                textBox7.Text = dt.Rows[0]["PhoneNumber"].ToString();
                if (dt.Rows[0]["NPI_Number"] != null && dt.Rows[0]["NPI_Number"].ToString() != "")
                {
                    label10.Visible = true;
                    label11.Visible = true;
                    textBox8.Visible = true;
                    textBox9.Visible = true;
                    label10.Text = "NPI Number";

                    comboBox2.SelectedIndex = comboBox2.FindString("Provider");
                    //Npi Number or Employee ID
                    textBox8.Text = dt.Rows[0]["NPI_Number"].ToString();
                    //DEA License
                    textBox9.Text = dt.Rows[0]["DEA_Number"].ToString();
                }
                else if (dt.Rows[0]["Employee_ID"] != null && dt.Rows[0]["Employee_ID"].ToString() != "")
                {
                    label10.Text = "Employee ID";
                    label10.Visible = true;
                    textBox8.Visible = true;
                    label11.Visible = false;
                    textBox9.Visible = false;

                    comboBox2.SelectedIndex = comboBox2.FindString("Staff");
                    //Npi Number or Employee ID
                    textBox8.Text = dt.Rows[0]["Employee_ID"].ToString();
                }
            }
            else
            {
                label12.Hide();
                label10.Hide();
                label11.Hide();
                textBox8.Hide();
                textBox9.Hide();
            }

            this.AcceptButton = button1;
            this.CancelButton = button2;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Contact_ID == 0)
                {

                    string MyConnection = "datasource=localhost;port=3306;username=root;password=root";

                    string Query = "insert into `database`.`contact_info` (`FirstName`, `LastName`, `Address1`, `Address2`, `City`, `State`, `Zip`, `PhoneNumber`) values('" + this.textBox1.Text + "','" + this.textBox2.Text + "', '" + this.textBox3.Text + "','" + this.textBox4.Text + "','" + this.textBox5.Text + "', '" + this.comboBox1.Text + "','" + this.textBox6.Text + "','" + this.textBox7.Text + "');SELECT LAST_INSERT_ID();";

                    MySqlConnection MyConn = new MySqlConnection(MyConnection);

                    MySqlCommand MyCommand = new MySqlCommand(Query, MyConn);
                    MySqlDataReader MyReader;
                    MyConn.Open();
                    MyReader = MyCommand.ExecuteReader();
                    string ContactID = string.Empty;
                    while (MyReader.Read())
                    {
                        ContactID = MyReader[0].ToString();
                    }
                    MyReader.Close();
                    if (comboBox2.SelectedItem.ToString() == "Provider")
                    {
                        string Query1 = "insert into `database`.`provider_info`(`contact_ID`,`NPI_Number`, `DEA_Number`) values('" + ContactID + "','" + this.textBox8.Text + "','" + this.textBox9.Text + "');";
                        MySqlCommand MyCommand1 = new MySqlCommand(Query1, MyConn);
                        MySqlDataReader MyReader1;
                        if (MyConn != null && MyConn.State == ConnectionState.Closed)
                        {
                            MyConn.Open();
                        }
                        MyReader1 = MyCommand1.ExecuteReader();
                        MyReader1.Close();
                    }
                    else if (comboBox2.SelectedItem.ToString() == "Staff")
                    {
                        string Query2 = "insert into `database`.`staff_info`(`contact_ID`,`Employee_ID`) values('" + ContactID + "','" + this.textBox8.Text + "');";
                        MySqlCommand MyCommand2 = new MySqlCommand(Query2, MyConn);
                        MySqlDataReader MyReader2;
                        if (MyConn != null && MyConn.State == ConnectionState.Closed)
                        {
                            MyConn.Open();
                        }
                        MyReader2 = MyCommand2.ExecuteReader();
                        MyReader2.Close();
                    }
                    MessageBox.Show("Saved successfully");
                    this.Hide();
                               
                }
                else
                {
                    string MyConnection = "datasource=localhost;port=3306;username=root;password=root";

                    string Query = "Update `database`.`contact_info` SET `FirstName`='" + textBox1.Text + "', `LastName`='" + textBox2.Text + "', `Address1`= '" + textBox3.Text + "', `Address2` = '" + textBox4.Text + "', `City`='" + textBox5.Text + "', `State`='" + this.comboBox1.Text + "', `Zip`= '" + textBox6.Text + "', `PhoneNumber`=" + textBox7.Text + " where `Contact_ID`=" + Contact_ID + "";

                    MySqlConnection MyConn = new MySqlConnection(MyConnection);

                    MySqlCommand MyCommand = new MySqlCommand(Query, MyConn);
                    MySqlDataReader MyReader;
                    MyConn.Open();
                    MyReader = MyCommand.ExecuteReader();
                    MyReader.Close();
                    if (comboBox2.SelectedItem.ToString() == "Provider")
                    {
                        string Query1 = "Update `database`.`provider_info` SET `contact_ID`=" + Contact_ID + ",`NPI_Number`='" + this.textBox8.Text + "' , `DEA_Number`='" + this.textBox9.Text + "' where contact_ID=" + Contact_ID + " ;";
                        MySqlCommand MyCommand1 = new MySqlCommand(Query1, MyConn);
                        MySqlDataReader MyReader1;
                        if (MyConn != null && MyConn.State == ConnectionState.Closed)
                        {
                            MyConn.Open();
                        }
                        MyReader1 = MyCommand1.ExecuteReader();
                        MyReader1.Close();
                    }
                    else if (comboBox2.SelectedItem.ToString() == "Staff")
                    {
                        string Query2 = "Update `database`.`staff_info`SET `contact_ID`=" + Contact_ID + ",`Employee_ID`='" + this.textBox8.Text + "' where contact_ID=" + Contact_ID + ";";
                        MySqlCommand MyCommand2 = new MySqlCommand(Query2, MyConn);
                        MySqlDataReader MyReader2;
                        if (MyConn != null && MyConn.State == ConnectionState.Closed)
                        {
                            MyConn.Open();
                        }
                        MyReader2 = MyCommand2.ExecuteReader();
                        MyReader2.Close();
                    }
                    MessageBox.Show("Saved successfully");
                    this.Hide();
                }
                MainForm main = new MainForm("");
                main.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Phone_Keypress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '-')
            {

            }
            else
            {
                e.Handled = e.KeyChar != (char)Keys.Back;
            }
        }

        private void Zip_Keypress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '-')
            {

            }
            else
            {
                e.Handled = e.KeyChar != (char)Keys.Back;
            }
        }



        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBox2.SelectedItem.ToString() == "Staff")
                {
                    label10.Text = "Employee ID";
                    label10.Visible = true;
                    textBox8.Visible = true;
                    label11.Visible = false;
                    textBox9.Visible = false;

                }
                else if (comboBox2.SelectedItem.ToString() == "Provider")
                {
                    label10.Visible = true;
                    label11.Visible = true;
                    textBox8.Visible = true;
                    textBox9.Visible = true;

                }
            }
            catch (Exception)
            {
            }
        }

        private void textboxvalidating(object sender, CancelEventArgs e)
        {

            TextBox tb = (TextBox)sender;
            if (tb.Text.Length == 0)
            {
                tb.BackColor = Color.PowderBlue;
                label12.Visible = true;
            }
            else
                tb.BackColor = System.Drawing.SystemColors.Window;

            validate();
        }

        private void validate()
        {
            if (textBox8.Text.Length != 0)
                button1.Enabled = (textBox1.BackColor != Color.PowderBlue && textBox2.BackColor != Color.PowderBlue && textBox3.BackColor != Color.PowderBlue && textBox5.BackColor != Color.PowderBlue && textBox6.BackColor != Color.PowderBlue && textBox7.BackColor != Color.PowderBlue && textBox8.BackColor != Color.PowderBlue);

        }
    }
}
