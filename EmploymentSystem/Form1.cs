using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace EmploymentSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-4KJLAHE;Initial Catalog=EmploymentSystem;Integrated Security=True");
        DataSet dataSet = new DataSet();
   
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("Lütfen tüm kutuları doldurunuz !");
            }
            else
            {
            string sql = "INSERT INTO Users VALUES (@Name,@Surname,@Email,@Password,@PhoneNumber)";

            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataAdapter adapter = new SqlDataAdapter();          
            adapter.SelectCommand = command;

            command.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = textBox1.Text;
            command.Parameters.Add("@Surname", SqlDbType.VarChar, 50).Value = textBox2.Text;
            command.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = textBox3.Text;
            command.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = textBox4.Text;
            command.Parameters.Add("@PhoneNumber", SqlDbType.Char,11).Value = maskedTextBox1.Text;

            adapter.Fill(dataSet,"Users");
            MessageBox.Show("Kayıt Başarılı !");
            tabControl1.SelectedIndex = 1;     

            }
              
        }


        private void button4_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand();
            connection.Open();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_GETUsers";
            SqlDataReader reader = command.ExecuteReader();       
            bool control = false;

            if (textBox13.Text == "admin" && textBox14.Text == "admin")
            {
                Admin admin = new Admin();
                admin.Show();
                this.Hide();
            }
            else
            {
                while (reader.Read())
                {
                    control = userToCheck(reader["Email"].ToString(), reader["Password"].ToString());
                    if (control)
                    {
                        break;
                    }
                }

                if (control)
                {
                    MessageBox.Show("Giriş Başarılı");
                    User form2 = new User(textBox13.Text);
                    form2.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Bilgilerinizde hata var ! Lütfen kontrol ediniz !");
                }
                connection.Close();
            }
        }

        private bool userToCheck(string email,string password)
        {
            if(textBox13.Text != email)
            {
                return false;
            }

            if (textBox14.Text != password)
            {
                return false;
            }
            return true;
        }
    }
}
