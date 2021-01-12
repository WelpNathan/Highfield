using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace ExamInvigilatorProject
{
    public class Program
    {
        public static void Main(string[] args)
        {

            string connetionString;
            SqlConnection cnn;
            connetionString = "Server = localhost; Database = exam_db; User Id = SA; Password = strong!123";
            cnn = new SqlConnection(connetionString);
            cnn.Open();

            string delete = "DELETE FROM dbo.tblLogins;";
            string sql = "insert into dbo.tblLogins(Id, Email, FirstName, LastName, PasswordHash, PasswordSalt) VALUES(@Id, @email, @FirstName, @LastName, @PasswordHash, @PasswordSalt)";

            SqlCommand deleteTable = new SqlCommand(delete, cnn);

            deleteTable.ExecuteNonQuery();

            Guid guid = Guid.NewGuid();
            

            using (SqlCommand cmd = new SqlCommand(sql, cnn))
            {
                // Create and set the parameters values 
                cmd.Parameters.AddWithValue("@Id", guid);
                cmd.Parameters.AddWithValue("@Email", "hello");
                cmd.Parameters.AddWithValue("@FirstName", "hello");
                cmd.Parameters.AddWithValue("@LastName", "hello");
                cmd.Parameters.AddWithValue("@PasswordHash", "hello");
                cmd.Parameters.AddWithValue("@PasswordSalt", "hello");

                cmd.ExecuteNonQuery();

                CreateHostBuilder(args).Build().Run();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    


        public byte[] generateSalt()
        {
            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }


        public string hashPassword(string password, byte[] salt)
        {

            

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        }
    
    }
}
