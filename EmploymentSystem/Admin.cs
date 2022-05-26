using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
namespace EmploymentSystem
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-4KJLAHE;Initial Catalog=EmploymentSystem;Integrated Security=True");
        private void Admin_Load(object sender, EventArgs e)
        {
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            connection.Open();
         
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_GETJobAdverts";

            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand();
            connection.Open();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_ADDJobAdvert";

            command.Parameters.Add("@pTitle", SqlDbType.VarChar, 50);
            command.Parameters.Add("@pDescription", SqlDbType.VarChar, 200);
            command.Parameters.Add("@pDeadline", SqlDbType.Date);
            command.Parameters.Add("@pCity", SqlDbType.VarChar, 20);

            command.Parameters["@pTitle"].Value = textBox1.Text;
            command.Parameters["@pDescription"].Value = textBox2.Text;
            command.Parameters["@pDeadline"].Value = dateTimePicker1.Text;
            command.Parameters["@pCity"].Value = comboBox1.Text;
            command.ExecuteNonQuery();
            MessageBox.Show("İlan eklendi");

            connection.Close();
            button1_Click(sender, e); //tekrar select sorgusunu çağırmamın sebebi sürümden dolayı data set oluşturamadım
                                      // ve  this.ogrenciTableAdapter.Fill(this.okulDataSet.ogrenci) gibi bir kod yazarak böyle bir 
                                      // tabloyu veriyi ekledikten hemen sonra refresh  edemiyordum.o yüzden veriyi ekledikten sonra 
                                      // otomatik olarak güncellesin diye select sorgusunu çağırıyorum
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            dateTimePicker1.Text = "";
            comboBox1.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand();
            connection.Open();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_DELETEJobAdvert";

            command.Parameters.Add("@pId", SqlDbType.Int);
            command.Parameters["@pId"].Value = textBox3.Text;
            command.ExecuteNonQuery();
            MessageBox.Show("İlan Kayıtı Silindi");
            connection.Close();

            button1_Click(sender, e); //tekrar select sorgusunu çağırmamın sebebi sürümden dolayı data set oluşturamadım
                                      // ve  this.ogrenciTableAdapter.Fill(this.okulDataSet.ogrenci) gibi bir kod yazarak böyle bir 
                                      // tabloyu veriyi sildikten hemen sonra refresh  edemiyordum.o yüzden veriyi sildikten sonra 
                                      // otomatik olarak güncellesin diye select sorgusunu çağırıyorum
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            dateTimePicker1.Text = "";
            comboBox1.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int satirno;
            satirno = e.RowIndex;
            textBox3.Text = dataGridView1.Rows[satirno].Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.Rows[satirno].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[satirno].Cells[2].Value.ToString();
            dateTimePicker1.Text = dataGridView1.Rows[satirno].Cells[3].Value.ToString();
            comboBox1.SelectedItem = dataGridView1.Rows[satirno].Cells[4].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand();
            connection.Open();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_UPDATEJobAdvert";

            command.Parameters.Add("@pId", SqlDbType.Int);
            command.Parameters.Add("@pTitle", SqlDbType.VarChar, 50);
            command.Parameters.Add("@pDescription", SqlDbType.VarChar, 200);
            command.Parameters.Add("@pDeadline", SqlDbType.Date);
            command.Parameters.Add("@pCity", SqlDbType.VarChar, 20);

            command.Parameters["@pId"].Value = textBox3.Text;
            command.Parameters["@pTitle"].Value = textBox1.Text;
            command.Parameters["@pDescription"].Value = textBox2.Text;
            command.Parameters["@pDeadline"].Value = dateTimePicker1.Text;
            command.Parameters["@pCity"].Value = comboBox1.Text;

            command.ExecuteNonQuery();
            MessageBox.Show("İlan Kayıtı Güncellendi !");
            connection.Close();

            button1_Click(sender, e); //tekrar select sorgusunu çağırmamın sebebi sürümden dolayı data set oluşturamadım
                                      // ve  this.ogrenciTableAdapter.Fill(this.okulDataSet.ogrenci) gibi bir kod yazarak böyle bir 
                                      // tabloyu veriyi güncelledikten hemen sonra refresh  edemiyordum.o yüzden veriyi güncelledikten sonra 
                                      // otomatik olarak güncellesin diye select sorgusunu çağırıyorum   

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            dateTimePicker1.Text = "";
            comboBox1.Text = "";
        }

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
                Kayit.WriteLine(dataGridView1.Columns[0].Name + "\t"+ "\t" + dataGridView1.Columns[1].Name +
                 "\t"+"\t" + dataGridView1.Columns[2].Name + "\t" +"\t" + dataGridView1.Columns[3].Name + "\t \t" +
                    dataGridView1.Columns[4].Name);

                //Excelde gözükecek satırları excel'e yazdırıyoruz.
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        Kayit.Write(cell.Value + "\t");
                    }
                    Kayit.WriteLine();
                }

                Kayit.Close();
            }
        }
    }
}
