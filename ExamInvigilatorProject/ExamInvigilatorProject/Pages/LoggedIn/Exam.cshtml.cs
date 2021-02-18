using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ExamInvigilatorProject.Pages
{
    public class ExamModel : PageModel
    {
        private readonly ILogger<ExamModel> _logger;
        private ILearnerService _learnerService;
        private List<string> learnerIds;
        public ExamModel(ILogger<ExamModel> logger, ILearnerService learnerService)
        {
            _logger = logger;
            _learnerService = learnerService;
        }

        public void OnGetStart(List<string> learnerIds)
        {
            this.learnerIds = learnerIds;
        }
    }
}
