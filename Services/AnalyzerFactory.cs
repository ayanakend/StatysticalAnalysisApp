using System;
using StatysticalAnalysisApp.Analyzers;
namespace StatysticalAnalysisApp.Services
{
    public static class AnalyzerFactory
    {
        public static AnalyzerBase CreateAnalyzer(int variant)
        {
            switch (variant)
            {
                case 10:
                    return new InflationAnalyzer();
                case 14:
                    return new PopulationAnalyzer();
                case 16:
                    return new RoadsAnalyzer();
                default:
                    throw new ArgumentException("Неверный вариант");
            }
        }
    }
}
