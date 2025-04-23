using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace StatysticalAnalysisApp.Analyzers
{
    public class PopulationAnalyzer : AnalyzerBase
    {
        public override string Analyze(DataTable data, int N)
        {
            // 1. Анализ снижения численности
            var regions = data.AsEnumerable()
                .GroupBy(row => row["Субъект РФ"].ToString())
                .Select(g => new
                {
                    Region = g.Key,
                    Start = Convert.ToDouble(g.First()["Численность (млн)"]),
                    End = Convert.ToDouble(g.Last()["Численность (млн)"]),
                    Change = (Convert.ToDouble(g.Last()["Численность (млн)"]) -
                             Convert.ToDouble(g.First()["Численность (млн)"])) /
                            (double)Convert.ToDouble(g.First()["Численность (млн)"]) * 100
                })
                .Where(r => r.Change < 0)
                .ToList();

            if (!regions.Any())
                return "Нет данных о снижении численности";

            var maxDecline = regions.OrderBy(r => r.Change).First();
            var minDecline = regions.OrderBy(r => r.Change).Last();

            // 2. Прогнозирование для каждого региона
            StringBuilder forecastResult = new StringBuilder();
            var allRegions = data.AsEnumerable()
                .GroupBy(row => row["Субъект РФ"].ToString());

            foreach (var region in allRegions)
            {
                var lastYear = region.Max(r => Convert.ToInt32(r["Год"]));
                var populationData = region
                    .OrderBy(r => Convert.ToInt32(r["Год"]))
                    .Select(r => Convert.ToDouble(r["Численность (млн)"]))
                    .ToList();

                // Скользящая средняя для прогноза
                double forecast = CalculateMovingAverage(populationData, N);
                forecastResult.AppendLine(
                    $"{region.Key}: прогноз на {lastYear + N} год — {forecast:F1} тыс. чел.");
            }

            return $"Макс. снижение: {maxDecline.Region} ({maxDecline.Change:F1}%)\n" +
                   $"Мин. снижение: {minDecline.Region} ({minDecline.Change:F1}%)\n\n" +
                   $"Прогноз на {N} лет:\n{forecastResult}";
        }
        public override double CalculateMovingAverage(List<double> data, int N)
        {
            if (data.Count < N || N <= 0)
                return data.LastOrDefault();

            return data.Skip(data.Count - N).Average();
        }
        public override void DrawChart(DataTable data, Chart chart, int N)  // Теперь принимает N
        {
            chart.Series.Clear();
            var regions = data.AsEnumerable()
                .GroupBy(row => row["Субъект РФ"].ToString());

            // Настройки графика
            chart.ChartAreas[0].AxisX.Title = "Год";
            chart.ChartAreas[0].AxisY.Title = "Численность (млн. чел.)";

            foreach (var region in regions)
            {
                // Исторические данные
                var seriesHistorical = new Series(region.Key)
                {
                    ChartType = SeriesChartType.Line,
                    BorderWidth = 3,
                };

                var populationData = region
                    .OrderBy(r => Convert.ToInt32(r["Год"]))
                    .Select(r => new
                    {
                        Year = Convert.ToInt32(r["Год"]),
                        Value = Convert.ToDouble(r["Численность (млн)"])
                    })
                    .ToList();

                foreach (var point in populationData)
                    seriesHistorical.Points.AddXY(point.Year, point.Value);

                chart.Series.Add(seriesHistorical);

                // Прогноз (если N > 0) if (N > 0 && populationData.Count >= N)
                if (N > 0)
                {
                    double lastValue = populationData.Last().Value;
                    double forecast = populationData
                        .Skip(populationData.Count - N)
                        .Average(p => p.Value);

                    var seriesForecast = new Series($"{region.Key} (прогноз)")
                    {
                        ChartType = SeriesChartType.Line,
                        BorderWidth = 2,
                        Color = Color.Red,
                        BorderDashStyle = ChartDashStyle.Dash
                    };

                    seriesForecast.Points.AddXY(populationData.Last().Year, lastValue);
                    seriesForecast.Points.AddXY(populationData.Last().Year + N, forecast);

                    chart.Series.Add(seriesForecast);
                }
            }
        }
        public override string Analyse(DataTable data)
        {
            var regions = data.AsEnumerable()
                .GroupBy(row => row["Субъект РФ"].ToString())
                .Select(g => new
                {
                    Region = g.Key,
                    StartYear = g.Min(r => Convert.ToInt32(r["Год"])),
                    EndYear = g.Max(r => Convert.ToInt32(r["Год"])),
                    StartPop = Convert.ToDouble(g.First(r => Convert.ToInt32(r["Год"]) == g.Min(x => Convert.ToInt32(x["Год"])))["Численность (млн)"]),
                    EndPop = Convert.ToDouble(g.First(r => Convert.ToInt32(r["Год"]) == g.Max(x => Convert.ToInt32(x["Год"])))["Численность (млн)"]),
                    Change = (Convert.ToDouble(g.First(r => Convert.ToInt32(r["Год"]) == g.Max(x => Convert.ToInt32(x["Год"])))["Численность (млн)"]) -
                            Convert.ToDouble(g.First(r => Convert.ToInt32(r["Год"]) == g.Min(x => Convert.ToInt32(x["Год"])))["Численность (млн)"])) /
                            Convert.ToDouble(g.First(r => Convert.ToInt32(r["Год"]) == g.Min(x => Convert.ToInt32(x["Год"])))["Численность (млн)"]) * 100
                })
                .Where(r => r.Change < 0) // Только снижение
                .ToList();

            if (!regions.Any())
                return "Все регионы показали рост численности";

            var maxDecline = regions.OrderBy(r => r.Change).First();
            var minDecline = regions.OrderBy(r => r.Change).Last();

            return $"За период {maxDecline.StartYear}-{maxDecline.EndYear}:\n" +
                   $"Макс. снижение: {maxDecline.Region} ({maxDecline.Change:F1}%)\n" +
                   $"Мин. снижение: {minDecline.Region} ({minDecline.Change:F1}%)";
        }
    }
}
