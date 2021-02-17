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
    public class profileModel : PageModel
    {
        public bool success = false;
        public int attempts = 0;

        private readonly ILogger<profileModel> _logger;

        public profileModel(ILogger<profileModel> logger)
        {
            _logger = logger;
        }

       
    }
}
