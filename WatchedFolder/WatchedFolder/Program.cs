using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Configuration;

namespace WatchedFolder
{
    class Program
    {
        /// <summary>
        /// main 函数
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {  
            IList<string> lists=new List<string>();
            string src = GetAppConfig("src");
            string obj = GetAppConfig("obj");
            if (!Directory.Exists(src)||!Directory.Exists(obj)) { Console.WriteLine("输入的目录不存在，请检查路径名"); Console.ReadKey(); return; }
            lists.Add(src);
            lists.Add(obj);
            Console.WriteLine("监视开始："+lists.Count+"个目录");

            WatchedProcess wcp = new WatchedProcess(lists);
            wcp.WatcherStrat();


            Console.ReadKey();
        }


        /// <summary>
        /// 读取配置信息
        /// </summary>
        /// <param name="strKey"></param>
      public  static string GetAppConfig(string strKey)
        {
            foreach (string key in ConfigurationManager.AppSettings)
            {
                if (key == strKey)
                {
                    return ConfigurationManager.AppSettings[strKey];
                }
            }
            return null;
        }

      


    }
}
