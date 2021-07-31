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
    public partial class Envanter : Form
    {
        MySqlConnection con;
        MySqlDataAdapter adtr;
        DataSet dtst = new DataSet();
        string sql;
        public static int silinecekID;
        public String yonSeviye;
        int yazarID = 0;
        int yayineviID = 0;
        int cevirmenID = 0;
        int temp = 0;
        int temp1 = 0;

        public Envanter()
        {
            InitializeComponent();
            Kontrol();
            sql = "select kitap.kitap_id, kitap.kitap_ad, kitap.kitap_isbn, kitap.kitap_basimYili, cevirmen.cevirmen_ad, yayinevi.yayinevi_ad, yazar.yazar_ad, kitap.kitap_kategori, kitap.kitap_konum, kitap.kitap_mevcutsayi from kitap inner join yazar on kitap.kitap_yazarId=yazar_id inner join cevirmen on kitap.kitap_cevirmenid=cevirmen.cevirmen_id inner join yayinevi on kitap.kitap_yayineviid=yayinevi.yayinevi_id";
            con = new MySqlConnection("Server=localhost;Database=proje_DBMS;Uid=root;Pwd='151906007';");
            adtr = new MySqlDataAdapter(sql, con);
            fillGridView();
            Arama();
            ComboDoldurYazar();
            ComboDoldurYayinevi();
            ComboDoldurCevirmen();
        }

        void fillGridView()
        {
            try
            {
                con.Open();
                adtr.Fill(dtst, "Kitaplar2");
                dataGridView1.DataSource = dtst.Tables["Kitaplar2"];
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Kitap Adı";
                dataGridView1.Columns[2].HeaderText = "ISBN No";
                dataGridView1.Columns[3].HeaderText = "Basım Yılı";
                dataGridView1.Columns[4].HeaderText = "Çevirmen Adı";
                dataGridView1.Columns[5].HeaderText = "Yayın Evi Adı";
                dataGridView1.Columns[6].HeaderText = "Yazar Adı";
                dataGridView1.Columns[7].HeaderText = "Kategori";
                dataGridView1.Columns[8].HeaderText = "Konum";
                dataGridView1.Columns[9].HeaderText = "Mevcut Sayı";
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
            if (Anasayfa.seviye == "Çalışan")
            {
                button5.Enabled = false;
            }
        }

        private void ComboDoldurYazar()
        {
            con.Open();
            MySqlCommand fillComboYazar = new MySqlCommand("select yazar_ad from yazar", con);
            MySqlDataReader dr2fC = fillComboYazar.ExecuteReader();
            while (dr2fC.Read())
            {
                comboBox2.Items.Add(dr2fC["yazar_ad"]);
            }
            con.Close();
        }

        private void ComboDoldurYayinevi()
        {
            con.Open();
            MySqlCommand fillComboYayinevi = new MySqlCommand("select yayinevi_ad from yayinevi", con);
            MySqlDataReader dr3fC = fillComboYayinevi.ExecuteReader();
            while (dr3fC.Read())
            {
                comboBox3.Items.Add(dr3fC["yayinevi_ad"]);
            }
            con.Close();
        }

        private void ComboDoldurCevirmen()
        {
            con.Open();
            MySqlCommand fillComboCevirmen = new MySqlCommand("select cevirmen_ad from cevirmen", con);
            MySqlDataReader dr4fC = fillComboCevirmen.ExecuteReader();
            while (dr4fC.Read())
            {
                comboBox4.Items.Add(dr4fC["cevirmen_ad"]);
            }
            con.Close();
        }


        private void TabloYenile()
        {
            dtst.Clear();
            sql = "select kitap.kitap_id, kitap.kitap_ad, kitap.kitap_isbn, kitap.kitap_basimYili, cevirmen.cevirmen_ad, yayinevi.yayinevi_ad, yazar.yazar_ad, kitap.kitap_kategori, kitap.kitap_konum, kitap.kitap_mevcutsayi from kitap inner join yazar on kitap.kitap_yazarId=yazar_id inner join cevirmen on kitap.kitap_cevirmenid=cevirmen.cevirmen_id inner join yayinevi on kitap.kitap_yayineviid=yayinevi.yayinevi_id";
            adtr = new MySqlDataAdapter(sql, con);
            fillGridView();
        }

        private void Update()
        {
            yazarID = comboBox2.SelectedIndex + 1;
            yayineviID = comboBox3.SelectedIndex + 1;
            cevirmenID = comboBox4.SelectedIndex + 1;
            con.Open();
            MySqlCommand cmm = new MySqlCommand("select kitap_id from kitap where kitap_id = " + textBox1.Text, con);
            MySqlDataReader drr = cmm.ExecuteReader();
            while (drr.Read())
            {
                if (Convert.ToInt32(drr["kitap_id"]) > 0)
                {
                    temp = Convert.ToInt32(drr["kitap_id"]);
                }
            }
            con.Close();
            con.Open();
            String dropSQL1 = "ALTER TABLE kitap DROP FOREIGN KEY `kitap_ibfk_1`, DROP FOREIGN KEY `kitap_ibfk_2`, DROP FOREIGN KEY `kitap_ibfk_3`";
            String dropSQL2 = "ALTER TABLE kitapdurum DROP FOREIGN KEY `kitapdurum_ibfk_1`, DROP FOREIGN KEY `kitapdurum_ibfk_2`";
            MySqlCommand dropCmd1 = new MySqlCommand(dropSQL1, con);
            MySqlCommand dropCmd2 = new MySqlCommand(dropSQL2, con);
            dropCmd1.ExecuteNonQuery();
            dropCmd2.ExecuteNonQuery();
            con.Close();
            con.Open();
            String delSQL = "delete from kitap where kitap_id="+temp;
            MySqlCommand delCmd = new MySqlCommand(delSQL,con);
            delCmd.ExecuteNonQuery();
            con.Close();
            con.Open();
            String addSQL = "INSERT INTO `Kitap` (`kitap_id`,`kitap_ad`,`kitap_isbn`,`kitap_basimyili`,`kitap_cevirmenid`,`kitap_yayineviid`,`kitap_yazarid`,`kitap_kategori`,`kitap_konum`,`kitap_mevcutsayi`) VALUES("+ temp +", 'Simyacı', '9789750726439', 2010," + cevirmenID + ", " + yayineviID + ", " + yazarID + ", 'Roman', 'K4-S3-S7', 3)";
            MySqlCommand command1 = new MySqlCommand(addSQL, con);
            command1.ExecuteNonQuery();
            con.Close();
            con.Open();
            String updateSQL2 = "Update kitap inner join yazar on kitap.kitap_yazarId = yazar.yazar_id inner join cevirmen on kitap.kitap_cevirmenid = cevirmen.cevirmen_id inner join yayinevi on kitap.kitap_yayineviid = yayinevi.yayinevi_id set kitap.kitap_ad = '" + textBox2.Text + "', kitap.kitap_isbn = '" + textBox3.Text + "', kitap.kitap_basimyili = '" + textBox4.Text + "', cevirmen.cevirmen_id = '" + cevirmenID + "', yayinevi.yayinevi_id = '" + yayineviID + "', yazar.yazar_id = '" + yazarID + "', kitap.kitap_kategori = '" + textBox8.Text + "', kitap.kitap_konum = '" + textBox9.Text + "', kitap.kitap_mevcutsayi = '" + textBox10.Text + "' where kitap.kitap_id = " + temp;
            MySqlCommand commnd = new MySqlCommand(updateSQL2, con);
            commnd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Bilgiler başarıyla güncellendi!", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            TabloYenile();
            yazarID = 0;
            yayineviID = 0;
            cevirmenID = 0;
            con.Open();
            String addSQL1 = "ALTER TABLE kitap add FOREIGN KEY (`kitap_yazarid`) REFERENCES Yazar(`yazar_id`), add FOREIGN KEY (`kitap_cevirmenid`) REFERENCES Cevirmen(`cevirmen_id`), add FOREIGN KEY (`kitap_yayineviid`) REFERENCES YayinEvi(`yayinevi_id`)";
            String addSQL2 = "ALTER TABLE kitapdurum add FOREIGN KEY (`uye_id`) REFERENCES uye(`uye_id`), add FOREIGN KEY (`kitap_id`) REFERENCES kitap(`kitap_id`)";
            MySqlCommand addCmd1 = new MySqlCommand(addSQL1, con);
            MySqlCommand addCmd2 = new MySqlCommand(addSQL2, con);
            addCmd1.ExecuteNonQuery();
            addCmd2.ExecuteNonQuery();
            con.Close();
        }

        private void AramaYap()
        {
            dtst.Clear();
            if (comboBox1.SelectedItem == "Ad")
            {
                sql = "select kitap.kitap_id, kitap.kitap_ad, kitap.kitap_isbn, kitap.kitap_basimYili, cevirmen.cevirmen_ad, yayinevi.yayinevi_ad, yazar.yazar_ad, kitap.kitap_kategori, kitap.kitap_konum, kitap.kitap_mevcutsayi from kitap inner join yazar on kitap.kitap_yazarId = yazar_id inner join cevirmen on kitap.kitap_cevirmenid = cevirmen.cevirmen_id inner join yayinevi on kitap.kitap_yayineviid = yayinevi.yayinevi_id where kitap.kitap_ad like'%" + textBox11.Text + "%'";
            }
            else if (comboBox1.SelectedItem == "ISBN")
            {
                sql = "select kitap.kitap_id, kitap.kitap_ad, kitap.kitap_isbn, kitap.kitap_basimYili, cevirmen.cevirmen_ad, yayinevi.yayinevi_ad, yazar.yazar_ad, kitap.kitap_kategori, kitap.kitap_konum, kitap.kitap_mevcutsayi from kitap inner join yazar on kitap.kitap_yazarId = yazar_id inner join cevirmen on kitap.kitap_cevirmenid = cevirmen.cevirmen_id inner join yayinevi on kitap.kitap_yayineviid = yayinevi.yayinevi_id where kitap.kitap_isbn like'%" + textBox11.Text + "%'";
            }

            else if (comboBox1.SelectedItem == "Yazar")
            {
                sql = "select kitap.kitap_id, kitap.kitap_ad, kitap.kitap_isbn, kitap.kitap_basimYili, cevirmen.cevirmen_ad, yayinevi.yayinevi_ad, yazar.yazar_ad, kitap.kitap_kategori, kitap.kitap_konum, kitap.kitap_mevcutsayi from kitap inner join yazar on kitap.kitap_yazarId = yazar_id inner join cevirmen on kitap.kitap_cevirmenid = cevirmen.cevirmen_id inner join yayinevi on kitap.kitap_yayineviid = yayinevi.yayinevi_id where yazar.yazar_ad like'%" + textBox11.Text + "%'";
            }
            else if (comboBox1.SelectedItem == "Kategori")
            {
                sql = "select kitap.kitap_id, kitap.kitap_ad, kitap.kitap_isbn, kitap.kitap_basimYili, cevirmen.cevirmen_ad, yayinevi.yayinevi_ad, yazar.yazar_ad, kitap.kitap_kategori, kitap.kitap_konum, kitap.kitap_mevcutsayi from kitap inner join yazar on kitap.kitap_yazarId = yazar_id inner join cevirmen on kitap.kitap_cevirmenid = cevirmen.cevirmen_id inner join yayinevi on kitap.kitap_yayineviid = yayinevi.yayinevi_id where kitap.kitap_kategori like'%" + textBox11.Text + "%'";
            }
            else
            {
                MessageBox.Show("Arama kriteri seçiniz.", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            try
            {
                adtr = new MySqlDataAdapter(sql, con);
                con.Open();
                adtr.Fill(dtst, "Kitaplar2");
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
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                comboBox4.SelectedItem = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                comboBox3.SelectedItem = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                comboBox2.SelectedItem = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                textBox8.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                textBox9.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                textBox10.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
            }
        }

        private void Temizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            comboBox2.SelectedItem = null;
            comboBox3.SelectedItem = null;
            comboBox4.SelectedItem = null;
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
        }


        public void Ekle()
        {
            yazarID = comboBox2.SelectedIndex + 1;
            yayineviID = comboBox3.SelectedIndex + 1;
            cevirmenID = comboBox4.SelectedIndex + 1;
            con.Open();
            String addSQL = "INSERT INTO `Kitap` (`kitap_ad`,`kitap_isbn`,`kitap_basimyili`,`kitap_cevirmenid`,`kitap_yayineviid`,`kitap_yazarid`,`kitap_kategori`,`kitap_konum`,`kitap_mevcutsayi`) VALUES('Simyacı', '9789750726439', 2010,"+cevirmenID+", "+yayineviID+", "+yazarID+", 'Roman', 'K4-S3-S7', 3)";
            MySqlCommand command1 = new MySqlCommand(addSQL, con);
            command1.ExecuteNonQuery();
            con.Close();
            int max = 1;
            con.Open();
            MySqlCommand cmd1 = new MySqlCommand("select kitap_id from kitap where kitap_id = (select max(kitap_id) from kitap)", con);
            MySqlDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                if (Convert.ToInt32(dr1["kitap_id"]) > 0)
                {
                    max = Convert.ToInt32(dr1["kitap_id"]);
                }
            }
            con.Close();
            con.Open();
            String modifySQL = "Update kitap inner join yazar on kitap.kitap_yazarId = yazar.yazar_id inner join cevirmen on kitap.kitap_cevirmenid = cevirmen.cevirmen_id inner join yayinevi on kitap.kitap_yayineviid = yayinevi.yayinevi_id set kitap.kitap_ad = '" + textBox2.Text + "', kitap.kitap_isbn = '" + textBox3.Text + "', kitap.kitap_basimyili = '" + textBox4.Text + "', kitap.kitap_kategori = '" + textBox8.Text + "', kitap.kitap_konum = '" + textBox9.Text + "', kitap.kitap_mevcutsayi = '" + textBox10.Text + "' where kitap_id="+ max;
            MySqlCommand command2 = new MySqlCommand(modifySQL, con);
            command2.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Envantere başarıyla kaydedildi!", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            TabloYenile();
            yazarID = 0;
            yayineviID = 0;
            cevirmenID = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AramaYap();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Update();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            TabloYenile();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Ekle();
        }

        private void Azalt()
        {
            con.Open();
            MySqlCommand cmm = new MySqlCommand("select kitap_id from kitap where kitap_id = " + textBox1.Text, con);
            MySqlDataReader drr = cmm.ExecuteReader();
            while (drr.Read())
            {
                if (Convert.ToInt32(drr["kitap_id"]) > 0)
                {
                    temp1 = Convert.ToInt32(drr["kitap_id"]);
                }
            }
            con.Close();
            con.Open();
            String delSQL = "Update kitap inner join yazar on kitap.kitap_yazarId = yazar.yazar_id inner join cevirmen on kitap.kitap_cevirmenid = cevirmen.cevirmen_id inner join yayinevi on kitap.kitap_yayineviid = yayinevi.yayinevi_id set kitap.kitap_mevcutsayi=kitap.kitap_mevcutsayi-1 where kitap_id=" + temp1;
            MySqlCommand delCmd = new MySqlCommand(delSQL, con);
            delCmd.ExecuteNonQuery();
            MessageBox.Show("Seçilen kayıt envanterde 1 azaltıldı!", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();
            TabloYenile();
        }
        private void Arttır()
        {
            con.Open();
            MySqlCommand cmm = new MySqlCommand("select kitap_id from kitap where kitap_id = " + textBox1.Text, con);
            MySqlDataReader drr = cmm.ExecuteReader();
            while (drr.Read())
            {
                if (Convert.ToInt32(drr["kitap_id"]) > 0)
                {
                    temp1 = Convert.ToInt32(drr["kitap_id"]);
                }
            }
            con.Close();
            con.Open();
            String delSQL = "Update kitap inner join yazar on kitap.kitap_yazarId = yazar.yazar_id inner join cevirmen on kitap.kitap_cevirmenid = cevirmen.cevirmen_id inner join yayinevi on kitap.kitap_yayineviid = yayinevi.yayinevi_id set kitap.kitap_mevcutsayi=kitap.kitap_mevcutsayi+1 where kitap_id="+ temp1;
            MySqlCommand delCmd = new MySqlCommand(delSQL, con);
            delCmd.ExecuteNonQuery();
            MessageBox.Show("Seçilen kayıt envanterde 1 arttırıldı!", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();
            TabloYenile();
        }
        private void Sil()
        {
            con.Open();
            MySqlCommand cmm = new MySqlCommand("select kitap_id from kitap where kitap_id = " + textBox1.Text, con);
            MySqlDataReader drr = cmm.ExecuteReader();
            while (drr.Read())
            {
                if (Convert.ToInt32(drr["kitap_id"]) > 0)
                {
                    temp1 = Convert.ToInt32(drr["kitap_id"]);
                }
            }
            con.Close();
            con.Open();
            String delSQL1 = "delete from kitapdurum where kitap_id=" + temp1;
            String delSQL = "delete from kitap where kitap_id=" + temp1;
            MySqlCommand delCmd1 = new MySqlCommand(delSQL1, con);
            MySqlCommand delCmd = new MySqlCommand(delSQL, con);
            delCmd1.ExecuteNonQuery();
            delCmd.ExecuteNonQuery();
            MessageBox.Show("Seçilen kayıt envanterden çıkartıldı!", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();
            TabloYenile();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Sil();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Arttır();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Azalt();
        }
    }

    
}
