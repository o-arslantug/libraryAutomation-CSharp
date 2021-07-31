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
    public partial class Uyelik : Form
    {
        MySqlConnection con;
        MySqlDataAdapter adtr;
        Boolean isMevcut = false;
        Boolean uyeMevcut = false;
        DataSet dtst = new DataSet();
        string sql;
        public static int silinecekID;
        public Uyelik()
        {
            InitializeComponent();
            con = new MySqlConnection("Server=localhost;Database=proje_DBMS;Uid=root;Pwd='151906007';");
            adtr = new MySqlDataAdapter(sql, con);
            fillGridView();
            Kontrol();
            Arama();
        }

        void fillGridView()
        {
            sql = "select uye_id, uye_ad, uye_gsm, uye_mail, uye_adres from uye";
            con = new MySqlConnection("Server=localhost;Database=proje_DBMS;Uid=root;Pwd='151906007';");
            adtr = new MySqlDataAdapter(sql, con);
            try
            {
                con.Open();
                adtr.Fill(dtst, "Uye");
                dataGridView1.DataSource = dtst.Tables["Uye"];
                dataGridView1.Columns[0].HeaderText = "Üye ID";
                dataGridView1.Columns[1].HeaderText = "Üye Adı";
                dataGridView1.Columns[2].HeaderText = "GSM No";
                dataGridView1.Columns[3].HeaderText = "Mail";
                dataGridView1.Columns[4].HeaderText = "Adres";
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Database ile bağlantı kurulamadı!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void TabloYenile()
        {
            dtst.Clear();
            sql = "select uye_id, uye_ad, uye_gsm, uye_mail, uye_adres from uye";
            adtr = new MySqlDataAdapter(sql, con);
            fillGridView();
        }

        private void Kontrol()
        {
            if (Anasayfa.seviye == "Çalışan")
            {
                button2.Enabled = false;
                textBox6.Enabled = false;
            }
        }

        private void Arama()
        {
            comboBox1.Items.Add("Ad-Soyad");
            comboBox1.Items.Add("GSM Numarası");
            comboBox1.Items.Add("Mail Adresi");
        }

        private void AramaYap()
        {
            dtst.Clear();
            if (comboBox1.SelectedItem == "Ad-Soyad")
            {
                sql = "select uye_id, uye_ad, uye_gsm, uye_mail, uye_adres from uye where uye_ad like'%" + textBox5.Text + "%'";
            }
            else if (comboBox1.SelectedItem == "GSM Numarası")
            {
                sql = "select uye_id, uye_ad, uye_gsm, uye_mail, uye_adres from uye where uye_gsm like'%" + textBox5.Text + "%'";
            }

            else if (comboBox1.SelectedItem == "Mail Adresi")
            {
                sql = "select uye_id, uye_ad, uye_gsm, uye_mail, uye_adres from uye where uye_mail like'%" + textBox5.Text + "%'";
            }
            else
            {
                MessageBox.Show("Arama kriteri seçiniz!", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            try
            {
                adtr = new MySqlDataAdapter(sql, con);
                con.Open();
                adtr.Fill(dtst, "Uye");
                con.Close();
            }
            catch (Exception e)
            {
                TabloYenile();
                MessageBox.Show("Arama sırasında hata oluştu!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UyeEkle();
        }

        private void UyeEkle()
        {

            if (textBox1.Text == "" | textBox2.Text == "" | textBox3.Text == "" | textBox4.Text == "")
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {
                con.Open();
                MySqlCommand cmd1 = new MySqlCommand("select uye_gsm from uye", con);
                MySqlDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    if (Convert.ToString(dr1["uye_gsm"]) == Convert.ToString(textBox2.Text))
                    {
                        MessageBox.Show("Üye zaten mevcut!", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        isMevcut = true;
                        con.Close();
                        return;
                    }
                }
                con.Close();
                if (isMevcut == false)
                {
                    con.Open();
                    String addSQL = "insert into uye (uye_ad, uye_gsm, uye_mail, uye_adres, uye_yasakdurumu) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "', '0')";
                    MySqlCommand command1 = new MySqlCommand(addSQL, con);
                    command1.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Üye başarıyla kaydedildi!", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            isMevcut = false;
            TabloYenile();
        }

        private void UyeSil()
        {
            if (textBox6.Text == "Silinecek ID yazılır." | textBox6.Text == "")
            {
                MessageBox.Show("Silinecek üyenin ID değerini giriniz!", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (textBox6.Text != "")
            {
                con.Open();
                MySqlCommand deletecmd = new MySqlCommand("select uye_id from uye where uye_id=" + textBox6.Text, con);
                MySqlDataReader deletecmddr = deletecmd.ExecuteReader();
                while (deletecmddr.Read())
                {
                    if (Convert.ToInt32(deletecmddr["uye_id"]) > 0)
                    {
                        silinecekID = Convert.ToInt32(textBox6.Text);
                        uyeMevcut = true;
                        Onay onay = new Onay();
                        onay.Show();
                    }
                    if (uyeMevcut == false)
                    {
                        MessageBox.Show("Kayıtlı üye bulunamadı!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                con.Close();
                uyeMevcut = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AramaYap();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UyeSil();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TabloYenile();
        }
    }
}
