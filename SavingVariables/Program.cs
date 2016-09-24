using SavingVariables.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingVariables
{
    class Program
    {
        static void Main(string[] args)
        {
            string lastq = null;
            bool KeepGoing = true;
            while (KeepGoing)
            {
                Console.Write(">>");
                string input = Console.ReadLine();
                Expression newInput = new Expression(input, lastq);
                Console.WriteLine(newInput.Response());
                lastq = input;
            }
        }
    }
}
