using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace StatysticalAnalysisApp.Analyzers
{
    public class InflationAnalyzer : AnalyzerBase
    {
        public override string Analyze(DataTable data, int N)
        {
            // 1. Проверка данных 
            if (data.Rows.Count < 2)
                return "Недостаточно данных для анализа";

            // 2. Расчет средней инфляции 
            double lastInflation = Convert.ToDouble(data.Rows[data.Rows.Count - 1]["Уровень инфляции (%)"]);
            double avgInflation = data.AsEnumerable()
                .Skip(data.Rows.Count - Math.Min(N, data.Rows.Count))
                .Average(row => Convert.ToDouble(row["Уровень инфляции (%)"]));

            // 3. Прогноз инфляции 
            double forecastInflation = CalculateMovingAverage(
                data.AsEnumerable()
                    .Select(row => Convert.ToDouble(row["Уровень инфляции (%)"]))
                    .ToList(),
                N
            );

            // 4. Расчет стоимости товара через N лет 
            double currentPrice = 1000; // Базовая цена товара (можно заменить на ввод пользователя) 
            double futurePrice = currentPrice * Math.Pow(1 + forecastInflation /100, N);

            // 5. Формирование результата 
            return $"Средняя инфляция за последние {N} лет: {avgInflation:F1}%\n" +
                   $"Прогноз инфляции на {N} лет вперед: {forecastInflation: F1}%\n\n" +
                   $"Пример расчета:\n" +
                   $"Текущая цена товара: {currentPrice} руб.\n" +
                   $"Через {N} лет: {futurePrice:F0} руб. (при сохранении тенденции)";
        }
        public override double CalculateMovingAverage(List<double> data, int N)
        {
            if (N <= 0 || data.Count == 0)
                return data.LastOrDefault();

            int actualN = Math.Min(N, data.Count);
            return data.Skip(data.Count - actualN).Average();
        }
        public override void DrawChart(DataTable data, Chart chart, int N)
        {
            chart.Series.Clear();
            chart.ChartAreas[0].AxisX.Title = "Год";
            chart.ChartAreas[0].AxisY.Title = "Уровень инфляции (%)";

            // 1. Подготовка данных 
            var years = data.AsEnumerable()
                .Select(row => Convert.ToInt32(row["Год"]))
                .ToList();
            var inflations = data.AsEnumerable()
                .Select(row => Convert.ToDouble(row["Уровень инфляции (%)"]))
                .ToList();

            // 2. Исторические данные (синяя линия) 
            var historicalSeries = new Series("Фактическая инфляция")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 3
            };

            for (int i = 0; i < years.Count; i++)
            {
                historicalSeries.Points.AddXY(years[i], inflations[i]);
            }
            chart.Series.Add(historicalSeries);

            // 3. Прогноз (красная пунктирная линия) 
            if (N > 0 && years.Count >= 2)
            {
                // Рассчитываем прогноз 
                double lastInflation = inflations.Last();
                double forecast = inflations
                    .Skip(inflations.Count - Math.Min(N, inflations.Count))
                    .Average();

                // Создаем серию прогноза 
                var forecastSeries = new Series("Прогноз инфляции")
                {
                    ChartType = SeriesChartType.Line,
                    Color = Color.Red,
                    BorderWidth = 2,
                    BorderDashStyle = ChartDashStyle.Dash
                };

                // Соединяем последнюю точку данных с прогнозом 
                forecastSeries.Points.AddXY(years.Last(), lastInflation);
                forecastSeries.Points.AddXY(years.Last() + N, forecast);

                chart.Series.Add(forecastSeries);
            }
        }
        public override string Analyse(DataTable data)
        {
            return $"ДАННЫЕ В ТАБЛИЦЕ!";
        }
    }
}
