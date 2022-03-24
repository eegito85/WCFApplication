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
            throw new NotImplementedException();
        }

        public string DeleteRecord(int id)
        {
            throw new NotImplementedException();
        }

        public DataTable GetAllRecords(string email)
        {
            throw new NotImplementedException();
        }

        public string Login(string email, string password)
        {
            throw new NotImplementedException();
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

        public string UpdateRecord(int id, string desc, int amount)
        {
            throw new NotImplementedException();
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
