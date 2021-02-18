using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamInvigilatorProject
{
    public interface ILearnerService
    {
        List<Learner> GetAll();
        //List<Learner> GetNonSelected();
        //List<Learner> GetSelected();
    }
    
    public class LearnerService : ILearnerService
    {
        List<Guid> ids = new List<Guid>();
        List<Guid> cookies = new List<Guid>();
        private dbEdit editor = new dbEdit();

        
        public List<Learner> GetAll()
        {
            
            List<Learner> learners = new List<Learner>();
           
            ids = editor.getAllAccountIds();
            cookies = editor.getAllSessionIds();

            for (int i = 0; i < ids.Count; i++)
            {
                if (editor.getRole(ids[i]) == "LEARNER")
                {


                    Learner learner = new Learner(ids[i], cookies[i]);
                    learners.Add(learner);
                }
            }
            return learners;
           
        }

/*        public List<Learner> GetNonSelected()
        {
            List<Learner> temp = GetAll();

        }

        public List<Learner> GetSelected()
        {

  }*/     
    }
}
