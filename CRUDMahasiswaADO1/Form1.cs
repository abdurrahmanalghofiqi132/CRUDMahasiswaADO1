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
