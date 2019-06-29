using EContact.eContactClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EContact
{
    public partial class Econtact : Form
    {
        public Econtact()
        {
            InitializeComponent();
        }
        contactClass c = new contactClass();

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            


        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void DgvContact_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BtnTambah_Click(object sender, EventArgs e)
        {
            //dapatkan nilai dari kolom input 
            c.Nama = txtNama.Text;
            c.Ponsel = txtPonsel.Text;
            c.Jekel = cmbJekel.Text;
            c.Alamat = txtAlamat.Text;

            //enter data into the database using this method
            bool success = c.Insert(c);
            if (success == true)
            {
                //Success Inserted
                MessageBox.Show("Kontak Baru berhasil ditambahkan");
                Clear();
            }
            else
            {
                //Gagal Ditambahkan
                MessageBox.Show("Gagal Menambahkan Kontak, Coba Lagi.");
            }
            DataTable dt = c.Select();
            dgvContact.DataSource = dt;
        }
        private void Clear()
        {
            txtContactID.Text = "";
            txtNama.Text = "";
            txtPonsel.Text = "";
            cmbJekel.Text = "";
            txtAlamat.Text = "";
            txtSearch.Text = "";

        }

        private void Econtact_Load(object sender, EventArgs e)
        {
            DataTable dt = c.Select();
            dgvContact.DataSource = dt;
        }

        private void DgvContact_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtContactID.Text = dgvContact.Rows[rowIndex].Cells[0].Value.ToString();
            txtNama.Text = dgvContact.Rows[rowIndex].Cells[1].Value.ToString();
            txtPonsel.Text = dgvContact.Rows[rowIndex].Cells[2].Value.ToString();
            cmbJekel.Text = dgvContact.Rows[rowIndex].Cells[3].Value.ToString();
            txtAlamat.Text = dgvContact.Rows[rowIndex].Cells[4].Value.ToString();
        }

        private void BtnUbah_Click(object sender, EventArgs e)
        {
            c.ContactID = Convert.ToInt32(txtContactID.Text);
            c.Nama = txtNama.Text;
            c.Jekel= cmbJekel.Text;
            c.Ponsel = txtPonsel.Text;
            c.Alamat = txtAlamat.Text;

            bool success = c.Update(c);

            if (success == true)
            {
                MessageBox.Show("Data Kontak Berhasil diubah");
                Clear();
            }
            else
            {
                MessageBox.Show("Maaf, Data Kontak Gagal diubah.");
            }
            DataTable dt = c.Select();
            dgvContact.DataSource = dt;
        }

        private void BtnHapus_Click(object sender, EventArgs e)
        {
            c.ContactID = Convert.ToInt32(txtContactID.Text);

            bool success = c.Delete(c);
            if (success)
            {
                MessageBox.Show("Data Kontak Berhasil Terhapus");
                Clear(); 
            }
            else
            {
                MessageBox.Show("Maaf, Data Kontak Gagal dihapus ");
            }

            DataTable dt = c.Select();
            dgvContact.DataSource = dt;
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text;
            if (keyword!=null)
            {
                DataTable dt = c.Search(keyword);
                dgvContact.DataSource = dt;
            }
            else
            {
                DataTable dt = c.Select();
                dgvContact.DataSource = dt;
            }
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
