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
using System.Collections;
using System.IO;

namespace EmploymentSystem
{
    public partial class JobAdverts : Form
    {
        string _email;
        public JobAdverts(string email)
        {
            _email = email;
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-4KJLAHE;Initial Catalog=EmploymentSystem;Integrated Security=True");

        // ARAŞTIRMA KONUM -- SaveFileDialog --> Kullanıcı başvurduğu ilanları kaydet tuşuna bastıktan sonra 
        // bilgisayarına kaydedebilecek. Bilgisayarda dosya üzerinde herhangi bir işlem için veya başvurduğu ilanları 
        // bilgisayarına kaydedip çıktı almak üzere kullanılabilecek bir özellik.
        private void kaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
  
            saveFileDialog1.Title = "İlanları Kaydet"; //Dosya kaydetme diyaloğunun başlığı
            saveFileDialog1.DefaultExt = "xlsx"; // dosyayı default olarak hangi uzantıda kaydedileceği
            saveFileDialog1.CreatePrompt = true; // dosya oluşturma özelliği 
            saveFileDialog1.Filter = "xls Dosyaları (*.xls)|*.xls|Tüm Dosyalar(*.*)|*.*"; //dosyaya izin verilen uzantılar
                                                                                          //
            //Not :Bütün uzantılara izin verdiğim için
            //dosyayı excel olarak kaydetmeye gerek yok txt uzantılı kaydedincede çalışmaktadır.

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamWriter Kayit = new StreamWriter(saveFileDialog1.FileName);

                //Excelde gözükmesi için kolonları writeline ile excel içine yazdık.
                Kayit.WriteLine(dataGridView1.Columns[0].Name +"\t \t "+ dataGridView1.Columns[1].Name +
                 "\t \t" + dataGridView1.Columns[2].Name + "\t \t" + dataGridView1.Columns[3].Name + "\t \t" +
                    dataGridView1.Columns[4].Name);

                //Excelde gözükecek satırları excel'e yazdırıyoruz.
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        Kayit.Write(cell.Value +"\t");
                    }
                    Kayit.WriteLine();
                }

                Kayit.Close();
            }

        }

        private void JobAdverts_Load(object sender, EventArgs e)
        {
            ArrayList list = new ArrayList();

            connection.Open();
            string selectSql = "SELECT * FROM JobApplications WHERE UserEmail ='"+_email+"'";       
            SqlCommand command = new SqlCommand(selectSql, connection);
            command.Connection = connection;
        
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(reader["JobAdvertID"]);
            }

            connection.Close();
            getJobAdverts(list);

        }

        private void getJobAdverts(ArrayList list)
        {

            foreach (var item in list)
            {
                connection.Open();
                string sql = "SELECT * FROM JobAdverts WHERE Id=" + item;
                      
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                DataRow row = dataTable.Rows[0];

                dataGridView1.Rows.Add(row["Id"].ToString(), row["Title"].ToString(),
                    row["Description"].ToString(), row["Deadline"].ToString(), row["City"].ToString());
                connection.Close();
            }      
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection.Open();
            string sql = "DELETE FROM JobApplications WHERE JobAdvertID = @JobAdvertID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.Add("@JobAdvertID", SqlDbType.Int).Value = textBox1.Text;
            command.ExecuteNonQuery();
            connection.Close();
            dataGridView1.Rows.Clear();
            JobAdverts_Load(sender, e);
            MessageBox.Show("İlan Silindi !");

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int satirno;
            satirno = e.RowIndex;
            textBox1.Text = dataGridView1.Rows[satirno].Cells[0].Value.ToString();
        }

        private void dosyaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
