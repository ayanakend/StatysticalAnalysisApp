namespace StatysticalAnalysisApp
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.comboBoxDataset = new System.Windows.Forms.ComboBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label3 = new System.Windows.Forms.Label();
            this.labelResult = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.textBoxN = new System.Windows.Forms.TextBox();
            this.buttonAnalyze = new System.Windows.Forms.Button();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxDataset
            // 
            this.comboBoxDataset.FormattingEnabled = true;
            this.comboBoxDataset.Location = new System.Drawing.Point(880, 27);
            this.comboBoxDataset.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBoxDataset.Name = "comboBoxDataset";
            this.comboBoxDataset.Size = new System.Drawing.Size(274, 28);
            this.comboBoxDataset.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(667, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(216, 20);
            this.label3.TabIndex = 15;
            this.label3.Text = "Выберите вариант данных:";
            // 
            // labelResult
            // 
            this.labelResult.AutoSize = true;
            this.labelResult.Location = new System.Drawing.Point(677, 116);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(195, 20);
            this.labelResult.TabIndex = 14;
            this.labelResult.Text = "РЕЗУЛЬТАТ БУДЕТ ТУТ!";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(631, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(255, 20);
            this.label1.TabIndex = 13;
            this.label1.Text = "Введите число  N для прогноза:";
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(14, 15);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(567, 226);
            this.dataGridView.TabIndex = 12;
            // 
            // textBoxN
            // 
            this.textBoxN.Location = new System.Drawing.Point(880, 75);
            this.textBoxN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxN.Name = "textBoxN";
            this.textBoxN.Size = new System.Drawing.Size(112, 26);
            this.textBoxN.TabIndex = 11;
            // 
            // buttonAnalyze
            // 
            this.buttonAnalyze.Location = new System.Drawing.Point(1009, 75);
            this.buttonAnalyze.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonAnalyze.Name = "buttonAnalyze";
            this.buttonAnalyze.Size = new System.Drawing.Size(145, 29);
            this.buttonAnalyze.TabIndex = 10;
            this.buttonAnalyze.Text = "Анализировать";
            this.buttonAnalyze.UseVisualStyleBackColor = true;
            // 
            // chart
            // 
            chartArea1.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart.Legends.Add(legend1);
            this.chart.Location = new System.Drawing.Point(14, 249);
            this.chart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chart.Name = "chart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart.Series.Add(series1);
            this.chart.Size = new System.Drawing.Size(1211, 571);
            this.chart.TabIndex = 16;
            this.chart.Text = "chart1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1237, 877);
            this.Controls.Add(this.chart);
            this.Controls.Add(this.comboBoxDataset);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelResult);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.textBoxN);
            this.Controls.Add(this.buttonAnalyze);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Статистический анализ";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxDataset;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelResult;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.TextBox textBoxN;
        private System.Windows.Forms.Button buttonAnalyze;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
    }
}

