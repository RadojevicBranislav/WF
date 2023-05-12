using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace DataLayer
{
    public class Model_PicRepository
    {
        internal string connString = ConstantParameters.connString;

        public byte[] GetModel_PicById(int id)
        {

            Model_Pic model = new Model_Pic();
            
            using(SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand();
                
                cmd.Connection = conn;
                
                cmd.CommandText = "SELECT * FROM Pic where Id=@id";
                
                cmd.Parameters.Add("@id", SqlDbType.Int, -1).Value = id;

                conn.Open();

                byte[] imageData = null;

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    imageData = (byte[])reader["img"];
                }

                reader.Close();
                
                return imageData;
            }
        }
       
        public int InsertPic(Model_Pic pic)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConstantParameters.connString))
            {
                SqlCommand sql = new SqlCommand();

                sql.Connection = sqlConnection;

                sql.CommandText = "INSERT INTO Pic VALUES (@img);";
                
                sql.Parameters.Add("@img", SqlDbType.VarBinary, -1).Value = pic.Image;

                sqlConnection.Open();

                return sql.ExecuteNonQuery();
            }
        }
    }
}

