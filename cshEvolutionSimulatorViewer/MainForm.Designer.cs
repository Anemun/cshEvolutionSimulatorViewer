namespace cshEvolutionSimulatorViewer
{
    partial class MainForm
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
            this.panel_controls = new System.Windows.Forms.Panel();
            this.label_loadData = new System.Windows.Forms.Label();
            this.button_setPath = new System.Windows.Forms.Button();
            this.button_startSimulation = new System.Windows.Forms.Button();
            this.panel_botData = new System.Windows.Forms.Panel();
            this.label_tps_Title = new System.Windows.Forms.Label();
            this.label_botCount_Title = new System.Windows.Forms.Label();
            this.label_tickIndex_Title = new System.Windows.Forms.Label();
            this.label_tps = new System.Windows.Forms.Label();
            this.label_botCount = new System.Windows.Forms.Label();
            this.label_tickIndex = new System.Windows.Forms.Label();
            this.panel_view = new System.Windows.Forms.Panel();
            this.dialog_loadData = new System.Windows.Forms.FolderBrowserDialog();
            this.label_worldSize_title = new System.Windows.Forms.Label();
            this.label_worldSize = new System.Windows.Forms.Label();
            this.panel_controls.SuspendLayout();
            this.panel_botData.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_controls
            // 
            this.panel_controls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_controls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_controls.Controls.Add(this.label_loadData);
            this.panel_controls.Controls.Add(this.button_setPath);
            this.panel_controls.Controls.Add(this.button_startSimulation);
            this.panel_controls.Location = new System.Drawing.Point(818, 12);
            this.panel_controls.Name = "panel_controls";
            this.panel_controls.Size = new System.Drawing.Size(354, 271);
            this.panel_controls.TabIndex = 1;
            // 
            // label_loadData
            // 
            this.label_loadData.AutoSize = true;
            this.label_loadData.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label_loadData.Location = new System.Drawing.Point(6, 34);
            this.label_loadData.Name = "label_loadData";
            this.label_loadData.Size = new System.Drawing.Size(149, 13);
            this.label_loadData.TabIndex = 2;
            this.label_loadData.Text = "load some data before starting";
            // 
            // button_setPath
            // 
            this.button_setPath.Location = new System.Drawing.Point(6, 4);
            this.button_setPath.Name = "button_setPath";
            this.button_setPath.Size = new System.Drawing.Size(75, 23);
            this.button_setPath.TabIndex = 1;
            this.button_setPath.Text = "Load data";
            this.button_setPath.UseVisualStyleBackColor = true;
            this.button_setPath.Click += new System.EventHandler(this.button_setPath_Click);
            // 
            // button_startSimulation
            // 
            this.button_startSimulation.Enabled = false;
            this.button_startSimulation.Location = new System.Drawing.Point(274, 243);
            this.button_startSimulation.Name = "button_startSimulation";
            this.button_startSimulation.Size = new System.Drawing.Size(75, 23);
            this.button_startSimulation.TabIndex = 0;
            this.button_startSimulation.Text = "START";
            this.button_startSimulation.UseVisualStyleBackColor = true;
            this.button_startSimulation.Click += new System.EventHandler(this.button_startSimulation_Click);
            // 
            // panel_botData
            // 
            this.panel_botData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_botData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_botData.Controls.Add(this.label_worldSize_title);
            this.panel_botData.Controls.Add(this.label_tps_Title);
            this.panel_botData.Controls.Add(this.label_botCount_Title);
            this.panel_botData.Controls.Add(this.label_tickIndex_Title);
            this.panel_botData.Controls.Add(this.label_tps);
            this.panel_botData.Controls.Add(this.label_worldSize);
            this.panel_botData.Controls.Add(this.label_botCount);
            this.panel_botData.Controls.Add(this.label_tickIndex);
            this.panel_botData.Location = new System.Drawing.Point(818, 289);
            this.panel_botData.Name = "panel_botData";
            this.panel_botData.Size = new System.Drawing.Size(354, 658);
            this.panel_botData.TabIndex = 2;
            // 
            // label_tps_Title
            // 
            this.label_tps_Title.AutoSize = true;
            this.label_tps_Title.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label_tps_Title.Location = new System.Drawing.Point(237, 0);
            this.label_tps_Title.Name = "label_tps_Title";
            this.label_tps_Title.Size = new System.Drawing.Size(58, 13);
            this.label_tps_Title.TabIndex = 1;
            this.label_tps_Title.Text = "Ticks/sec:";
            this.label_tps_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_botCount_Title
            // 
            this.label_botCount_Title.AutoSize = true;
            this.label_botCount_Title.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label_botCount_Title.Location = new System.Drawing.Point(3, 13);
            this.label_botCount_Title.Name = "label_botCount_Title";
            this.label_botCount_Title.Size = new System.Drawing.Size(56, 13);
            this.label_botCount_Title.TabIndex = 1;
            this.label_botCount_Title.Text = "Bot count:";
            this.label_botCount_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_tickIndex_Title
            // 
            this.label_tickIndex_Title.AutoSize = true;
            this.label_tickIndex_Title.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label_tickIndex_Title.Location = new System.Drawing.Point(3, 0);
            this.label_tickIndex_Title.Name = "label_tickIndex_Title";
            this.label_tickIndex_Title.Size = new System.Drawing.Size(31, 13);
            this.label_tickIndex_Title.TabIndex = 1;
            this.label_tickIndex_Title.Text = "Tick:";
            this.label_tickIndex_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_tps
            // 
            this.label_tps.AutoSize = true;
            this.label_tps.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label_tps.Location = new System.Drawing.Point(301, -1);
            this.label_tps.Name = "label_tps";
            this.label_tps.Size = new System.Drawing.Size(39, 13);
            this.label_tps.TabIndex = 0;
            this.label_tps.Text = "ticks/s";
            this.label_tps.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_botCount
            // 
            this.label_botCount.AutoSize = true;
            this.label_botCount.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label_botCount.Location = new System.Drawing.Point(74, 13);
            this.label_botCount.Name = "label_botCount";
            this.label_botCount.Size = new System.Drawing.Size(50, 13);
            this.label_botCount.TabIndex = 0;
            this.label_botCount.Text = "botCount";
            this.label_botCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_tickIndex
            // 
            this.label_tickIndex.AutoSize = true;
            this.label_tickIndex.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label_tickIndex.Location = new System.Drawing.Point(74, 0);
            this.label_tickIndex.Name = "label_tickIndex";
            this.label_tickIndex.Size = new System.Drawing.Size(50, 13);
            this.label_tickIndex.TabIndex = 0;
            this.label_tickIndex.Text = "tickIndex";
            this.label_tickIndex.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel_view
            // 
            this.panel_view.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_view.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_view.Location = new System.Drawing.Point(12, 12);
            this.panel_view.Name = "panel_view";
            this.panel_view.Size = new System.Drawing.Size(800, 935);
            this.panel_view.TabIndex = 3;
            // 
            // label_worldSize_title
            // 
            this.label_worldSize_title.AutoSize = true;
            this.label_worldSize_title.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label_worldSize_title.Location = new System.Drawing.Point(3, 26);
            this.label_worldSize_title.Name = "label_worldSize_title";
            this.label_worldSize_title.Size = new System.Drawing.Size(59, 13);
            this.label_worldSize_title.TabIndex = 2;
            this.label_worldSize_title.Text = "World size:";
            // 
            // label_worldSize
            // 
            this.label_worldSize.AutoSize = true;
            this.label_worldSize.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label_worldSize.Location = new System.Drawing.Point(74, 26);
            this.label_worldSize.Name = "label_worldSize";
            this.label_worldSize.Size = new System.Drawing.Size(52, 13);
            this.label_worldSize.TabIndex = 0;
            this.label_worldSize.Text = "worldSize";
            this.label_worldSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlText;
            this.ClientSize = new System.Drawing.Size(1184, 961);
            this.Controls.Add(this.panel_view);
            this.Controls.Add(this.panel_botData);
            this.Controls.Add(this.panel_controls);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "Anemun EvolutionSimulator Viewer";
            this.panel_controls.ResumeLayout(false);
            this.panel_controls.PerformLayout();
            this.panel_botData.ResumeLayout(false);
            this.panel_botData.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel_controls;
        private System.Windows.Forms.Panel panel_botData;
        private System.Windows.Forms.Panel panel_view;
        private System.Windows.Forms.Label label_tickIndex;
        private System.Windows.Forms.Label label_tickIndex_Title;
        private System.Windows.Forms.Label label_botCount_Title;
        private System.Windows.Forms.Label label_botCount;
        private System.Windows.Forms.Label label_tps_Title;
        private System.Windows.Forms.Label label_tps;
        private System.Windows.Forms.Button button_startSimulation;
        private System.Windows.Forms.Label label_loadData;
        private System.Windows.Forms.Button button_setPath;
        private System.Windows.Forms.FolderBrowserDialog dialog_loadData;
        private System.Windows.Forms.Label label_worldSize_title;
        private System.Windows.Forms.Label label_worldSize;
    }
}

