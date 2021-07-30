using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using Quartz.Simpl;
using Quartz.Xml;

namespace quartzService
{
    class Program
    {

        //protected void Application_Start(object sender,EventArgs e)
        public static void Application_Start()
        {
            RunpProgramRunExample().GetAwaiter().GetResult();
        }


        static void Main(string[] args)
        {
            Application_Start();

            Console.ReadKey();
        }
        private static async Task RunpProgramRunExample()
        {
            try
            {
                Console.WriteLine("[---------开启调度任务-----------]");
                
                XMLSchedulingDataProcessor processor = new XMLSchedulingDataProcessor(new SimpleTypeLoadHelper());
                StdSchedulerFactory factory = new StdSchedulerFactory();
                IScheduler scheduler = await factory.GetScheduler();
                await processor.ProcessFileAndScheduleJobs("~/quartz_jobs.xml", scheduler);

                await scheduler.Start();

                await Task.Delay(TimeSpan.FromSeconds(60));

                await scheduler.Shutdown();  
            }
            catch (SchedulerException es)
            {
                await Console.Error.WriteLineAsync(es.ToString());
            }
        }
    }
}
