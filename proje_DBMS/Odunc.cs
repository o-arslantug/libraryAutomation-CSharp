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
    public partial class Odunc : Form
    {
        MySqlConnection con;
        MySqlDataAdapter adtr;
        MySqlDataAdapter adtr2;
        DataSet dtst = new DataSet();
        DataSet dtst2 = new DataSet();
        string sql;
        string sql2;
        Boolean uyeKontrol = false;
        Boolean kitapKontrol = false;
        Boolean cezaKontrol = false;
        Boolean teslimKontrol = false;
        Boolean sureKontrol = false;
        Boolean mevcutKontrol = false;

        public Odunc()
        {
            InitializeComponent();
            sql = "select kitapdurum.kitapdurum_id, kitapdurum.uye_id, uye.uye_ad, kitapdurum.kitap_id, kitap.kitap_ad, kitapdurum.kitapdurum_verilistarih, kitapdurum.kitapdurum_teslimtarih from kitapdurum inner join kitap on kitapdurum.kitap_id=kitap.kitap_id inner join uye on uye.uye_id=kitapdurum.uye_id";
            con = new MySqlConnection("Server=localhost;Database=proje_DBMS;Uid=root;Pwd='151906007';");
            adtr = new MySqlDataAdapter(sql, con);
            fillGridView();
            fillGridView2();
        }

        void fillGridView()
        {
            try
            {
                con.Open();
                adtr.Fill(dtst, "Kontrol");
                dataGridView1.DataSource = dtst.Tables["Kontrol"];
                dataGridView1.Columns[0].HeaderText = "İşlem ID";
                dataGridView1.Columns[1].HeaderText = "Üye ID";
                dataGridView1.Columns[2].HeaderText = "Üye Adı";
                dataGridView1.Columns[3].HeaderText = "Kitap ID";
                dataGridView1.Columns[4].HeaderText = "Kitap Adı";
                dataGridView1.Columns[5].HeaderText = "Veriliş Tarihi";
                dataGridView1.Columns[6].HeaderText = "Teslim Tarihi";
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Database ile bağlantı kurulamadı!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void fillGridView2()
        {
            sql2 = "select uye_id, uye_ad, uye_yasakdurumu, uye_gsm, uye_mail, uye_adres from uye";
            con = new MySqlConnection("Server=localhost;Database=proje_DBMS;Uid=root;Pwd='151906007';");
            adtr2 = new MySqlDataAdapter(sql2, con);
            try
            {
                con.Open();
                adtr2.Fill(dtst2, "Uye");
                dataGridView2.DataSource = dtst2.Tables["Uye"];
                dataGridView2.Columns[0].HeaderText = "Üye ID";
                dataGridView2.Columns[1].HeaderText = "Üye Adı";
                dataGridView2.Columns[2].HeaderText = "Yasak Durumu";
                dataGridView2.Columns[3].Visible = false;
                dataGridView2.Columns[4].Visible = false;
                dataGridView2.Columns[5].Visible = false;
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Database ile bağlantı kurulamadı!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void TabloYenile()
        {
            dtst.Clear();
            dtst2.Clear();
            sql = "select kitapdurum.kitapdurum_id, kitapdurum.uye_id, uye.uye_ad, kitapdurum.kitap_id, kitap.kitap_ad, kitapdurum.kitapdurum_verilistarih, kitapdurum.kitapdurum_teslimtarih from kitapdurum inner join kitap on kitapdurum.kitap_id = kitap.kitap_id inner join uye on uye.uye_id = kitapdurum.uye_id";
            sql2 = "select uye_id, uye_ad, uye_yasakdurumu, uye_gsm, uye_mail, uye_adres from uye";
            adtr = new MySqlDataAdapter(sql, con);
            adtr2 = new MySqlDataAdapter(sql2, con);
            fillGridView();
            fillGridView2();
        }

        private void TeslimatYokla()
        {
            dtst.Clear();
            sql = "select kitapdurum.kitapdurum_id, kitapdurum.uye_id, uye.uye_ad, kitapdurum.kitap_id, kitap.kitap_ad, kitapdurum.kitapdurum_verilistarih, kitapdurum.kitapdurum_teslimtarih from kitapdurum inner join kitap on kitapdurum.kitap_id = kitap.kitap_id inner join uye on uye.uye_id = kitapdurum.uye_id where datediff(kitapdurum.kitapDurum_teslimTarih, curdate())<=7;";
            try
            {
                adtr = new MySqlDataAdapter(sql, con);
                con.Open();
                adtr.Fill(dtst, "Kontrol");
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Arama sırasında hata oluştu!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Information);;
            }
        }

        private void UyeAramaYap()
        {
            dtst2.Clear();
            if (textBox3.Text != "")
            {
                sql2 = "select uye_id, uye_ad, uye_yasakdurumu from uye where uye_ad like'%" + textBox3.Text + "%'";
            }
            else
            {
                MessageBox.Show("Lütfen bir isim girin!", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TabloYenile();
                return;
            }
            try
            {
                adtr2 = new MySqlDataAdapter(sql2, con);
                con.Open();
                adtr2.Fill(dtst2, "Uye");
                con.Close();
            }
            catch (Exception e)
            {
                TabloYenile();
                MessageBox.Show("Arama sırasında hata oluştu!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void SureUzat()
        {
            if (textBox2.Text == "" | textBox1.Text == "")
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (textBox2.Text != "" && textBox1.Text != "")
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select kitapdurum_id from kitapdurum where uye_id=" + textBox1.Text + " and kitap_id=" + textBox2.Text + "", con);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (Convert.ToInt32(dr["kitapdurum_id"]) >= 0)
                    {
                        con.Close();
                        con.Open();
                        String updateSQL = "update kitapdurum set kitapdurum_teslimtarih=adddate(kitapdurum_teslimtarih,7) where uye_id='" + textBox1.Text + "' and kitap_id='" + textBox2.Text + "' ";
                        MySqlCommand command3 = new MySqlCommand(updateSQL, con);
                        command3.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Teslim tarihi 1 hafta ertelendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        sureKontrol = true;
                        break;
                    }
                }
                con.Close();
                TabloYenile();
                if (sureKontrol == false)
                {
                    MessageBox.Show("Mevcut bir üye-kitap çifti girin!", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            sureKontrol = false;
        }

        private void OduncVer()
        {
            if (textBox2.Text == "" | textBox1.Text == "")
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (textBox2.Text != "" && textBox1.Text != "")
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select uye_id, uye_yasakdurumu from uye", con);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (Convert.ToInt32(dr["uye_id"]) == Convert.ToInt32(textBox1.Text) && Convert.ToInt32(dr["uye_yasakdurumu"]) == 0)
                    {
                        uyeKontrol = true;
                        break;
                    }
                }
                con.Close();
                con.Open();
                MySqlCommand cmd2 = new MySqlCommand("select kitap_id from kitap", con);
                MySqlDataReader dr2 = cmd2.ExecuteReader();
                while (dr2.Read())
                {
                    if (Convert.ToInt32(dr2["kitap_id"]) == Convert.ToInt32(textBox2.Text))
                    {
                        kitapKontrol = true;
                        break;
                    }
                }
                con.Close();
                con.Open();
                MySqlCommand cmd3 = new MySqlCommand("select kitap_mevcutsayi from kitap where kitap_id='"+textBox2.Text+"'", con);
                MySqlDataReader dr3 = cmd3.ExecuteReader();
                while (dr3.Read())
                {
                    if (Convert.ToInt32(dr3["kitap_mevcutsayi"])>0)
                    {
                        mevcutKontrol = true;
                        break;
                    }
                }
                con.Close();           
                if (uyeKontrol == true && kitapKontrol == true && mevcutKontrol == true)
                {
                    con.Open();
                    String addSQL = "insert into kitapdurum (uye_id, kitap_id, kitapdurum_verilistarih, kitapdurum_teslimtarih) values (" + textBox1.Text + "," + textBox2.Text + ", curdate(), adddate(curdate(),21))";
                    String decreaseSQL = "update kitap set kitap_mevcutsayi=kitap_mevcutsayi-1 where kitap_id=" + textBox2.Text + "";
                    MySqlCommand command1 = new MySqlCommand(addSQL, con);
                    MySqlCommand command2 = new MySqlCommand(decreaseSQL, con);
                    command1.ExecuteNonQuery();
                    command2.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Bilgiler başarıyla güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TabloYenile();
                }
                else if (uyeKontrol == false && kitapKontrol == false)
                {
                    MessageBox.Show("Kitap bilgisi bulunamadı, üye bilgisi ise ya bulunamadı ya da üye cezalı!", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (uyeKontrol == false)
                {
                    MessageBox.Show("Üye bilgisi bulunamadı ya da cezalı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (kitapKontrol == false)
                {
                    MessageBox.Show("Kitap bilgisi bulunamadı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (mevcutKontrol == false)
                {
                    MessageBox.Show("Aradığınız kitap envanterde şu an için mevcut değil!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            kitapKontrol = false;
            uyeKontrol = false;
            cezaKontrol = false;
            mevcutKontrol = false;
        }

        private void TeslimAl()
        {

            if (textBox2.Text == "" | textBox1.Text == "")
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (textBox2.Text != "" && textBox1.Text != "")
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select kitapdurum_id from kitapdurum where uye_id=" + textBox1.Text + " and kitap_id=" + textBox2.Text + "", con);
                String increaseSQL = "update kitap set kitap_mevcutsayi=kitap_mevcutsayi+1 where kitap_id=" + textBox2.Text + "";
                MySqlCommand command1 = new MySqlCommand(increaseSQL, con);
                command1.ExecuteNonQuery();
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (Convert.ToInt32(dr["kitapdurum_id"]) >= 0)
                    {
                        con.Close();
                        con.Open();
                        String deleteSQL = "delete from kitapdurum where uye_id=" + textBox1.Text + " and kitap_id=" + textBox2.Text + "";
                        MySqlCommand command2 = new MySqlCommand(deleteSQL, con);
                        command2.ExecuteNonQuery();
                        MessageBox.Show("Teslim alındı!", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        teslimKontrol = true;
                        break;
                    }
                }
                con.Close();
                TabloYenile();
                if (teslimKontrol == false)
                {
                    MessageBox.Show("Mevcut bir üye-kitap çifti girin!", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            teslimKontrol = false;
        }

        public void CezaKontrol()
        {
            int id = 0;
            con.Open();
            MySqlCommand cmd = new MySqlCommand("select uye.uye_id from kitapdurum inner join uye on kitapdurum.uye_id=uye.uye_id where datediff(kitapdurum_teslimtarih,kitapdurum_verilistarih)<datediff(curdate(),kitapdurum_verilistarih) and uye.uye_yasakdurumu=0", con);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (Convert.ToInt32(dr["uye_id"]) >= 0)
                {
                    id = Convert.ToInt32(dr["uye_id"]);
                    con.Close();
                    con.Open();
                    String updateSQL = "update uye set uye_yasakdurumu=1 where uye_id="+id +"";
                    MySqlCommand command3 = new MySqlCommand(updateSQL, con);
                    command3.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Cezalandırılan var!", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cezaKontrol = true;
                    break;
                }
            }
            con.Close();
            TabloYenile();
            if (cezaKontrol==false)
            {
                MessageBox.Show("Cezalandırılacak kullanıcı yok!", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            cezaKontrol= false;
        }

        private void CezaKaldır()
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Lütfen bir kullanıcı ID'si girin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (textBox1.Text != "")
            {
                con.Open();
                String updateSQL = "update uye set uye_yasakdurumu=0 where uye_id=" + textBox1.Text;
                MySqlCommand command3 = new MySqlCommand(updateSQL, con);
                MessageBox.Show("Kulanıcının cezası var ise kaldırıldı!", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                command3.ExecuteNonQuery();
                con.Close();
            }    
        }


        private void button3_Click(object sender, EventArgs e)
        
        {
            TeslimatYokla();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TabloYenile();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UyeAramaYap();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OduncVer();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TeslimAl();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SureUzat();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            CezaKontrol();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            CezaKaldır();
        }
    }
}
