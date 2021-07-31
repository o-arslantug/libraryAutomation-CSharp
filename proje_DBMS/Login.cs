using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace proje_DBMS
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        bool pwdControl1 = false;
        bool pwdControl2 = false;
        public void button1_Click(object sender, EventArgs e)
        {

            if ((textBox1.Text == "" && textBox2.Text == "") || (textBox1.Text != "" && textBox2.Text == "") || (textBox1.Text == "" && textBox2.Text != ""))
            {
                MessageBox.Show("Lütfen bilgilerinizi giriniz!", "Uyarı!", MessageBoxButtons.OK);
            }
            else
            {
                int id = 0;
                string sifre = "";
                MySqlConnection con = new MySqlConnection("Server=localhost;Database=proje_DBMS;Uid=root;Pwd='151906007';");
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select personel_id from personel", con);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if(Convert.ToInt32(dr["personel_id"]) == Convert.ToInt32(textBox1.Text))
                    {
                        id = Convert.ToInt32(dr["personel_id"]);
                        pwdControl1 = true;
                    }
                }
                con.Close();
                con.Open();
                MySqlCommand cmd1 = new MySqlCommand("select personel_sifre from personel where personel_id="+id, con);
                MySqlDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    if (Convert.ToString(dr1["personel_sifre"]) == Convert.ToString(textBox2.Text))
                    {
                        sifre = Convert.ToString(dr1["personel_sifre"]);
                        pwdControl2 = true;
                    }
                }
                con.Close();
                String Seviye = "";
                con.Open();
                MySqlCommand cmd2 = new MySqlCommand("select personel_yetki from personel where personel_id="+id, con);
                MySqlDataReader dr2 = cmd2.ExecuteReader();
                while (dr2.Read())
                {
                    Seviye = Convert.ToString(dr2["personel_yetki"]);
                }
                if (pwdControl1==true && pwdControl2==true)
                {
                    MessageBox.Show(Seviye + " olarak anasayfa yönlendiriliyorsunuz.", "Bilgi!");
                    Anasayfa Anasayfa = new Anasayfa();
                    Anasayfa.anaSeviye = Seviye;
                    Anasayfa.Show();       
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı!", "Hata!");
                    pwdControl1 = false;
                    pwdControl2 = false;
                }
            }
        }
    }
}