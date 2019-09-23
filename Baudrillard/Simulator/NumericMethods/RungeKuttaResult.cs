using System.Collections.Generic;

namespace Baudrillard.Simulator.NumericMethods
{
    public abstract class RungeKuttaResult<TVector> where TVector : RungeKuttaVector
    {
        public IList<TVector> Vectors { get; set; }
        public float Value { get; set; }

        public RungeKuttaResult()
        {
            Vectors = new List<TVector>();
        }
    }
}