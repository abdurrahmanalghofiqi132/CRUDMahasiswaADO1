using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace CRUDMahasiswaADO1
{
    public partial class Form1 : Form
    {
        MySqlConnection conn;
        DataTable dt = new DataTable();

        string connString = "server=localhost;database=dbkampus;uid=root;pwd=;";

        public Form1()
        {
            InitializeComponent();
            conn = new MySqlConnection(connString);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Laki-laki");
            comboBox1.Items.Add("Perempuan");

            textBox1.MaxLength = 11;
            textBox2.MaxLength = 50;
            textBox3.MaxLength = 50;
            textBox4.MaxLength = 30;
        }
