using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ExamInvigilatorProject.Pages
{
    public class LearnerModel : PageModel
    {
        private readonly ILogger<LearnerModel> _logger;

        public LearnerModel(ILogger<LearnerModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
