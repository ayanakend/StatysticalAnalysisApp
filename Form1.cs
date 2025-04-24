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
    }
}
