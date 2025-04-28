using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace StatysticalAnalysisApp.Analyzers
{
    public abstract class AnalyzerBase
    {
        public abstract string Analyze(DataTable data, int N);
        public abstract void DrawChart(DataTable data, Chart chart, int N);
        public abstract string Analyse(DataTable data);
        public abstract double CalculateMovingAverage(List<double> data, int N);
    }
}
