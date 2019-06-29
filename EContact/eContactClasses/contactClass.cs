using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EContact.eContactClasses
{
    class contactClass
    {
        public int ContactID { get; set; }
        public string Nama { get; set; }
        public string Ponsel { get; set; }
        public string Jekel { get; set; }
        public string Alamat { get; set; }

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region METHDO TAMPIL DATA  
        public DataTable Select()
        {
            //Sql Connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            //DataTable
            DataTable dt = new DataTable();
            try
            {
                //Query tampil Data
                string sql = "SELECT * FROM tbl_contact ";
                //Sql Command
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Data Adapter
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //Open Connection
                conn.Open();

                adapter.Fill(dt);
               
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        #endregion

        #region METHOD TAMBAH DATA
        public bool Insert(contactClass c)
        {
            bool success = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Query tambah data
                string sql = "INSERT INTO tbl_contact (Nama,Ponsel,Jekel,Alamat) VALUES (@Nama,@Ponsel,@Jekel,@Alamat) ";

                SqlCommand cmd = new SqlCommand(sql, conn);
                //Buat Parameter untuk menambahkan data

             
                cmd.Parameters.AddWithValue("@Nama", c.Nama);
                cmd.Parameters.AddWithValue("@Ponsel", c.Ponsel);
                cmd.Parameters.AddWithValue("@Jekel", c.Jekel);
                cmd.Parameters.AddWithValue("@Alamat", c.Alamat);

                //Open connection
                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                //jika query berhasil
                if (rows>0)
                {
                    success = true;
                }
                else
                {
                    success = false;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return success;
        }
        #endregion

        #region METHOD UBAH DATA
        public bool Update(contactClass c)
        {
            bool success = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Query Update Data
                string sql = "UPDATE tbl_contact SET Nama=@Nama, Ponsel=@Ponsel, Jekel=@Jekel, Alamat=@Alamat where ContactID=@ContactID";

                //SqlCommand
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Buat parameter untuk update data
                cmd.Parameters.AddWithValue("@Nama", c.Nama);
                cmd.Parameters.AddWithValue("@Ponsel", c.Ponsel);
                cmd.Parameters.AddWithValue("@Jekel", c.Jekel);
                cmd.Parameters.AddWithValue("@Alamat", c.Alamat);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows>0)
                {
                    success = true;
                }
                else
                {
                    success = false;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return success;
        }
        #endregion

        #region METHOD HAPUS DATA
        public bool Delete(contactClass c)
        {
            bool success = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Query untuk hapus data
                string sql = "DELETE FROM tbl_contact where ContactID=@ContactID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows>0)
                {
                    success = true;
                }
                else
                {
                    success = false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return success;
        }
        #endregion

        #region METHOD SEARCH DATA DENGAN KEYWORD
        public DataTable Search(string keyword)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();


            try
            {
                string sql = "SELECT * FROM tbl_contact where ContactID LIKE'%" + keyword + "%' OR Nama LIKE '%"+keyword+"%' OR Ponsel LIKE '%"+keyword+"%'  ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        #endregion
    }
}
