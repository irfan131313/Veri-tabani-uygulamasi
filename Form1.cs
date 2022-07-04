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

namespace eticaretuygulamasi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-3DPH028;Initial Catalog=everitabani;Integrated Security=True");
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();

     

        void yenile()
        {
            
            con = new SqlConnection("Data Source=DESKTOP-3DPH028;Initial Catalog=everitabani;Integrated Security=True");
            da = new SqlDataAdapter("Select *From KullaniciVerileri", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "KullaniciVerileri");
            dataGridView1.DataSource = ds.Tables["KullaniciVerileri"];
            con.Close();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            yenile();
        }

      


       
        
        private void button1_Click(object sender, EventArgs e) // ekleme butonu
        {
          
            // KullaniciVerileri 
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into KullaniciVerileri(id,ad,soyad,adres,sehir,tarihgiris,tarihcikis,email) values (@id,@ad,@soyad,@adres,@sehir,@tarihgiris,@tarihcikis,@email)", con);

            // cmd.CommandText = "insert into KullaniciVerileri(id,ad,soyad,adres,sehir,tarihgiris,tarihcikis,email) values (@Kid,@Kad,@Ksoyad,@Kadres,@Ksehir,@Ktarihgiris,@Ktarihcikis,@Kemail)";

            cmd.Parameters.AddWithValue("@id",textBox1.Text);  
            cmd.Parameters.AddWithValue("@ad", textBox2.Text);
            cmd.Parameters.AddWithValue("@soyad", textBox3.Text);
            cmd.Parameters.AddWithValue("@adres", textBox4.Text);
            cmd.Parameters.AddWithValue("@sehir", textBox5.Text);
            cmd.Parameters.AddWithValue("@tarihgiris", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@tarihcikis", dateTimePicker2.Value);
            cmd.Parameters.AddWithValue("@email", textBox6.Text);


            cmd.ExecuteNonQuery();
            con.Close();
            yenile();
        }

        private void button2_Click(object sender, EventArgs e) // güncelle butonu
        {

            con.Open();
            SqlCommand cmd = new SqlCommand("update KullaniciVerileri set ad=@ad,soyad=@soyad,adres=@adres,sehir=@sehir,tarihgiris=@tarihgiris,tarihcikis=@tarihcikis,email=@email  where id=@id", con);


            cmd.Parameters.AddWithValue("@id", textBox1.Text);
            cmd.Parameters.AddWithValue("@ad", textBox2.Text);
            cmd.Parameters.AddWithValue("@soyad", textBox3.Text);
            cmd.Parameters.AddWithValue("@adres", textBox4.Text);
            cmd.Parameters.AddWithValue("@sehir", textBox5.Text);
            cmd.Parameters.AddWithValue("@tarihgiris", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@tarihcikis", dateTimePicker2.Value);
            cmd.Parameters.AddWithValue("@email", textBox6.Text);
            

            cmd.ExecuteNonQuery();
            con.Close();
            yenile();



        }

        private void button3_Click(object sender, EventArgs e) // sil butonu
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from  KullaniciVerileri where id=@Kid", con);

            cmd.Parameters.AddWithValue("@Kid", textBox1.Text);

     
            cmd.ExecuteNonQuery();
            con.Close();
            yenile();
        }

        private void button4_Click(object sender, EventArgs e) // filtrele butonu
        {


            con.Open();


            SqlCommand cmd = new SqlCommand();

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM KullaniciVerileri WHERE tarihgiris between @giris and @cikis ", con);

            da.SelectCommand.Parameters.Add("@giris", SqlDbType.Date).Value = dateTimePicker1.Value;
            da.SelectCommand.Parameters.Add("@cikis", SqlDbType.Date).Value = dateTimePicker2.Value;

            dataGridView1.DataSource = dt;
            
            dt.Clear();
           
            da.Fill(dt);
            con.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM KullaniciVerileri WHERE ad LIKE '%"+ textBox2.Text +"%' ", con);
            
            //  cmd.Parameters.AddWithValue("@ad", textBox2.Text);

            dataGridView1.DataSource = dt;
            
            dt.Clear();
            da.Fill(dt);
            con.Close();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

       
    }
}
