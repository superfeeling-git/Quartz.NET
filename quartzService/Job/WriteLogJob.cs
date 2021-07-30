using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quartzService.Job
{
    [PersistJobDataAfterExecution]//保存执行状态
    [DisallowConcurrentExecution]//不允许并发执行
    class WriteLogJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +":执行调度");
            WriteLogFun("");
            await Task.Delay(1);
        }

        /// <summary>
        /// Log 日志
        /// </summary>
        /// <param name="logInfo"></param>
        private void WriteLogFun(string logInfo)
        {
            string currentPath = System.AppDomain.CurrentDomain.BaseDirectory;
            string logDirectory = currentPath + "MyServiceLog_" + DateTime.Now.ToString("yyyy");
            string logFileName = logDirectory + "\\" + DateTime.Today.ToString("yyyyMMdd") + "-1.txt";
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
            using (StreamWriter sw = new StreamWriter(logFileName, true))
            {
                sw.WriteLine(System.DateTime.Now.ToString() + " " + logInfo);
                sw.Flush();
                sw.Close();
            }
        }
    }
}
