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
    public partial class YayineviYazar : Form
    {
        MySqlConnection con;
        Boolean isMevcut = false;
        Boolean isMevcutY = false;

        public YayineviYazar()
        {
            InitializeComponent();
            con = new MySqlConnection("Server=localhost;Database=proje_DBMS;Uid=root;Pwd='151906007';");
        }

        private void CevirmenEkle()
        {
            if (textBox4.Text == "")
            {
                MessageBox.Show("Çevirmen adı boş bırakılamaz!", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (textBox4.Text != "")
            {
                con.Open();
                MySqlCommand cmd1 = new MySqlCommand("select cevirmen_ad from cevirmen", con);
                MySqlDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    if (Convert.ToString(dr1["cevirmen_ad"]) == Convert.ToString(textBox4.Text))
                    {
                        MessageBox.Show("Çevirmen zaten mevcut!", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        isMevcut = true;
                        con.Close();
                        return;
                    }
                }
                con.Close();
                if (isMevcut == false)
                {
                    con.Open();
                    String addSQL = "insert into cevirmen (cevirmen_ad) values ('" + textBox4.Text + "')";
                    MySqlCommand command1 = new MySqlCommand(addSQL, con);
                    command1.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Çevirmen başarıyla eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            isMevcut = false;
        }

        private void YazarEkle()
        {
            if (textBox3.Text == "")
            {
                MessageBox.Show("Yazar adı boş bırakılamaz!", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (textBox3.Text != "")
            {
                con.Open();
                MySqlCommand cmd1 = new MySqlCommand("select yazar_ad from yazar", con);
                MySqlDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    if (Convert.ToString(dr1["yazar_ad"]) == Convert.ToString(textBox3.Text))
                    {  
                        MessageBox.Show("Yazar zaten mevcut!", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        isMevcut = true;
                        con.Close();
                        return;
                    }
                }
                con.Close();
                if (isMevcut == false)
                {
                    con.Open();
                    String addSQL = "insert into yazar (yazar_ad) values ('"+ textBox3.Text + "')";
                    MySqlCommand command1 = new MySqlCommand(addSQL, con);
                    command1.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Yazar başarıyla eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        isMevcut = false;
        }

        private void YayineviEkle()
        {
            if (textBox1.Text == "" | textBox2.Text =="")
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (textBox1.Text !="" && textBox2.Text !="")
            {
                con.Open();
                MySqlCommand cmd1 = new MySqlCommand("select yayinevi_ad from yayinevi", con);
                MySqlDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    if (Convert.ToString(dr1["yayinevi_ad"]) == Convert.ToString(textBox1.Text))
                    {
                        MessageBox.Show("Yayınevi zaten mevcut!", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        isMevcutY = true;
                        con.Close();
                        return;
                    }
                }
                con.Close();
                if (isMevcutY == false)
                {
                    con.Open();
                    String addSQL = "insert into yayinevi (yayinevi_ad, yayinevi_adres) values ('" + textBox1.Text + "','" + textBox2.Text + "')";
                    MySqlCommand command1 = new MySqlCommand(addSQL, con);
                    command1.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Yayınevi başarıyla eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            isMevcutY = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            YazarEkle();
        }

        private void button1_Click(object sender, EventArgs e)
        {     
            YayineviEkle();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CevirmenEkle();
        }
    }
}
