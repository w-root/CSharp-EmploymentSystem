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
    public partial class User : Form
    {
        string _email;
        public User(string email)
        {
            InitializeComponent();
            _email = email;
        }

        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-4KJLAHE;Initial Catalog=EmploymentSystem;Integrated Security=True");
        DataSet dataSet = new DataSet();

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label9.Text = _email;

            string[] cities ={"Adana", "Adıyaman", "Afyon", "Ağrı", "Amasya", "Ankara", "Antalya", "Artvin", "Aydın", "Balıkesir",
                "Bilecik", "Bingöl", "Bitlis", "Bolu", "Burdur", "Bursa", "Çanakkale",
                "Çankırı", "Çorum", "Denizli", "Diyarbakır", "Edirne", "Elazığ", "Erzincan", 
                "Erzurum", "Eskişehir", "Gaziantep", "Giresun", "Gümüşhane", "Hakkari", "Hatay", "Isparta", 
                "Mersin", "İstanbul", "İzmir", "Kars", "Kastamonu",
                "Kayseri", "Kırklareli", "Kırşehir", "Kocaeli", "Konya", 
                "Kütahya", "Malatya", "Manisa", "Kahramanmaraş", "Mardin", "Muğla",
                "Muş", "Nevşehir", "Niğde", "Ordu", "Rize", "Sakarya", "Samsun", "Siirt", "Sinop", "Sivas", "Tekirdağ",
                "Tokat", "Trabzon", "Tunceli", "Şanlıurfa", "Uşak", "Van", "Yozgat", "Zonguldak", "Aksaray", "Bayburt", 
                "Karaman", "Kırıkkale", "Batman", "Şırnak", "Bartın", "Ardahan", "Iğdır", "Yalova", "Karabük", "Kilis", "Osmaniye", "Düzce"};
            for (int i = 0; i < cities.Length; i++)
            {
                comboBox1.Items.Add(cities[i]);
            }                       
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dataGridView1.ColumnHeadersHeight = 55;

            button1_Click(sender,e);
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int satirno;
            satirno = e.RowIndex;
            textBox1.Text = dataGridView1.Rows[satirno].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[satirno].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[satirno].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[satirno].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.Rows[satirno].Cells[4].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_SEARCHJobAdvert";

            command.Parameters.Add("@pTitle", SqlDbType.VarChar, 30);
            command.Parameters.Add("@pDeadline", SqlDbType.Date);
            command.Parameters.Add("@pCity", SqlDbType.VarChar, 20);

            command.Parameters["@pTitle"].Value = textBox9.Text;
            command.Parameters["@pDeadline"].Value = dateTimePicker1.Text;
            command.Parameters["@pCity"].Value = comboBox1.Text;

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            connection.Open();
            string sql = "INSERT INTO JobApplications VALUES (@JobAdvertID,@UserEmail)";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add("@JobAdvertID", SqlDbType.Int).Value = textBox1.Text;
            command.Parameters.Add("@UserEmail", SqlDbType.VarChar,50).Value = _email;
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("İlan başvurunuz tamamlandı ! Başvurularım butonuyla başvurduğunuz ilanları görebilirsiniz !","Tamamlandı",MessageBoxButtons.OK,MessageBoxIcon.Information);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            JobAdverts adverts = new JobAdverts(_email);
            adverts.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_GETJobAdverts";

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close();
        }
    }
}
