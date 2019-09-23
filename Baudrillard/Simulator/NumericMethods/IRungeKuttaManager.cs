namespace Baudrillard.Simulator.NumericMethods
{
    public interface IRungeKuttaManager<TRungeKuttaParameters, TRungeKuttaResult, TVector> where TVector : RungeKuttaVector where TRungeKuttaParameters : RungeKuttaParameters where TRungeKuttaResult : RungeKuttaResult<TVector>
    {
        TRungeKuttaResult RungeKutta(TRungeKuttaParameters parameters);
    }
}
