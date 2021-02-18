using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamInvigilatorProject
{
    public interface ILearnerService
    {
        List<Learner> GetAll();
        void AddSelectedLearner(Guid id);

    }
    
    public class LearnerService : ILearnerService
    {
        List<Guid> ids = new List<Guid>();
        List<Guid> cookies = new List<Guid>();
        List<Guid> selectedLearnerIds = new List<Guid>();
        private dbEdit editor = new dbEdit();

        
        public List<Learner> GetAll()
        {
            
            List<Learner> learners = new List<Learner>();
           
            ids = editor.getAllAccountIds();
            cookies = editor.getAllSessionIds();

            for (int i = 0; i < ids.Count; i++)
            {
                if (editor.getRoleWithId(ids[i]) == "LEARNER")
                {
                    Learner learner = new Learner(ids[i], cookies[i]);
                    learners.Add(learner);
                }
            }
            return learners;
           
        }

        public void AddSelectedLearner(Guid id)
        {
            this.selectedLearnerIds.Add(id);
        }

    }
}
