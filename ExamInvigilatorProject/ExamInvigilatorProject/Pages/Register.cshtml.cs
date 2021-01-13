using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ExamInvigilatorProject.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly ILogger<RegisterModel> _logger;

        public RegisterModel(ILogger<RegisterModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
        public void OnPostRegister()
        {
            bool success = false;
            dbEdit editor = new dbEdit();
            char role = 'C';
            var emailAddress = Request.Form["email"];
            var userpassword = Request.Form["userpassword"];
            var confirm = Request.Form["passwordConfirm"];
            var firstName = Request.Form["firstName"];
            var lastName = Request.Form["surname"];
            var roled = Request.Form["userType"];

            if(roled == "learner")
            {
                role = 'C';
            }
            else if (roled == "invigilator")
            {
                role = 'I';
            }

            if(userpassword == confirm)
            {
                byte[] salt = editor.generateSalt();
                success = editor.register(emailAddress, firstName, lastName, userpassword, salt, role);
                
            }


        }
    }
}
