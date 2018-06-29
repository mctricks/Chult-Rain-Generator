using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chult_Rain_Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            RainStateMachine machine = new RainStateMachine("Clear");

            Console.WriteLine(machine.CurrentState.Name);

            for(int i = 0; i < 25; i++)
            {
                machine.Update();

                Console.WriteLine(machine.CurrentState.Name);
            }
        }
    }
}
