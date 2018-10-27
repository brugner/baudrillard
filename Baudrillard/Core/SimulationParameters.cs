using System;

namespace Baudrillard.Core
{
    public class SimulationParameters
    {
        public TimeSpan TimeToSimulate { get; set; }
        public TimeSpan ShowStateVectorsFrom { get; set; }
        public TimeSpan ShowStateVectorsTo { get; set; }
    }
}