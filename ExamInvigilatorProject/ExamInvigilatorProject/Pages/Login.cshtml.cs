using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace ExamInvigilatorProject.Pages
{
    public class LoginModel : PageModel
    {


        private readonly ILogger<LoginModel> _logger;

        public LoginModel(ILogger<LoginModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            dbEdit editor = new dbEdit();

            var emailAddress = Request.Form["loginemail"];
            var password = Request.Form["loginpassword"];

            string connetionString;
            SqlConnection cnn;
            connetionString = "Server = localhost; Database = exam_db; User Id = SA; Password = strong!123";
            cnn = new SqlConnection(connetionString);

            bool found = editor.checkEmail(emailAddress, cnn);

            if (found)
            {
                string passwordHash = editor.getPasswordHash(emailAddress, cnn);
                byte[] passwordSalt = editor.getPasswordSalt(emailAddress, cnn);



                string saltedPassword = editor.hashPassword(password, passwordSalt);


                if (saltedPassword == passwordHash)
                {
                    //login successful!
                }
                else
                {
                    //login failed.
                }
            }

        }
    }
}
