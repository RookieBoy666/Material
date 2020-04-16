using System;
using System.Windows.Forms;

namespace Material
{
    static class Program
    {

        //private static string connectionString = ConfigurationManager.AppSettings["connectionstring"];
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new sMaterialNo());
        }
    }
}
