using Baudrillard.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Baudrillard.Core
{
    public abstract class Simulator<TParameters, TStateVector, TShowableStateVector, TResults>
        where TParameters : SimulationParameters
        where TStateVector : StateVector, new()
        where TShowableStateVector : ShowableStateVector
        where TResults : SimulationResult, new()
    {
        public TParameters Parameters { get; set; }
        protected TStateVector PreviousStateVector { get; set; }
        protected TStateVector CurrentStateVector { get; set; }
        protected List<TShowableStateVector> ShowableStateVectors { get; set; }
        protected Random Random { get; } = new Random();

        private const string DISPLAY_EMPTY_VALUE = "-";
        private Stopwatch _stopwatch = new Stopwatch();

        protected Simulator(TParameters parameters)
        {
            Parameters = parameters;
        }

        public SimulationResult Simulate()
        {
            InitRoutine();
            _stopwatch.Start();

            //while (_previousStateVector.Reloj <= _parametros.TiempoSimular)
            for (int i = 0; i < 10; i++)
            {
                TimeRoutine();
                EventRoutine();

                if (IsValidAsShowableStateVector())
                {
                    AddStateVectorToShowables();
                }

                PreviousStateVector = CurrentStateVector;
                CurrentStateVector = new TStateVector();
            }

            _stopwatch.Stop();

            return GetResults();
        }

        public Task<SimulationResult> SimulateAsync()
        {
            return Task.Run(() =>
            {
                return Simulate();
            });
        }

        public virtual TResults GetResults()
        {
            return new TResults
            {
                SimulatedTime = Parameters.TimeToSimulate,
                ShowableStateVectorsFromTo = Parameters.ShowStateVectorsFrom.ToString() + " - " + Parameters.ShowStateVectorsTo.ToString(),
                SimulationTime = TimeSpan.FromMilliseconds(_stopwatch.ElapsedMilliseconds),
                ShowableStateVectors = ShowableStateVectors
            };
        }

        public DataTable GetShowableStateVectorsAsDataTable()
        {
            var dtVectors = CreateDataTable();

            foreach (var vector in ShowableStateVectors)
            {
                var row = dtVectors.NewRow();

                foreach (var property in GetPropertiesOrdered())
                {
                    var title = GetPropertyDisplayTitle(property);
                    var value = property.GetValue(vector);

                    if (value != null)
                    {
                        row[title] = value;
                    }
                    else
                    {
                        row[title] = DISPLAY_EMPTY_VALUE;
                    }
                }

                dtVectors.Rows.Add(row);
            }

            return dtVectors;
        }

        public Dictionary<string, int> GetShowableStateVectorDataGridConfiguration()
        {
            var config = new Dictionary<string, int>();

            foreach (var property in GetPropertiesOrdered())
            {
                string title = GetPropertyDisplayTitle(property);

                if (property.PropertyType == typeof(TimeSpan) || property.PropertyType == typeof(int) || Nullable.GetUnderlyingType(property.PropertyType) != null)
                    config.Add(title, 32);
                else
                    config.Add(title, 16);
            }

            return config;
        }

        protected abstract void InitRoutine();
        protected abstract void TimeRoutine();
        protected abstract void EventRoutine();
        protected abstract void CopyStateVector();
        protected abstract void AddStateVectorToShowables();

        private bool IsValidAsShowableStateVector()
        {
            return CurrentStateVector.Clock >= Parameters.ShowStateVectorsFrom && CurrentStateVector.Clock <= Parameters.ShowStateVectorsTo;
        }

        private DataTable CreateDataTable()
        {
            var dtVectors = new DataTable();

            foreach (var property in GetPropertiesOrdered())
            {
                string title = GetPropertyDisplayTitle(property);
                dtVectors.Columns.Add(new DataColumn(title, typeof(string)));
            }

            return dtVectors;
        }

        private IEnumerable<PropertyInfo> GetPropertiesOrdered()
        {
            return typeof(TShowableStateVector).GetProperties()
                .Select(x => new
                {
                    Property = x,
                    Attribute = (DisplayAttribute)Attribute.GetCustomAttribute(x, typeof(DisplayAttribute), true)
                })
                .OrderBy(x => x.Attribute != null ? x.Attribute.Order : 999)
                .Select(x => x.Property);
        }

        private string GetPropertyDisplayTitle(PropertyInfo property)
        {
            var title = property.GetCustomAttribute<DisplayAttribute>(true)?.Title;

            return string.IsNullOrEmpty(title) ? property.Name : title;
        }
    }
}
