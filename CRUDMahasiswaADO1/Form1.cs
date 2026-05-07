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

        private void Buka_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                MessageBox.Show("Koneksi berhasil");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                conn.Open();

                string query = @"INSERT INTO mahasiswa
                                (nim,nama,jenis_kelamin,tanggal_lahir,alamat,nama_prodi)
                                VALUES
                                (@nim,@nama,@jk,@tgl,@alamat,@prodi)";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@nim", textBox1.Text);
                cmd.Parameters.AddWithValue("@nama", textBox2.Text);
                cmd.Parameters.AddWithValue("@jk", comboBox1.Text);
                cmd.Parameters.AddWithValue("@tgl", dateTimePicker1.Value.Date);
                cmd.Parameters.AddWithValue("@alamat", textBox3.Text);
                cmd.Parameters.AddWithValue("@prodi", textBox4.Text);

                cmd.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show("Data berhasil ditambahkan");

                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                conn.Open();

                string query = @"UPDATE mahasiswa
                                SET nama=@nama,
                                    jenis_kelamin=@jk,
                                    tanggal_lahir=@tgl,
                                    alamat=@alamat,
                                    nama_prodi=@prodi
                                WHERE nim=@nim";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@nim", textBox1.Text);
                cmd.Parameters.AddWithValue("@nama", textBox2.Text);
                cmd.Parameters.AddWithValue("@jk", comboBox1.Text);
                cmd.Parameters.AddWithValue("@tgl", dateTimePicker1.Value.Date);
                cmd.Parameters.AddWithValue("@alamat", textBox3.Text);
                cmd.Parameters.AddWithValue("@prodi", textBox4.Text);

                cmd.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show("Data berhasil diupdate");

                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                string query = "DELETE FROM mahasiswa WHERE nim=@nim";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nim", textBox1.Text);

                cmd.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show("Data berhasil dihapus");

                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable test = new DataTable();

                string query = "SELECT * FROM mahasiswa WHERE nim = '" + textBox1.Text + "'";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                da.Fill(test);

                dataGridView1.DataSource = test;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["nim"].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["nama"].Value.ToString();
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["jenis_kelamin"].Value.ToString();
                