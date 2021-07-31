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
    public partial class Onay : Form
    {
        MySqlConnection con;
        MySqlDataAdapter adtr;
        DataSet dtst = new DataSet();
        public Onay()
        {
            InitializeComponent();
            con = new MySqlConnection("Server=localhost;Database=proje_DBMS;Uid=root;Pwd='151906007';");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Close();
            this.Hide();
        }

        private void Sil()
        {
            con.Open();
            String deleteSQL = "delete from kitapdurum where uye_id=" + Uyelik.silinecekID;
            MySqlCommand command2 = new MySqlCommand(deleteSQL, con);
            command2.ExecuteNonQuery();
            con.Close();
            con.Open();
            String deleteSQL2 = "delete from uye where uye_id=" + Uyelik.silinecekID;
            MySqlCommand command3 = new MySqlCommand(deleteSQL2, con);
            command3.ExecuteNonQuery();
            con.Close();
            this.Hide();
            MessageBox.Show("Üye başarıyla silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Sil();
            con.Close();
        }
    }
}
