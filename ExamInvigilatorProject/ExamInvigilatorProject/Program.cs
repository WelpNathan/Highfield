
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
using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

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

            string connetionString;
            SqlConnection cnn;
            connetionString = "Server = localhost; Database = exam_db; User Id = SA; Password = strong!123";
            cnn = new SqlConnection(connetionString);
            cnn.Open();         // need error message for exceptions - e.g. if connection can't be made load error page

            
            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSystemd()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

       
    }



    public class dbEdit
    {
        static string connetionString = "Server = localhost; Database = exam_db; User Id = SA; Password = strong!123";
        SqlConnection cnn = new SqlConnection(connetionString);

        public object Response { get; private set; }

        //code to add a note to a page
        public void AddNewNote(string examId, string notes)
        {
            cnn.Open();
            const string query = "UPDATE dbo.tblExams SET Notes=@Notes WHERE Id=@ExamId";
            using (var cmd = new SqlCommand(query, cnn))
            {
                cmd.Parameters.AddWithValue("@ExamId", examId);
                cmd.Parameters.AddWithValue("@Notes", notes);
                cmd.ExecuteReader();
            }
            cnn.Close();
        }

        //Gets a users Id with a given session cookie.
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

            // get time of session start.
            DateTime time = DateTime.Now;
            string sqlFormattedDate = time.ToString("yyyy-MM-dd HH:mm:ss.fff");
            // add into login sessions

            
            List<Guid> Ids = getAllAccountIds();
            cnn.Open();
            if (!Ids.Contains((Guid) accountId))
            {
                const string email = "INSERT INTO dbo.tblLoginSessions(AccountId, SessionId, Data) VALUES(@AccountId, @SessionId, @Data)";
                using (var cmd = new SqlCommand(email, cnn))
                {
                    cmd.Parameters.AddWithValue("@AccountId", accountId);
                    cmd.Parameters.AddWithValue("@SessionId", cookieAuthCode);
                    cmd.Parameters.AddWithValue("@Data", sqlFormattedDate);
                    using var reader = cmd.ExecuteReader();

                }
            }
            else
            {
                string update = "UPDATE dbo.tblLoginSessions SET Data=@Data, SessionId=@SessionId WHERE AccountId=@AccountId";
                using (var cmd = new SqlCommand(update, cnn))
                {
                    cmd.Parameters.AddWithValue("@SessionId", cookieAuthCode);
                    cmd.Parameters.AddWithValue("@Data", sqlFormattedDate);
                    cmd.Parameters.AddWithValue("@AccountId", accountId);
                    using var reader = cmd.ExecuteReader();
                }
            }
            
            cnn.Close();

            // add cookie
            Console.WriteLine("hi: " + cookieAuthCode.ToString());
            response.Cookies.Append("HIGHFIELD_AUTH", cookieAuthCode.ToString(), option);       
        }

        //get a users Id from a given email
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

        //sets a learner to ready inside the db
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

        //checks if an email exists inside the db, if it does true is returned, if not false is returned
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


        //returns the passwordHash from a given Email.
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

        //returns the passwordSalt from a given email.
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

        //registers a user into the system.
        public bool register(string email, string firstName, string LastName, string password, byte[] salt, string role)
        {
            try
            {
                cnn.Open();

                string register = "INSERT INTO dbo.tblLogins(Id, Email, FirstName, LastName, PasswordHash, PasswordSalt, IsReady) VALUES(@Id, @email, @FirstName, @LastName, @PasswordHash, @PasswordSalt, @IsReady)";

                Guid guid = Guid.NewGuid();

                // create base account
                using (SqlCommand cmd = new SqlCommand(register, cnn))
                {
                    // Prepare and Bind Parameters
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@Id", guid);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    cmd.Parameters.AddWithValue("@LastName", LastName);
                    cmd.Parameters.AddWithValue("@PasswordHash", hashPassword(password, salt));
                    cmd.Parameters.AddWithValue("@PasswordSalt", Convert.ToBase64String(salt));
                    cmd.Parameters.AddWithValue("@IsReady", 0);
                    cmd.ExecuteNonQuery();
                }

                // create role
                var roleSql = "INSERT INTO dbo.tblRoles(LoginId, RoleName) VALUES(@Id, @Role)";

                using (SqlCommand cmd = new SqlCommand(roleSql, cnn))
                {
                    cmd.Parameters.AddWithValue("@Id", guid);
                    cmd.Parameters.AddWithValue("@Role", role.ToUpper());
                    cmd.ExecuteNonQuery();
                }

                cnn.Close();
                return true;
                //once registered return to login page.


            }
            catch (SqlException exception)
            {
                cnn.Close();
                Console.WriteLine(exception);
                return false;
            }

            
        }

        public string getRole(string givenEmail)
        {
            Guid? id = GetIdFromEmail(givenEmail);

            return getRoleWithId(id);


        }

        public string getRoleWithId(Guid? id)
        {
            cnn.Open();
            string roled = "C";
            string role = "SELECT RoleName FROM dbo.tblRoles WHERE LoginId = @Id";
            using (SqlCommand cmd = new SqlCommand(role, cnn))
            {
                cmd.Parameters.AddWithValue("@Id", id.ToString());
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roled = reader["RoleName"].ToString();
                    }

                }
            }
            cnn.Close();
            return roled;


        }


        public string[] getName(Guid? guid)
        {
            cnn.Open();
            string[] name = new string[2];
            string sql = "SELECT FirstName, LastName FROM dbo.tblLogins WHERE Id = @Id";
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


       public List<Guid> getAllSessionIds()
        {
            cnn.Open();
            List<Guid> ids = new List<Guid>();
            string sql = "SELECT SessionId FROM dbo.tblLoginSessions";
            using (SqlCommand cmd = new SqlCommand(sql, cnn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string temp = reader["SessionId"].ToString();
                        Guid guid = new Guid(temp);
                        ids.Add(guid);
                    }

                }
            }
            cnn.Close();
            return ids;
        }

        public List<Guid> getAllAccountIds()
        {
            cnn.Open();
            List<Guid> ids = new List<Guid>();
            string sql = "SELECT AccountId FROM dbo.tblLoginSessions";
            using (SqlCommand cmd = new SqlCommand(sql, cnn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string temp = reader["AccountId"].ToString();
                        Guid guid = new Guid(temp);
                        ids.Add(guid);
                    }

                }
            }
            cnn.Close();
            return ids;
        }

        public string getTime(Guid? guid)
        {
            cnn.Open();
            string date = "";
            string sql = "SELECT Data FROM dbo.tblLoginSessions WHERE SessionId = @Id";
            using (SqlCommand cmd = new SqlCommand(sql, cnn))
            {
                cmd.Parameters.AddWithValue("@Id", guid);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        date = reader["Data"].ToString();
                    }

                }
            }
            cnn.Close();
            return date;
        }

        //adds a user to the exam table
        public void addToExam(Guid? invigId, Guid? learnerId)
        {
            cnn.Open();

            string exam = "INSERT INTO dbo.tblExams(Id, InvigilatorId, CandidateId, Date, noteId) VALUES(@Id, @InvigilatorId, @CandidateId, @Date, @noteId)";
            DateTime time = DateTime.Now;
            Guid noteId = Guid.NewGuid();
            Guid examId = Guid.NewGuid();
            using (SqlCommand cmd = new SqlCommand(exam, cnn))
            {
                // Prepare and Bind Parameters
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@Id", examId);
                cmd.Parameters.AddWithValue("@InvigilatorId", invigId);
                cmd.Parameters.AddWithValue("@CandidateId", learnerId);
                cmd.Parameters.AddWithValue("@Date", time);
                cmd.Parameters.AddWithValue("@noteId", noteId);
                cmd.ExecuteNonQuery();
            }
            cnn.Close();
        }

        //removes a user from the sessions table, call when adding to the exam table.
        public void removeFromSession(Guid? id)
        {
            cnn.Open();

            string delete = "DELETE FROM dbo.tblLoginSessions WHERE AccountId = @Id";
            using (SqlCommand cmd = new SqlCommand(delete, cnn))
            {
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
            cnn.Close();
        }
    }
}




