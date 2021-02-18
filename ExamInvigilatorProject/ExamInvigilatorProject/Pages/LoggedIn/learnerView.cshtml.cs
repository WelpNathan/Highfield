using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ExamInvigilatorProject.Pages
{
    public class LearnerViewModel : PageModel
    {
        private readonly ILogger<LearnerViewModel> _logger;

        public LearnerViewModel(ILogger<LearnerViewModel> logger)
        {
            _logger = logger;
        }

   
}
