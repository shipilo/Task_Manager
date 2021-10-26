using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Manager
{
    class TeamMember
    {
        public string Name;
        public Task Task;
        public PerfomType Type;
        public enum PerfomType
        {
            Заказчик = 1,
            Тимлид,
            Исполнитель            
        }
        public TeamMember(string name)
        {
            Name = name;
        }
    }
}
