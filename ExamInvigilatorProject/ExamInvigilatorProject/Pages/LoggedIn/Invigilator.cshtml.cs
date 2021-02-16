using System;
using System.Collections;
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


        public string test = "test";
        public int noOfNames = 0;
        public dbEdit editor = new dbEdit();
        public List<Guid> sessions;
        public List<Guid> ids;
        public List<learner> learners;
        public List<learner> chosenLearners;
        public bool firstLoad = true;

        public struct learner
        {
            public string[] name;
            public string time;
        }

        private readonly ILogger<InvigilatorModel> _logger;

        public InvigilatorModel(ILogger<InvigilatorModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }


        public void OnGetSelect()
        {
            string test = "ts";
        }

        private List<string[]> getNames()
        {
            List<string[]> names = new List<string[]>();
            //ids = editor.getAllIds();
            for (int i = 0; i < ids.Count; i++)
            {
                names.Add(editor.getName(ids[i]));
            }
            return names;

        }

        private List<string> getTimes()
        {
            List<string> times = new List<string>();
            //ids = editor.getAllIds();
            for (int i = 0; i < sessions.Count; i++)
            {
                times.Add(editor.getTime(sessions[i]));
            }
            return times;
        }

        public List<learner> GetLearners()
        {
            List<learner> learners = new List<learner>();
            List<string[]> names = getNames();
            List<string> times = getTimes();

            for(int i = 0; i < names.Count; i++)
            {
                learner temp = new learner();
                temp.name = names[i];
                temp.time = times[i];
                learners.Add(temp);

            }

            return learners;
        }
        
    }
}
