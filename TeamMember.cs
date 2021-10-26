namespace Task_Manager
{
    class TeamMember
    {
        public string Name;
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
