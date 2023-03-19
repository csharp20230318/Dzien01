using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PararellTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PararellOper oper = new PararellOper();
            //oper.LoopNoPararell();
            //oper.LoopWithPararell();
            //oper.LoopWithPararellForEach();
            //oper.LoopWithPararellBreakStop();
            //oper.ParallelInvoke();
            oper.LoopParallelCancel();

            Console.ReadKey();
        }
    }
}
