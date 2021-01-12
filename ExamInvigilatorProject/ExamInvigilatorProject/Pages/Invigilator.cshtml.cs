using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ExamInvigilatorProject.Pages
{
    public class InvigilatorModel : PageModel
    {
        private readonly ILogger<InvigilatorModel> _logger;

        public InvigilatorModel(ILogger<InvigilatorModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
