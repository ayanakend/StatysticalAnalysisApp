using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
    }
}
