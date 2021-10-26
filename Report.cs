using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Manager
{
    class Report
    {
        public string Text;
        //data
        public TeamMember Performer;
        public Report(string text, TeamMember performer)
        {
            Text = text;
            Performer = performer;
        }
        public override string ToString()
        {
            return $"{Text} by {Performer.Name}";
        }
    }
}
