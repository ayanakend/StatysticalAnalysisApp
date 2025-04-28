using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace StatysticalAnalysisApp.Analyzers
{
    public class RoadsAnalyzer : AnalyzerBase
    {
        public override string Analyze(DataTable data, int N)
        {
            // 1. Анализ улучшения состояния дорог
            var regions = data.AsEnumerable()
                .GroupBy(row => row["Регион"].ToString())
                .Select(g => new
                {
                    Region = g.Key,
                    StartValue = Convert.ToDouble(g.First()["% плохих дорог"]),
                    EndValue = Convert.ToDouble(g.Last()["% плохих дорог"]),
                    Improvement = Convert.ToDouble(g.First()["% плохих дорог"]) - Convert.ToDouble(g.Last()["% плохих дорог"])
                })
                .Where(r => r.Improvement > 0) // Только улучшение
                .ToList();

            if (!regions.Any())
                return "Нет данных об улучшении состояния дорог";

            var maxImprovement = regions.OrderByDescending(r => r.Improvement).First();
            var minImprovement = regions.OrderByDescending(r => r.Improvement).Last();

            // 2. Прогнозирование для каждого региона
            StringBuilder forecastResult = new StringBuilder();
            var allRegions = data.AsEnumerable()
                .GroupBy(row => row["Регион"].ToString());

            foreach (var region in allRegions)
            {
                var lastYear = region.Max(r => Convert.ToInt32(r["Год"]));
                var roadData = region
                    .OrderBy(r => Convert.ToInt32(r["Год"]))
                    .Select(r => Convert.ToDouble(r["% плохих дорог"]))
                    .ToList();

                // Скользящая средняя для прогноза
                double forecast = CalculateMovingAverage(roadData, N);
                forecast = Math.Max(0, Math.Min(100, forecast)); // Ограничение 0-100 %
                forecastResult.AppendLine($"{region.Key}: прогноз на {lastYear + N} год — {forecast:F1}% плохих дорог");
            }

            return $"Макс. улучшение: {maxImprovement.Region} (-{maxImprovement.Improvement:F1}%)\n" +
                   $"Мин. улучшение: {minImprovement.Region} (-{minImprovement.Improvement:F1}%)\n\n" +
                   $"Прогноз на {N} лет:\n{forecastResult}";
        }

        public override double CalculateMovingAverage(List<double> data, int N)
        {
            if (data.Count < 2 || N <= 0)
                return data.LastOrDefault();

            // Берем минимум из N и доступных данных
            int actualN = Math.Min(N, data.Count);
            return data.Skip(data.Count - actualN).Average();
        }

        public override void DrawChart(DataTable data, Chart chart, int N)
        {
            chart.Series.Clear();
            var regions = data.AsEnumerable()
                .GroupBy(row => row["Регион"].ToString());

            // Настройки графика
            chart.ChartAreas[0].AxisX.Title = "Год";
            chart.ChartAreas[0].AxisY.Title = "% плохих дорог";
            chart.ChartAreas[0].AxisY.Minimum = 0;
            chart.ChartAreas[0].AxisY.Maximum = 100;

            foreach (var region in regions)
            {
                // Исторические данные
                var seriesHistorical = new Series(region.Key)
                {
                    ChartType = SeriesChartType.Line,
                    BorderWidth = 3,
                };

                var roadData = region
                    .OrderBy(r => Convert.ToInt32(r["Год"]))
                    .Select(r => new
                    {
                        Year = Convert.ToInt32(r["Год"]),
                        Value = Convert.ToDouble(r["% плохих дорог"])
                    })
                    .ToList();

                foreach (var point in roadData)
                    seriesHistorical.Points.AddXY(point.Year, point.Value);

                chart.Series.Add(seriesHistorical);

                // Прогноз (если N > 0 и есть данные)
                if (N > 0 && roadData.Count >= 2)
                {
                    double lastValue = roadData.Last().Value;
                    double forecast = CalculateMovingAverage(roadData.Select(p => p.Value).ToList(), N);

                    var seriesForecast = new Series($"{region.Key} (прогноз)")
                    {
                        ChartType = SeriesChartType.Line,
                        BorderWidth = 2,
                        Color = Color.Red,
                        BorderDashStyle = ChartDashStyle.Dash
                    };

                    seriesForecast.Points.AddXY(roadData.Last().Year, lastValue);
                    seriesForecast.Points.AddXY(roadData.Last().Year + N, forecast);
                    chart.Series.Add(seriesForecast);
                }
            }
        }

        public override string Analyse(DataTable data)
        {
            var regions = data.AsEnumerable()
                .GroupBy(row => row["Регион"].ToString())
                .Select(g => new
                {
                    Region = g.Key,
                    StartValue = Convert.ToDouble(g.First()["% плохих дорог"]),
                    EndValue = Convert.ToDouble(g.Last()["% плохих дорог"]),
                    Improvement = Convert.ToDouble(g.First()["% плохих дорог"]) - Convert.ToDouble(g.Last()["% плохих дорог"])
                })
                .Where(r => r.Improvement > 0) // Только улучшение
                .ToList();

            if (!regions.Any())
                return "Нет данных об улучшении состояния дорог";

            var maxImprovement = regions.OrderByDescending(r => r.Improvement).First();
            var minImprovement = regions.OrderByDescending(r => r.Improvement).Last();

            return $"Макс. улучшение: {maxImprovement.Region} (-{maxImprovement.Improvement:F1}%)\n" +
                   $"Мин. улучшение: {minImprovement.Region} (-{minImprovement.Improvement:F1}%)";
        }
    }
}