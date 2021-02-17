using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamInvigilatorProject
{
    public class Learner
    {
        dbEdit editor = new dbEdit();
        public string[] name { get; set; }
        private Guid? id { get; set; }

        public string time { get; set; }

        public Learner(Guid id)
        {
            this.id = id;
            this.name = editor.getName(id);
            this.time = editor.getTime(id);
        }

        





    }
}
