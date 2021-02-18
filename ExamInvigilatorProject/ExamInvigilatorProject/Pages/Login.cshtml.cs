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
using System.Security.Authentication;

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

        public RedirectToPageResult OnPostLogin()
        {
            dbEdit editor = new dbEdit();

            var emailAddress = Request.Form["loginemail"];          //needs sanitising to avoid sql injection
            var password = Request.Form["loginpassword"];           //needs sanitising to avoid sql injection



            bool found = editor.checkEmail(emailAddress);

            if (found)
            {
                string passwordHash = editor.getPasswordHash(emailAddress);
                byte[] passwordSalt = editor.getPasswordSalt(emailAddress);

                string saltedPassword = editor.hashPassword(password, passwordSalt);


                if (saltedPassword == passwordHash)
                {
                    // add cookie when signed in
                    editor.AddNewSession(editor.GetIdFromEmail(emailAddress), Response);

                    success = true;
                    string role = editor.getRole(emailAddress);

                    if (role == "INVIGILATOR")
                    {
                        return RedirectToPage("LoggedIn/Invigilator", "Start", new {id = editor.GetIdFromEmail(emailAddress)});
                        //Response.Redirect("LoggedIn/Invigilator");
                    }
                    else
                    {
                        //editor.logLogin(emailAddress);
                        
                        Response.Redirect("LoggedIn/Learner");
                        //Student redirect.
                    }
                }
                else
                {
                    //login failed.
                    attempts += 1;
                    //need error message
                    //after 3 failed login attempts, link to forgot password/username page?
                }
            }
            attempts += 1;

            return RedirectToPage();
        }


    }
}
