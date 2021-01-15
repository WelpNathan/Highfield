using System;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExamInvigilatorProject.Pages.LoggedIn
{
    public class TempNotePageModel : PageModel
    {
        public void OnGet()
        {
        }

        public void OnPostSubmit()
        {
            var examId = Request.Form["examId"];
            var notes = Request.Form["notes"];
            new dbEdit().AddNewNote(examId, notes);
        }
    }
}
