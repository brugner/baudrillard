using System;
using System.Collections.Generic;

namespace Baudrillard.Simulator
{
    public class SimulationResult
    {
        public TimeSpan SimulatedTime { get; set; }
        public string ShowableStateVectorsFromTo { get; set; }
        public TimeSpan SimulationTime { get; set; }
        public IEnumerable<ShowableStateVector> ShowableStateVectors { get; set; }
    }
}