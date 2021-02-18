using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ExamInvigilatorProject.Pages
{
    public class InvigilatorModel : PageModel
    {


        public string test = "test";
        public int noOfNames = 0;
        public dbEdit editor = new dbEdit();
        public List<Guid> sessions;
        public List<Guid> ids;

        public List<learner> chosenLearners;
        public bool firstLoad = true;

        public struct learner
        {
            public string[] name;
            public string time;
        }

        private readonly ILogger<InvigilatorModel> _logger;
        private ILearnerService _learnerService;

        public InvigilatorModel(ILearnerService learnerService, ILogger<InvigilatorModel> logger)
        {
            _learnerService = learnerService;
            _logger = logger;
        }

        public List<Learner> learners { get; set; }

        public PartialViewResult OnGetLearnerPartial()
        {
            learners = _learnerService.GetAll();
            return Partial("_PartialTable", learners);
        }

        public void OnPostSelected(string ids)
        {
            List<string> result = JsonConvert.DeserializeObject<List<string>>(ids);
            Response.Redirect("learner", false);
        }
       
        
    }
}
