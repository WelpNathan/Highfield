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

        public bool success = true;
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
            dbEdit editor = new dbEdit();
            var emailAddress = Request.Form["email"];
            var userpassword = Request.Form["userpassword"];
            var confirm = Request.Form["passwordConfirm"];
            var firstName = Request.Form["firstName"];
            var lastName = Request.Form["surname"];
            var roled = Request.Form["userType"];


            //prepare and bind parameters



            //end of binding

            string role = "UNDEFINED";
            if (roled == "learner")
            {
                role = "LEARNER";
            }
            else if (roled == "invigilator")
            {
                role = "INVIGILATOR";
            }

            if(userpassword == confirm)
            {
                byte[] salt = editor.generateSalt();
                success = editor.register(emailAddress, firstName, lastName, userpassword, salt, role);
                
            }


        }
    }
}
