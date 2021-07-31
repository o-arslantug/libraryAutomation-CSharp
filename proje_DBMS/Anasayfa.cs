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
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Runtime.InteropServices.ComTypes;

namespace proje_DBMS
{
    public partial class Anasayfa : Form
    {
        MySqlConnection con;
        MySqlDataAdapter adtr;
        DataSet dtst = new DataSet();
        string sql;
        public String anaSeviye;
        public static string seviye;

        public Anasayfa()
        {
            InitializeComponent();
            Kontrol();
            sql = "select kitap.kitap_id, kitap.kitap_ad, kitap.kitap_isbn, yazar.yazar_ad, kitap.kitap_kategori, kitap.kitap_basimYili, cevirmen.cevirmen_ad, yayinevi.yayinevi_ad, kitap.kitap_konum, kitap.kitap_mevcutSayi from kitap inner join yazar on kitap.kitap_yazarId = yazar_id inner join cevirmen on kitap.kitap_cevirmenid = cevirmen.cevirmen_id inner join yayinevi on kitap.kitap_yayineviid = yayinevi.yayinevi_id";
            con = new MySqlConnection("Server=localhost;Database=proje_DBMS;Uid=root;Pwd='151906007';");
            adtr = new MySqlDataAdapter(sql, con);      
        }

        void fillGridView()
        {
            try
            {
                con.Open();
                adtr.Fill(dtst, "Kitaplar");
                dataGridView1.DataSource = dtst.Tables["Kitaplar"];
                dataGridView1.Columns[0].HeaderText = "Kitap ID";
                dataGridView1.Columns[1].HeaderText = "Kitap Adı";
                dataGridView1.Columns[2].HeaderText = "ISBN No";
                dataGridView1.Columns[3].HeaderText = "Yazar";
                dataGridView1.Columns[4].HeaderText = "Kategori";
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[9].Visible = false;
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Database ile bağlantı kurulamadı!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void Arama()
        {
            comboBox1.Items.Add("Ad");
            comboBox1.Items.Add("ISBN");
            comboBox1.Items.Add("Yazar");
            comboBox1.Items.Add("Kategori");
        }

        private void Kontrol()
        {
            if (anaSeviye == "Çalışan")
            {    
                button4.Enabled = false;
            }
        }

        private void TabloYenile()
        {
            dtst.Clear();
            sql = "select kitap.kitap_id, kitap.kitap_ad, kitap.kitap_isbn, yazar.yazar_ad, kitap.kitap_kategori, kitap.kitap_basimYili, cevirmen.cevirmen_ad, yayinevi.yayinevi_ad, kitap.kitap_konum, kitap.kitap_mevcutSayi from kitap inner join yazar on kitap.kitap_yazarId = yazar_id inner join cevirmen on kitap.kitap_cevirmenid = cevirmen.cevirmen_id inner join yayinevi on kitap.kitap_yayineviid = yayinevi.yayinevi_id";
            adtr = new MySqlDataAdapter(sql, con);
            fillGridView();
        }

        private void Anasayfa_Load(object sender, EventArgs e)
        {
            Kontrol();
            Arama();
            fillGridView();
        }

        private void AramaYap()
        {
            dtst.Clear();
            if (comboBox1.SelectedItem == "Ad")
            {
                sql = "select kitap.kitap_id, kitap.kitap_ad, kitap.kitap_isbn, yazar.yazar_ad, kitap.kitap_kategori, kitap.kitap_basimYili, kitap.kitap_cevirmenId, kitap.kitap_yayinEviId, kitap.kitap_konum, kitap.kitap_mevcutSayi from kitap inner join yazar on kitap.kitap_yazarId=yazar_id where kitap.kitap_ad like'%" + textBox1.Text + "%'";
            }
            else if (comboBox1.SelectedItem == "ISBN")
            {
                sql = "select kitap.kitap_id, kitap.kitap_ad, kitap.kitap_isbn, yazar.yazar_ad, kitap.kitap_kategori, kitap.kitap_basimYili, kitap.kitap_cevirmenId, kitap.kitap_yayinEviId, kitap.kitap_konum, kitap.kitap_mevcutSayi from kitap inner join yazar on kitap.kitap_yazarId=yazar_id where kitap.kitap_isbn like'%" + textBox1.Text + "%'";
            }

            else if (comboBox1.SelectedItem == "Yazar")
            {
                sql = "select kitap.kitap_id, kitap.kitap_ad, kitap.kitap_isbn, yazar.yazar_ad, kitap.kitap_kategori, kitap.kitap_basimYili, kitap.kitap_cevirmenId, kitap.kitap_yayinEviId, kitap.kitap_konum, kitap.kitap_mevcutSayi from kitap inner join yazar on kitap.kitap_yazarId=yazar_id where yazar.yazar_ad like'%" + textBox1.Text + "%'";
            }
            else if (comboBox1.SelectedItem == "Kategori")
            {
                sql = "select kitap.kitap_id, kitap.kitap_ad, kitap.kitap_isbn, yazar.yazar_ad, kitap.kitap_kategori, kitap.kitap_basimYili, kitap.kitap_cevirmenId, kitap.kitap_yayinEviId, kitap.kitap_konum, kitap.kitap_mevcutSayi from kitap inner join yazar on kitap.kitap_yazarId=yazar_id where kitap.kitap_kategori like'%" + textBox1.Text + "%'";
            }
            else
            {
                MessageBox.Show("Arama kriteri seçiniz!", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            try
            {
                adtr = new MySqlDataAdapter(sql, con);
                con.Open();
                adtr.Fill(dtst, "Kitaplar");
                con.Close();
            }
            catch (Exception e)
            {
                TabloYenile();
                MessageBox.Show("Arama sırasında hata oluştu!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                textBox11.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                textBox8.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                textBox9.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                textBox10.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
            }
        }

        private void Anasayfa_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Odunc Odunc = new Odunc();
            Odunc.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            seviye = anaSeviye;
            Envanter Envanter = new Envanter();
            Envanter.Show();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            seviye = anaSeviye;
            Uyelik Uyelik = new Uyelik();
            Uyelik.Show();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Yonetim Yonetim = new Yonetim();
            Yonetim.Show();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            TabloYenile();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            AramaYap();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            YayineviYazar yayineviYazar = new YayineviYazar();
            yayineviYazar.Show();
        }
    }
}
