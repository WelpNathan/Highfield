
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
using System.Text;
using System.Collections;
using Microsoft.AspNetCore.Http;

namespace ExamInvigilatorProject
{
    public class Program
    {
        public static void Main(string[] args)
        {

            dbEdit edit = new dbEdit();

            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }


            // Response.Redirect(String.Format("YourTargetPage.cshtml?YourValue={0}", yourValueToPass));

            string connetionString;
            SqlConnection cnn;
            connetionString = "Server = localhost; Database = exam_db; User Id = SA; Password = strong!123";
            cnn = new SqlConnection(connetionString);
            cnn.Open();         // need error message for exceptions - e.g. if connection can't be made load error page

            //string delete = "DELETE FROM dbo.tblLogins;";
          /*  string register = "INSERT INTO dbo.tblLogins(Id, Email, FirstName, LastName, PasswordHash, PasswordSalt) VALUES(@Id, @email, @FirstName, @LastName, @PasswordHash, @PasswordSalt)";


            SqlCommand deleteTable = new SqlCommand(delete, cnn);

            deleteTable.ExecuteNonQuery();

            Guid guid = Guid.NewGuid();


            using (SqlCommand cmd = new SqlCommand(register, cnn))
            {
                // Create and set the parameters values 
                cmd.Parameters.AddWithValue("@Id", guid);
                cmd.Parameters.AddWithValue("@Email", "hello@hello.com");
                cmd.Parameters.AddWithValue("@FirstName", "hello");
                cmd.Parameters.AddWithValue("@LastName", "hello");
                cmd.Parameters.AddWithValue("@PasswordHash", edit.hashPassword("hello", salt));
                cmd.Parameters.AddWithValue("@PasswordSalt", Convert.ToBase64String(salt));

                cmd.ExecuteNonQuery();
            }*/
            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });






        public void getEmail(string givenEmail, SqlConnection cnn)
        {

            string login = "SELECT * from dbo.tblLogins WHERE Email=@givenEmail";
            using (SqlCommand email = new SqlCommand(login, cnn))
            {
                email.Parameters.AddWithValue("@givenEmail", givenEmail);
            }

        }
    }









    public class dbEdit
    {
        static string connetionString = "Server = localhost; Database = exam_db; User Id = SA; Password = strong!123";
        SqlConnection cnn = new SqlConnection(connetionString);

        public Guid? GetUserIdFromCookie(string cookieAuthCode)
        {
            cnn.Open();
            const string query = "SELECT AccountId FROM dbo.tblLoginSessions WHERE SessionId=@sessionId";
            using (var cmd = new SqlCommand(query, cnn))
            {
                cmd.Parameters.AddWithValue("@sessionId", cookieAuthCode);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var id = reader[0];
                    cnn.Close();
                    return new Guid(id.ToString()!);
                }

            }
            cnn.Close();
            return null;
        }

        // creates a new session (for storing on cookie)
        public void AddNewSession(Guid? accountId, HttpResponse response)
        {
            // create new cookie options
            var option = new CookieOptions {Expires = DateTime.Now.AddMilliseconds(1000 * 60 * 24)};

            // create session id
            var cookieAuthCode = Guid.NewGuid();
            
            // add into login sessions
            cnn.Open();
            const string email = "INSERT INTO dbo.tblLoginSessions(AccountId, SessionId) VALUES(@AccountId, @SessionId)";
            using (var cmd = new SqlCommand(email, cnn))
            {
                cmd.Parameters.AddWithValue("@AccountId", accountId);
                cmd.Parameters.AddWithValue("@SessionId", cookieAuthCode);
                using var reader = cmd.ExecuteReader();

            }
            cnn.Close();

            // add cookie
            Console.WriteLine("hi: " + cookieAuthCode.ToString());
            response.Cookies.Append("HIGHFIELD_AUTH", cookieAuthCode.ToString(), option);
        }

        public Guid? GetIdFromEmail(string email)
        {
            cnn.Open();
            const string query = "SELECT Id FROM dbo.tblLogins WHERE Email=@givenEmail";
            using (var cmd = new SqlCommand(query, cnn))
            {
                cmd.Parameters.AddWithValue("@givenEmail", email);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var id = reader[0];
                    cnn.Close();
                    return new Guid(id.ToString()!);
                }

            }
            cnn.Close();
            return null;
        }

        public void SetLearnerReady(Guid id, bool isReady)
        {
            cnn.Open();
            const string email = "UPDATE dbo.tblLogins SET IsReady=@isReady WHERE Id=@id";
            using (var cmd = new SqlCommand(email, cnn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@isReady", isReady ? 1 : 0);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cnn.Close();
                    return;
                }

            }
            cnn.Close();
        }

        public bool checkEmail(string givenEmail)
        {
            cnn.Open();
            string email = "SELECT Email FROM dbo.tblLogins WHERE Email=@givenEmail";
            using (SqlCommand cmd = new SqlCommand(email, cnn))
            {

                cmd.Parameters.AddWithValue("@givenEmail", givenEmail);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cnn.Close();
                        return true;
                    }

                }

            }
            cnn.Close();

            return false;

        }



        public string getPasswordHash(string givenEmail)
        {
            cnn.Open();
            string hash = "";
            string password = "SELECT PasswordHash FROM dbo.tblLogins WHERE Email=@givenEmail";
            using (SqlCommand cmd = new SqlCommand(password, cnn))
            {
                cmd.Parameters.AddWithValue("@givenEmail", givenEmail);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        hash = reader["PasswordHash"].ToString();

                    }

                }
            }
            cnn.Close();
            return hash;
        }


        public byte[] getPasswordSalt(string givenEmail)
        {
            cnn.Open();
            byte[] salted = new byte[128 / 8];
            string salt = "";
            string password = "SELECT PasswordSalt FROM dbo.tblLogins WHERE Email=@givenEmail";
            using (SqlCommand cmd = new SqlCommand(password, cnn))
            {
                cmd.Parameters.AddWithValue("@givenEmail", givenEmail);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        salt = reader["PasswordSalt"].ToString();
                    }

                }
                salted = Convert.FromBase64String(salt);
            }
            cnn.Close();
            return salted;
        }

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
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        }


        public bool register(string email, string firstName, string LastName, string password, byte[] salt, char role)
        {
            try
            {
                cnn.Open();
                string register = "INSERT INTO dbo.tblLogins(Id, Email, FirstName, LastName, PasswordHash, PasswordSalt, Role, IsReady) VALUES(@Id, @email, @FirstName, @LastName, @PasswordHash, @PasswordSalt, @Role, @IsReady)";

                Guid guid = Guid.NewGuid();

                using (SqlCommand cmd = new SqlCommand(register, cnn))
                {
                    // Create and set the parameters values 
                    cmd.Parameters.AddWithValue("@Id", guid);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    cmd.Parameters.AddWithValue("@LastName", LastName);
                    cmd.Parameters.AddWithValue("@PasswordHash", hashPassword(password, salt));
                    cmd.Parameters.AddWithValue("@PasswordSalt", Convert.ToBase64String(salt));
                    cmd.Parameters.AddWithValue("@Role", role);
                    cmd.Parameters.AddWithValue("@IsReady", 0);
                    cmd.ExecuteNonQuery();
                }
                cnn.Close();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
            
           
        }

        public string getRole(string givenEmail)
        {
            cnn.Open();
            string roled = "C";
            string role = "SELECT Role FROM dbo.tblLogins WHERE Email = @givenEmail";
            using (SqlCommand cmd = new SqlCommand(role, cnn))
            {
                cmd.Parameters.AddWithValue("@givenEmail", givenEmail);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roled = reader["Role"].ToString();

                    }

                }
            }
            cnn.Close();
            return roled;


        }

        public void logLogin(string givenEmail)
        {
            cnn.Open();
            string id = "";
            string sql = "SELECT Id FROM dbo.tblLogins WHERE Email = @givenEmail";
            using (SqlCommand cmd = new SqlCommand(sql, cnn))
            {
                cmd.Parameters.AddWithValue("@givenEmail", givenEmail);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = reader["Id"].ToString();

                    }

                }
            }
            DateTime time = DateTime.Now;
            Guid guid = Guid.Empty;
            guid = new Guid(id);
            string sqlFormattedDate = time.ToString("yyyy-MM-dd HH:mm:ss.fff");

            sql = "INSERT INTO dbo.tblLoginLogs(AccountId, Date) VALUES(@AccountId, @Date)";
            using (SqlCommand cmd = new SqlCommand(sql, cnn))
            {
                cmd.Parameters.AddWithValue("@AccountId", guid);
                cmd.Parameters.AddWithValue("@Date", time);
                cmd.ExecuteNonQuery();
            }
            cnn.Close();
        }


        public string[] getName(Guid guid)
        {
            cnn.Open();
            string[] name = new string[2];
            string sql = "SELECT Email FROM dbo.tblLogins WHERE Id = @Id";
            using (SqlCommand cmd = new SqlCommand(sql, cnn))
            {
                cmd.Parameters.AddWithValue("@Id", guid);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        name[0] = reader["FirstName"].ToString();
                        name[1] = reader["LastName"].ToString();

                    }

                }
            }
            cnn.Close();
            return name;
        }


        public ArrayList getAllIds()
        {
            cnn.Open();
            ArrayList ids = new ArrayList();
            string sql = "SELECT AccountId FROM dbo.tblLoginLogs";
            using (SqlCommand cmd = new SqlCommand(sql, cnn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ids.Add(reader["AccountId"].ToString());
                    }

                }
            }
            cnn.Close();
            return ids;
        }
    }
}




