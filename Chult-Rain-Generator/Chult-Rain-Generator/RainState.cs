using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chult_Rain_Generator
{
    class RainState
    {
        public Dictionary<RainState, int> LinkedStates { get; private set; }

        public string Name { get; private set; }

        public RainState(string name)
        {
            LinkedStates = new Dictionary<RainState, int>();
            Name = name;
        }

        public bool Valid
        {
            get
            {
                int total = 0;
                foreach(var state in LinkedStates)
                {
                    total += state.Value;
                }
                return total == 100;
            }
        }

        public void Link(RainState state, int probability) => LinkedStates.Add(state, probability);
    }
}
