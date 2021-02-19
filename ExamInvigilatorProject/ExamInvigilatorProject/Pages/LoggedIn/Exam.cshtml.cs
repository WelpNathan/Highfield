using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ExamInvigilatorProject.Pages
{
    public class ExamModel : PageModel
    {
        private readonly ILogger<ExamModel> _logger;
        private ILearnerService _learnerService;
        public List<string> learnerIds;
        private string invigId;
        public dbEdit editor = new dbEdit();
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
            foreach (var learner in learnerIds)
            {
                editor.addToExam(Guid.Parse(invigId), Guid.Parse(learner));
                editor.removeFromSession(Guid.Parse(learner));
            }
        }

        public void OnGetStartExam(string ids)
        {
            List<string> result = JsonConvert.DeserializeObject<List<string>>(ids);

            
        }
    }
}
