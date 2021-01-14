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

        public void OnPostButton()
        {
            _logger.LogInformation("Setting learner ready status to yes.");
            // dbEdit.SetLearnerReady();

            var dbEdit = new dbEdit();

            var userId = dbEdit.GetUserIdFromCookie(Request.Cookies["HIGHFIELD_AUTH"]);

            if (userId == null) return;

            _logger.LogInformation($"Setting user Id {userId}'s status to ready.");
            dbEdit.SetLearnerReady(userId.Value, true);

        }

        public void OnPost()
        {

        }
    }
}
