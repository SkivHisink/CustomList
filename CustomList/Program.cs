using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomList
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CList<int> test = new CList<int>();
            for (int i = 0; i < 1000000; i++)
            {
                test.Add(i);
                test[i] = i - 1;
            }
        }
    }
}
