using OmsUI.test;
using OmsUI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OmsUI
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
          // Application.Run(new FormManagerInfo());
           Application.Run(new FormLogin());
          // Application.Run(new FormExportMemberInfo());
          // Application.Run(new FormMain());
          // Application.Run(new FormMemberTypeInfo());
          // Application.Run(new FormMemberList());
          // Application.Run(new FormDishInfo());
         //  Application.Run(new FormHallInfo());
           //Application.Run(new FormTableInfo());
        }
    }
}
