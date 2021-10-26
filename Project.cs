using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Manager
{    
    class Project
    {
        public enum State
        {
            Проект = 1,
            Исполнение,
            Закрыт
        }
        public string Description;
        public DateTime Deadline;
        public TeamMember Initiator;
        public TeamMember TeamLead;
        public List<Task> Tasks;
        public State Status;
        public Project(string description, DateTime deadline, TeamMember initiator, TeamMember teamLead, int taskNumber)
        {
            Description = description;
            Deadline = deadline;
            Initiator = initiator;
            initiator.Type = TeamMember.PerfomType.Заказчик;
            TeamLead = teamLead;
            teamLead.Type = TeamMember.PerfomType.Тимлид;
            Tasks = new List<Task>();
            while(Tasks.Count != taskNumber)
            {
                Tasks.Add(new Task((Tasks.Count + 1).ToString()));
            }
            Status = (State)1;
        }
        public string GetCurrentTasks()
        {
            string output = "";
            foreach(Task task in Tasks)
            {
                output += task + "\n";
            }
            return output;
        }
    }
}
