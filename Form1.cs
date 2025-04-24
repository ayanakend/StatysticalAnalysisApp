using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using StatysticalAnalysisApp.Services;

namespace StatysticalAnalysisApp
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBoxDataset.Items.AddRange(new[] { "10: Инфляция", "14: Население", "16: Дороги" });
            comboBoxDataset.SelectedIndexChanged += ComboBoxDataset_SelectedIndexChanged;
        }
        private void ComboBoxDataset_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataForSelectedVariant();
        }
        private void LoadDataForSelectedVariant()
        {
            if (comboBoxDataset.SelectedItem == null) return;

            int variant = int.Parse(comboBoxDataset.SelectedItem.ToString().Split(':')[0]);
            DataTable data = LoadExcelData(GetFilePathForVariant(variant));
            dataGridView.DataSource = data;
            // Получаем значение N из textBoxN
            int N = 0;
            AnalyzerFactory.CreateAnalyzer(variant).DrawChart(data, chart, N);
            labelResult.Text = AnalyzerFactory.CreateAnalyzer(variant).Analyse(data);
        }
        private DataTable LoadExcelData(string filePath)
        {
            ExcelPackage.License.SetNonCommercialOrganization("My Noncommercial organization");
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                DataTable dt = new DataTable();

                // Заголовки столбцов
                for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                    dt.Columns.Add(worksheet.Cells[1, col].Text);

                // Данные
                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    DataRow dr = dt.NewRow();
                    for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                        dr[col - 1] = worksheet.Cells[row, col].Text;
                    dt.Rows.Add(dr);
                }
                return dt;
            }
        }

    }
}
