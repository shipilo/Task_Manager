using System;

namespace Task_Manager
{
    class Program
    {            
        static Random rnd = new Random();
        static void Main(string[] args)
        {
            TeamMember[] team =
            {
                new TeamMember("Alex"),
                new TeamMember("Max"),
                new TeamMember("Jane"),
                new TeamMember("Mike"),
                new TeamMember("Leo"),
                new TeamMember("Kate"),
                new TeamMember("Andrew"),
                new TeamMember("Dina"),
                new TeamMember("Barbara"),
                new TeamMember("Lena"),
                new TeamMember("John"),
                new TeamMember("Polly")
            };

            int taskNumber = 0;

            Console.Write("Количество задач: ");
            taskNumber = Convert.ToInt32(Console.ReadLine());
            Project project001 = new Project("001", new DateTime(), team[0], team[1], taskNumber);

            Console.Write("Тимлид назначает задачи");
            for(int i = 0, j = 2; i < project001.Tasks.Count; i++, j++)
            {
                project001.Tasks[i].Initiator = project001.Initiator;
                project001.Tasks[i].Performer = team[j];
                if (j == team.Length - 1)
                {
                    j = 2;
                }
                TimerTick(200);
                Console.Write(".");
            }

            Console.WriteLine("\n" + project001.GetCurrentTasks());
            Console.WriteLine("Все задачи распределены.");

            project001.Status++;

            int progress = 0;
            int speed = 1000;
            bool notDeleted = true;
            while (progress < project001.Tasks.Count)
            {
                Console.WriteLine("\nОбновление информации...");
                for(int i = 0; i < project001.Tasks.Count; i++)
                {
                    TimerTick(speed);

                    if (project001.Tasks[i].Status == Task.State.Назначена)
                    {
                        if (Randomizer(10))
                        {
                            if (Randomizer(50))
                            {
                                project001.Tasks[i].Status = Task.State.Делегирование;
                            }
                            else
                            {
                                project001.Tasks[i].Status = Task.State.Отклонение;
                            }
                        }
                        else
                        {
                            project001.Tasks[i].Status = Task.State.В_работе;
                        }
                    }
                    else if (project001.Tasks[i].Status == Task.State.В_работе)
                    {
                        if (Randomizer(50))
                        {
                            project001.Tasks[i].Reports.Push(new Report(project001.Tasks[i].Description + " ~отчёт~", project001.Tasks[i].Performer));
                            project001.Tasks[i].Status = Task.State.На_проверке;
                        }
                    }
                    else if (project001.Tasks[i].Status == Task.State.На_проверке)
                    {
                        if (Randomizer(50))
                        {
                            Console.WriteLine($"{project001.Tasks[i].Reports.Peek()} утверждён.");
                            project001.Tasks[i].Status = Task.State.Выполнена;
                            progress++;
                        }
                    }
                    else if (project001.Tasks[i].Status == Task.State.Делегирование)
                    {
                        project001.Tasks[i].Performer = team[rnd.Next(2, team.Length - 1)];
                        project001.Tasks[i].Status = Task.State.Назначена;
                    }
                    else if (project001.Tasks[i].Status == Task.State.Отклонение)
                    {
                        if (Randomizer(50))
                        {
                            project001.Tasks[i].Performer = team[rnd.Next(2, team.Length - 1)];
                            project001.Tasks[i].Status = Task.State.Назначена;
                        }
                        else
                        {
                            project001.Tasks.RemoveAt(i);
                            i--;
                            Console.WriteLine($"{project001.Tasks[i].Description}. (задача удалена)");
                            notDeleted = false;
                        }
                    }

                    if (notDeleted) Console.WriteLine(project001.Tasks[i]);
                    notDeleted = true;

                    TimerTick(speed);
                }
            }

            project001.Status++;

            Console.WriteLine($"\nВсе задачи выполнены.\nПроект {project001.Description} закрыт.");

            Console.ReadLine();
        }
        static void TimerTick(int mSeconds)
        {
            DateTime startPosition = DateTime.Now;
            while((DateTime.Now - startPosition).TotalMilliseconds < mSeconds) { }
        }
        static bool Randomizer(int chance)
        {
            return (rnd.Next(0, 100 / chance) == 0) ? true : false;
        }
    }
}
