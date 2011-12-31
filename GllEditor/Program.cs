using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GrassLikeLanguage.Editor
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
			using (var form = new FormMain())
			{
				Application.Run(form);
			}
        }
    }
}
