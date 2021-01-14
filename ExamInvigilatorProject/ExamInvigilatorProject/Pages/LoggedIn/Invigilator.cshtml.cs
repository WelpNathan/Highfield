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
        public ArrayList ids;

        private readonly ILogger<InvigilatorModel> _logger;

        public InvigilatorModel(ILogger<InvigilatorModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
        
        public List<string[]> getNames()
        {
            List<string[]> names = new List<string[]>();
            ids = editor.getAllIds();
            for (int i = 0; i < ids.Count; i++)
            {
                names.Add(editor.getName(ids[i]));
            }
            return names;

        }

      
        
        

        
    }
}
