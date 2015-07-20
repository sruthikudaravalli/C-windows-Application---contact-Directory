using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using testexListBox;
using MySql.Data.MySqlClient;

namespace DevTest
{
    public partial class MainForm : Form
    {

        public MainForm(string Name)
        {
            InitializeComponent();
            label5.Text = Name;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            FillControls();
        }

        public void FillControls()
        {
            try
            {
                listBox1.Items.Clear();

                label4.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
                label9.Visible = false;
                label10.Visible = false;

                label11.Visible = false;
                label12.Visible = false;
                label13.Visible = false;
                label14.Visible = false;
                label15.Visible = false;
                label16.Visible = false;
                label17.Visible = false;

                string MyConnection = "datasource=localhost;port=3306;username=root;password=root";
                MySqlConnection conn = new MySqlConnection(MyConnection);
                MySqlDataAdapter da = new MySqlDataAdapter("select CI.Contact_ID,CI.FirstName,CI.LastName,CI.PhoneNumber,PI.NPI_Number, PI.DEA_Number,NULL AS Employee_ID from `database`.contact_info CI INNER JOIN `database`.provider_info PI on CI.contact_ID = PI.contact_ID union all select CI.Contact_ID,CI.FirstName,CI.LastName,CI.PhoneNumber,NULL,NULL,SI.Employee_ID from `database`.contact_info CI INNER JOIN `database`.staff_info SI on CI.contact_ID = SI.contact_ID", conn);
                DataTable dt = new System.Data.DataTable();
                da.Fill(dt);

                for (int row = 0; row < dt.Rows.Count; row++)
                {
                    if (dt.Rows[row]["NPI_Number"] != null && dt.Rows[row]["NPI_Number"].ToString() != "")
                    {
                        listBox1.Items.Add(new exListBoxItem(Convert.ToInt32(dt.Rows[row]["Contact_ID"]), "" + dt.Rows[row]["FirstName"].ToString() + ", " + dt.Rows[row]["LastName"].ToString() + "      " + dt.Rows[row]["PhoneNumber"].ToString() + "", "Provider      NPI Number:" + dt.Rows[row]["NPI_Number"].ToString() + ""));
                    }
                    else if (dt.Rows[row]["Employee_ID"] != null && dt.Rows[row]["Employee_ID"].ToString() != "")
                    {
                        listBox1.Items.Add(new exListBoxItem(Convert.ToInt32(dt.Rows[row]["Contact_ID"]), "" + dt.Rows[row]["FirstName"].ToString() + ", " + dt.Rows[row]["LastName"].ToString() + "      " + dt.Rows[row]["PhoneNumber"].ToString() + "", "Staff     Employee ID: " + dt.Rows[row]["Employee_ID"].ToString() + " "));
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.Items.Count >= 1)
                {
                    label10.Visible = true;
                    label4.Visible = true;
                    label6.Visible = true;
                    label7.Visible = true;

                    string MyConnection = "datasource=localhost;port=3306;username=root;password=root";
                    MySqlConnection conn = new MySqlConnection(MyConnection);
                    this.Text = ((exListBoxItem)listBox1.SelectedItem).Id.ToString();
                    int ID = Convert.ToInt32(this.Text);

                    MySqlDataAdapter da = new MySqlDataAdapter("select CI.Contact_ID, CI.FirstName,CI.LastName,CI.PhoneNumber,PI.NPI_Number, PI.DEA_Number,CI.Address1, CI.Address2, NULL AS Employee_ID from `database`.contact_info CI INNER JOIN `database`.provider_info PI on CI.contact_ID = PI.contact_ID where CI.Contact_ID=" + ID + " union all select CI.Contact_ID,CI.FirstName,CI.LastName,CI.PhoneNumber,CI.Address1,CI.Address2,NULL,NULL, SI.Employee_ID from `database`.contact_info CI INNER JOIN `database`.staff_info SI on CI.contact_ID = SI.contact_ID where CI.Contact_ID=" + ID + "", conn);

                    DataTable dt = new System.Data.DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        if (dt.Rows[0]["NPI_Number"] != null && dt.Rows[0]["NPI_Number"].ToString() != "")
                        {


                            label11.Visible = true;
                            label14.Visible = true;
                            label15.Visible = true;
                            label16.Visible = true;
                            label12.Visible = true;
                            label13.Visible = true;
                            label8.Visible = true;
                            label9.Visible = true;
                            label17.Visible = true;
                            label11.Text = dt.Rows[0]["FirstName"].ToString();
                            label17.Text = dt.Rows[0]["LastName"].ToString();
                            label12.Text = dt.Rows[0]["Address1"].ToString();
                            label13.Text = dt.Rows[0]["Address2"].ToString();
                            label14.Text = dt.Rows[0]["PhoneNumber"].ToString();
                            label15.Text = dt.Rows[0]["NPI_Number"].ToString();
                            label16.Text = dt.Rows[0]["DEA_Number"].ToString();
                        }
                        else if (dt.Rows[0]["Employee_ID"] != null && dt.Rows[0]["Employee_ID"].ToString() != "")
                        {

                            label11.Visible = true;
                            label14.Visible = true;
                            label15.Visible = true;
                            label16.Visible = false;
                            label12.Visible = true;
                            label13.Visible = true;
                            label8.Visible = true;
                            label9.Visible = false;
                            label17.Visible = true;
                            label8.Text = "Employee ID";
                            label11.Text = dt.Rows[0]["FirstName"].ToString();
                            label17.Text = dt.Rows[0]["LastName"].ToString();
                            label12.Text = dt.Rows[0]["Address1"].ToString();
                            label13.Text = dt.Rows[0]["Address2"].ToString();
                            label14.Text = dt.Rows[0]["PhoneNumber"].ToString();
                            label15.Text = dt.Rows[0]["Employee_ID"].ToString();

                        }
                    }
                }
                else
                {
                    label10.Visible = false;
                    label4.Visible = false;
                    label6.Visible = false;
                    label7.Visible = false;
                    label11.Visible = false;
                    label14.Visible = false;
                    label15.Visible = false;
                    label16.Visible = false;
                    label12.Visible = false;
                    label13.Visible = false;
                    label8.Visible = false;
                    label9.Visible = false;
                    label17.Visible = false;
                    label11.Text = "";
                    label17.Text = "";
                    label12.Text = "";
                    label13.Text = "";
                    label14.Text = "";
                    label15.Text = "";
                }
            }
            catch (Exception)
            {
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddNewContact add = new AddNewContact(0);
           
            add.Show();
        }

        private void add_FormClosed(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Text = ((exListBoxItem)listBox1.SelectedItem).Id.ToString();
            int ID = int.Parse(this.Text);

            AddNewContact Edit = new AddNewContact(ID);
            Edit.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddUpdateForm up = new AddUpdateForm();
            up.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string MyConnection = "datasource=localhost;port=3306;username=root;password=root";
            MySqlConnection conn = new MySqlConnection(MyConnection);
            conn.Close();
            this.Close();
            UserLogin l = new UserLogin();
            l.Show();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string MyConnection = "datasource=localhost;port=3306;username=root;password=root";
                MySqlConnection conn = new MySqlConnection(MyConnection);
                conn.Open();
                this.Text = ((exListBoxItem)listBox1.SelectedItem).Id.ToString();
                int ID = int.Parse(this.Text);
                if (label8.Text == "NPI NUMBER")
                {
                    string query1 = "delete from database.provider_info where contact_ID=" + ID + ";";
                    MySqlCommand Mycommand1 = new MySqlCommand(query1, conn);
                    Mycommand1.ExecuteNonQuery();
                    string query2 = "delete from database.contact_info where contact_ID=" + ID + ";";
                    MySqlCommand Mycommand2 = new MySqlCommand(query2, conn);
                    Mycommand1.ExecuteNonQuery();
                    MessageBox.Show("deleted contact");
                }
                else if (label8.Text == "Employee ID")
                {
                    string query3 = "delete from database.staff_info where contact_ID=" + ID + ";";
                    MySqlCommand Mycommand3 = new MySqlCommand(query3, conn);
                    Mycommand3.ExecuteNonQuery();
                    string query4 = "delete from database.contact_info where contact_ID=" + ID + ";";
                    MySqlCommand Mycommand4 = new MySqlCommand(query4, conn);
                    Mycommand4.ExecuteNonQuery();
                    MessageBox.Show("deleted contact");
                }
                FillControls();
            }
            catch (Exception)
            { 
            }
        }
    }
}
