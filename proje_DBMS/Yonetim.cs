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
using System.Security.Cryptography.X509Certificates;

namespace proje_DBMS
{
    public partial class Yonetim : Form
    {
        public string Seviye;
        MySqlConnection con;
        MySqlDataAdapter adtr;
        DataSet dtst = new DataSet();
        string sql;
        public Yonetim()
        {
            InitializeComponent();
            Kontrol();
            sql = "select * from personel";
            con = new MySqlConnection("Server=localhost;Database=proje_DBMS;Uid=root;Pwd='151906007';");
            adtr = new MySqlDataAdapter(sql, con);
        }

        private void Kontrol()
        {
            if (Seviye == "Çalışan")
            {
                this.Hide();
            }
        }

        void fillGridView()
        {
            try
            {
                con.Open();
                adtr.Fill(dtst, "Personeller");
                dataGridView1.DataSource = dtst.Tables["Personeller"];
                dataGridView1.Columns[0].HeaderText = "Personel ID";
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[2].HeaderText = "Personel Adı";
                dataGridView1.Columns[3].HeaderText = "Yetki";
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Database ile bağlantı kurulamadı!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Arama()
        {
            comboBox2.Items.Add("ID");
            comboBox2.Items.Add("Ad-Soyad");
            comboBox2.Items.Add("Yetki");
            comboBox1.Items.Add("Çalışan");
            comboBox1.Items.Add("Yönetici");
        }

        private void AramaYap()
        {
            dtst.Clear();
            if (comboBox2.SelectedItem == "ID")
            {
                sql = "select * from personel where personel_id like'%" + textBox4.Text + "%'";
            }
            else if (comboBox2.SelectedItem == "Ad-Soyad")
            {
                sql = "select * from personel where personel_ad like'%" + textBox4.Text + "%'";
            }
            else if (comboBox2.SelectedItem == "Yetki")
            {
                sql = "select * from personel where personel_yetki like'%" + textBox4.Text + "%'";
            }
            else
            {
                MessageBox.Show("Arama kriteri seçiniz!", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            try
            {
                adtr = new MySqlDataAdapter(sql, con);
                con.Open();
                adtr.Fill(dtst, "Personeller");
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Arama sırasında hata oluştu!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Yonetim_Load(object sender, EventArgs e)
        {
            Kontrol();
            Arama();
            fillGridView();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AramaYap();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("Tüm alanları doldurun!", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (comboBox1.Text.Equals("Çalışan") || comboBox1.Text.Equals("Yönetici")) {
                con.Open();
                String sql2 = "insert into personel values ('" + textBox2.Text + "','" + textBox3.Text + "','" + textBox1.Text + "','" + comboBox1.Text + "')";
                MySqlCommand command1 = new MySqlCommand(sql2, con);
                command1.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Bilgiler başarıyla eklendi!", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TabloYenile();
            }
            else
            {
                MessageBox.Show("Bir hata oluştu, tekrar deneyin!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Kaldırmak istediğiniz personelin ID bilgisini girin!", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            con.Open();
            String sql2 =("delete from personel where personel_id="+ textBox2.Text);
            MySqlCommand command1 = new MySqlCommand(sql2, con);
            command1.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Bilgiler başarıyla kaldırıldı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            TabloYenile();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TabloYenile();
        }

        private void TabloYenile()
        {
            dtst.Clear();
            sql = "select * from personel";
            adtr = new MySqlDataAdapter(sql, con);
            fillGridView();
        }
    }
}