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

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                textBox1.Text.Length != 11 ||
                !long.TryParse(textBox1.Text, out _))
            {
                MessageBox.Show("NIM harus 11 digit angka");
                textBox1.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text) || textBox2.Text.Length > 50)
            {
                MessageBox.Show("Nama wajib diisi dan maksimal 50 karakter");
                textBox2.Focus();
                return false;
            }

            if (textBox3.Text.Length > 50)
            {
                MessageBox.Show("Alamat maksimal 50 karakter");
                textBox3.Focus();
                return false;
            }

            if (textBox4.Text.Length > 30)
            {
                MessageBox.Show("Nama Prodi maksimal 30 karakter");
                textBox4.Focus();
                return false;
            }

            return true;
        }

        private void LoadData()
        {
            try
            {
                dt.Clear();

                string query = "SELECT * FROM mahasiswa";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                da.Fill(dt);

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearForm()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();

            comboBox1.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
        }
