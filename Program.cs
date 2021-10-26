using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            int progress = 0;
            int speed = 1000;
            bool notDeleted = true;
            while (progress < project001.Tasks.Count)
            {
                Console.WriteLine("\nОбновление информации...");
                foreach (Task task in project001.Tasks)
                {
                    TimerTick(speed);

                    if (task.Status == Task.State.Назначена)
                    {
                        if (Randomizer(10))
                        {
                            if (Randomizer(50))
                            {
                                task.Status = Task.State.Делегирование;
                            }
                            else
                            {
                                task.Status = Task.State.Отклонение;
                            }
                        }
                        else
                        {
                            task.Status = Task.State.В_работе;
                        }
                    }
                    else if (task.Status == Task.State.В_работе)
                    {
                        if (Randomizer(50))
                        {
                            task.Reports.Push(new Report(task.Description + " ~отчёт~", task.Performer));
                            task.Status = Task.State.На_проверке;
                        }
                    }
                    else if (task.Status == Task.State.На_проверке)
                    {
                        if (Randomizer(50))
                        {
                            Console.WriteLine($"{task.Reports.Peek()} утверждён.");
                            task.Status = Task.State.Выполнена;
                            progress++;
                        }
                    }
                    else if (task.Status == Task.State.Делегирование)
                    {
                        task.Performer = team[rnd.Next(2, team.Length - 1)];
                        task.Status = Task.State.Назначена;
                    }
                    else if (task.Status == Task.State.Отклонение)
                    {
                        if (Randomizer(50))
                        {
                            task.Performer = team[rnd.Next(2, team.Length - 1)];
                            task.Status = Task.State.Назначена;
                        }
                        else
                        {
                            project001.Tasks.Remove(task);
                            Console.WriteLine($"{task.Description}. (задача удалена)");
                            notDeleted = false;
                        }
                    }

                    if (notDeleted) Console.WriteLine(task);
                    notDeleted = true;

                    TimerTick(speed);
                }
            }

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
