using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chult_Rain_Generator
{
    class RainStateMachine
    {
        public List<RainState> States { get; private set; }

        public RainState CurrentState { get; private set; }

        public RainStateMachine(string initialStateName)
        {
            States = new List<RainState>
            {
                new RainState("Clear"),
                new RainState("Light Rain"),
                new RainState("Heavy Rain"),
                new RainState("Tropical Storm")
            };

            Get("Clear").Link(Get("Clear"), 40);
            Get("Clear").Link(Get("Light"), 40);
            Get("Clear").Link(Get("Heavy"), 19);
            Get("Clear").Link(Get("Storm"), 01);

            Get("Light").Link(Get("Clear"), 30);
            Get("Light").Link(Get("Light"), 50);
            Get("Light").Link(Get("Heavy"), 18);
            Get("Light").Link(Get("Storm"), 02);

            Get("Heavy").Link(Get("Clear"), 25);
            Get("Heavy").Link(Get("Light"), 50);
            Get("Heavy").Link(Get("Heavy"), 20);
            Get("Heavy").Link(Get("Storm"), 05);

            Get("Storm").Link(Get("Clear"), 05);
            Get("Storm").Link(Get("Light"), 15);
            Get("Storm").Link(Get("Heavy"), 75);
            Get("Storm").Link(Get("Storm"), 05);

            CurrentState = Get(initialStateName);

            if(CurrentState == null)
            {
                throw new Exception("The given initial state of '" + initialStateName + "' could not be found.");
            }

            bool valid = true;

            foreach(var state in States)
            {
                if(!state.Valid)
                {
                    valid = false;
                    break;
                }
            }

            if(!valid)
            {
                throw new Exception("The given states are not valid.");
            }
        }

        /// <summary>
        /// Randomize the value of Current State, following the probabilistic state transition diagram implemented in the constructor.
        /// </summary>
        /// <returns></returns>
        public void Update() => Update((new Random()).Next(1, 101));

        /// <summary>
        /// Follows the indicated path along the probabilistic state transition diagram implemented in the constructor.
        /// </summary>
        /// <returns></returns>
        private void Update(int roll)
        {
            int total = 0;

            foreach(var state in CurrentState.LinkedStates)
            {
                if(roll <= state.Value + total)
                {
                    CurrentState = state.Key;
                    return;
                }
                else
                {
                    total += state.Value;
                }
            }

            throw new Exception("A state cannot be found corresponding to the given value");
        }

        /// <summary>
        /// Returns the first RainState in States that contains the given name.
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        private RainState Get(string name) => States.FirstOrDefault(x => x.Name.ToLower().Contains(name.ToLower()));
    }
}
