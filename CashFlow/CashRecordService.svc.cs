using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace CashFlow
{
    // OBSERVAÇÃO: Você pode usar o comando "Renomear" no menu "Refatorar" para alterar o nome da classe "CashRecordService" no arquivo de código, svc e configuração ao mesmo tempo.
    // OBSERVAÇÃO: Para iniciar o cliente de teste do WCF para testar esse serviço, selecione CashRecordService.svc ou CashRecordService.svc.cs no Gerenciador de Soluções e inicie a depuração.
    public class CashRecordService : ICashRecordService
    {
        public string AddRecord(string description, int amount, string email)
        {
            try
            {
                string conStr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
                SqlConnection con = new SqlConnection(conStr);
                SqlCommand cmd = con.CreateCommand();
                con.Open();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into WCF_Finances values(@description,@amount,@email)";
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.ExecuteNonQuery();

                con.Close();

                return "Success";
            }
            catch
            {
                return "Error to insert record";
            }
        }

        public string DeleteRecord(int id)
        {
            try
            {
                string conStr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
                SqlConnection con = new SqlConnection(conStr);
                SqlCommand cmd = con.CreateCommand();
                con.Open();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Delete from WCF_Finances where  Id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                con.Close();

                return "Success";
            }
            catch
            {
                return "Error to delete record";
            }
        }

        public DataTable GetAllRecords(string email)
        {
            try
            {
                string conStr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
                SqlConnection con = new SqlConnection(conStr);
                SqlCommand cmd = con.CreateCommand();
                con.Open();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select * from WCF_Finances where  Email = @email";
                cmd.Parameters.AddWithValue("@email", email);
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                int numberRecords = dt.Rows.Count;

                con.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Login(string email, string password)
        {
            string conStr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = con.CreateCommand();
            con.Open();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from WCF_Users where  Email = @email and Password = @password";
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            var i = Convert.ToInt32(dt.Rows.Count.ToString());

            if (i == 1)
            {
                con.Close();
                return "Success";
            }
            else
            {
                con.Close();
                return "Invalid email or password";
            }

        }

        public string Register(string fullName, string email, string password)
        {
            string conStr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = con.CreateCommand();
            con.Open();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select Email from WCF_Users where Email = @email";
            cmd.Parameters.AddWithValue("@email", email);
            cmd.ExecuteNonQuery();
            
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            var i = Convert.ToInt32(dt.Rows.Count.ToString());

            if (i == 0)
            {
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "Insert into WCF_Users(FullName,Email,Password) values(@name,@email,@pwd)";
                cmd1.Parameters.AddWithValue("@email", email);
                cmd1.Parameters.AddWithValue("@name", fullName);
                //cmd1.Parameters.AddWithValue("@pwd", Encrypt(password));
                cmd1.Parameters.AddWithValue("@pwd", password);
                cmd1.ExecuteNonQuery();
                con.Close();
                return "Success";
            }
            else
            {
                con.Close();
                return "There is already a registered user with this email";
            }
        }

        public string UpdateRecord(int id, string description, int amount)
        {
            try
            {
                string conStr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
                SqlConnection con = new SqlConnection(conStr);
                SqlCommand cmd = con.CreateCommand();
                con.Open();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Update WCF_Finances set Description = @description, Amount = @amount  where  Id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.ExecuteNonQuery();

                con.Close();

                return "Success";
            }
            catch
            {
                return "Error to insert record";
            }
        }

        private static string Encrypt(string value)
        {
            using(MD5CryptoServiceProvider md5= new MD5CryptoServiceProvider())
            {
                UTF8Encoding utf8 = new UTF8Encoding();
                byte[] data = md5.ComputeHash(utf8.GetBytes(value));
                return Convert.ToBase64String(data);
            }
        }

    }
}
