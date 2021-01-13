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
        public bool success = false;
        public int attempts = 0;

        private readonly ILogger<LoginModel> _logger;

        public LoginModel(ILogger<LoginModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public void OnPostLogin()
        {


            dbEdit editor = new dbEdit();

            var emailAddress = Request.Form["loginemail"];
            var password = Request.Form["loginpassword"];



            bool found = editor.checkEmail(emailAddress);

            if (found)
            {
                string passwordHash = editor.getPasswordHash(emailAddress);
                byte[] passwordSalt = editor.getPasswordSalt(emailAddress);



                string saltedPassword = editor.hashPassword(password, passwordSalt);


                if (saltedPassword == passwordHash)
                {
                    success = true;
                    Response.Redirect("LoggedIn/Invigilator");
                }
                else
                {
                    //login failed.
                    attempts += 1;
                }
            }
            attempts += 1;

        }


    }
}
