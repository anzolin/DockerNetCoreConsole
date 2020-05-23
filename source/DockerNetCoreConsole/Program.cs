using FluentScheduler;
using System;
using System.Threading;

namespace DockerNetCoreConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            JobManager.JobException += JobManager_JobException;

            var registry = new Registry();

            // Executa a cada 5 minutos.
            registry.Schedule<MyFiveMinutesTask>().ToRunNow().AndEvery(5).Minutes();

            // Irá executar todos os dias às 5 da manhã.
            registry.Schedule<MyDailyTask>().ToRunEvery(1).Days().At(5, 0);

            JobManager.Initialize(registry);

            Thread.Sleep(Timeout.Infinite);
        }

        /// <summary>
        /// Tratamento de exceções
        /// </summary>
        /// <param name="obj"></param>
        private static void JobManager_JobException(JobExceptionInfo obj)
        {
            // Log Errors 
        }
    }

    /// <summary>
    /// Tarefa que executa a cada 5 minutos.
    /// </summary>
    public class MyFiveMinutesTask : IJob
    {
        public void Execute()
        {
            Console.WriteLine($" - [{DateTime.Now}] Executando tarefa de 5 em 5 minutos!");
            Thread.Sleep(TimeSpan.FromSeconds(10));
            Console.WriteLine($" - [{DateTime.Now}] Finalizando tarefa de 5 em 5 minutos!");
        }
    }

    /// <summary>
    /// Tarefa que executa diariamente.
    /// </summary>
    public class MyDailyTask : IJob
    {
        public void Execute()
        {
            Console.WriteLine($" - [{DateTime.Now}] Executando tarefa diária!");
            Thread.Sleep(TimeSpan.FromSeconds(10));
            Console.WriteLine($" - [{DateTime.Now}] Finalizando tarefa diária");
        }
    }
}
