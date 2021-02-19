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
        private string invigId;
        public dbEdit editor;
        public ExamModel(ILogger<ExamModel> logger, ILearnerService learnerService)
        {
            _logger = logger;
            _learnerService = learnerService;
        }

        public void OnGetStart(List<string> learnerIds)
        {
            this.invigId = learnerIds[0];
            learnerIds.RemoveAt(0);
            this.learnerIds = learnerIds;
            Guid? exam = new Guid();

            foreach (var learner in this.learnerIds)
            {
                editor.addToExam(Guid.Parse(invigId), Guid.Parse(learner), exam);
            }
            

        }
    }
}
