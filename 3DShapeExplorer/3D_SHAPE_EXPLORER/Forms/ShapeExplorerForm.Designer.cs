namespace _3D_SHAPE_EXPLORER
{
    partial class ShapeExplorerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.imgLogo = new System.Windows.Forms.PictureBox();
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.btnHelp = new Guna.UI2.WinForms.Guna2Button();
            this.btnReset = new Guna.UI2.WinForms.Guna2Button();
            this.paneSeparator2 = new System.Windows.Forms.Panel();
            this.pnlEditControls = new System.Windows.Forms.Panel();
            this.rbtnVertex = new Guna.UI2.WinForms.Guna2RadioButton();
            this.rbtnEdge = new Guna.UI2.WinForms.Guna2RadioButton();
            this.rbtnFace = new Guna.UI2.WinForms.Guna2RadioButton();
            this.rbtnPaint = new Guna.UI2.WinForms.Guna2RadioButton();
            this.btnColor = new Guna.UI2.WinForms.Guna2Button();
            this.cmbMode = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblMode = new System.Windows.Forms.Label();
            this.paneSeparator1 = new System.Windows.Forms.Panel();
            this.cmbFigures = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblFigures = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.picCanvas = new System.Windows.Forms.PictureBox();
            this.lblOverlay = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).BeginInit();
            this.panelSidebar.SuspendLayout();
            this.pnlEditControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // imgLogo
            // 
            this.imgLogo.Location = new System.Drawing.Point(0, 0);
            this.imgLogo.Name = "imgLogo";
            this.imgLogo.Size = new System.Drawing.Size(100, 50);
            this.imgLogo.TabIndex = 0;
            this.imgLogo.TabStop = false;
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.panelSidebar.Controls.Add(this.btnHelp);
            this.panelSidebar.Controls.Add(this.btnReset);
            this.panelSidebar.Controls.Add(this.paneSeparator2);
            this.panelSidebar.Controls.Add(this.pnlEditControls);
            this.panelSidebar.Controls.Add(this.cmbMode);
            this.panelSidebar.Controls.Add(this.lblMode);
            this.panelSidebar.Controls.Add(this.paneSeparator1);
            this.panelSidebar.Controls.Add(this.cmbFigures);
            this.panelSidebar.Controls.Add(this.lblFigures);
            this.panelSidebar.Controls.Add(this.lblTitle);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelSidebar.Location = new System.Drawing.Point(1220, 0);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(280, 850);
            this.panelSidebar.TabIndex = 1;
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.BorderRadius = 4;
            this.btnHelp.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnHelp.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnHelp.ForeColor = System.Drawing.Color.White;
            this.btnHelp.Location = new System.Drawing.Point(20, 1550);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(240, 40);
            this.btnHelp.TabIndex = 0;
            this.btnHelp.Text = "Help (F6)";
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.BorderRadius = 4;
            this.btnReset.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(65)))));
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.Location = new System.Drawing.Point(20, 1500);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(240, 40);
            this.btnReset.TabIndex = 1;
            this.btnReset.Text = "Reset Camera";
            // 
            // paneSeparator2
            // 
            this.paneSeparator2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.paneSeparator2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.paneSeparator2.Location = new System.Drawing.Point(0, 849);
            this.paneSeparator2.Name = "paneSeparator2";
            this.paneSeparator2.Size = new System.Drawing.Size(280, 1);
            this.paneSeparator2.TabIndex = 2;
            // 
            // pnlEditControls
            // 
            this.pnlEditControls.Controls.Add(this.rbtnVertex);
            this.pnlEditControls.Controls.Add(this.rbtnEdge);
            this.pnlEditControls.Controls.Add(this.rbtnFace);
            this.pnlEditControls.Controls.Add(this.rbtnPaint);
            this.pnlEditControls.Controls.Add(this.btnColor);
            this.pnlEditControls.Location = new System.Drawing.Point(20, 250);
            this.pnlEditControls.Name = "pnlEditControls";
            this.pnlEditControls.Size = new System.Drawing.Size(240, 240);
            this.pnlEditControls.TabIndex = 3;
            // 
            // rbtnVertex
            // 
            this.rbtnVertex.AutoSize = true;
            this.rbtnVertex.CheckedState.BorderColor = System.Drawing.Color.DodgerBlue;
            this.rbtnVertex.CheckedState.BorderThickness = 0;
            this.rbtnVertex.CheckedState.FillColor = System.Drawing.Color.DodgerBlue;
            this.rbtnVertex.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rbtnVertex.ForeColor = System.Drawing.Color.White;
            this.rbtnVertex.Location = new System.Drawing.Point(10, 10);
            this.rbtnVertex.Name = "rbtnVertex";
            this.rbtnVertex.Size = new System.Drawing.Size(95, 27);
            this.rbtnVertex.TabIndex = 0;
            this.rbtnVertex.Text = "Vertexes";
            this.rbtnVertex.UncheckedState.BorderThickness = 0;
            // 
            // rbtnEdge
            // 
            this.rbtnEdge.AutoSize = true;
            this.rbtnEdge.CheckedState.BorderColor = System.Drawing.Color.DodgerBlue;
            this.rbtnEdge.CheckedState.BorderThickness = 0;
            this.rbtnEdge.CheckedState.FillColor = System.Drawing.Color.DodgerBlue;
            this.rbtnEdge.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rbtnEdge.ForeColor = System.Drawing.Color.White;
            this.rbtnEdge.Location = new System.Drawing.Point(10, 45);
            this.rbtnEdge.Name = "rbtnEdge";
            this.rbtnEdge.Size = new System.Drawing.Size(76, 27);
            this.rbtnEdge.TabIndex = 1;
            this.rbtnEdge.Text = "Edges";
            this.rbtnEdge.UncheckedState.BorderThickness = 0;
            // 
            // rbtnFace
            // 
            this.rbtnFace.AutoSize = true;
            this.rbtnFace.CheckedState.BorderColor = System.Drawing.Color.DodgerBlue;
            this.rbtnFace.CheckedState.BorderThickness = 0;
            this.rbtnFace.CheckedState.FillColor = System.Drawing.Color.DodgerBlue;
            this.rbtnFace.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rbtnFace.ForeColor = System.Drawing.Color.White;
            this.rbtnFace.Location = new System.Drawing.Point(10, 80);
            this.rbtnFace.Name = "rbtnFace";
            this.rbtnFace.Size = new System.Drawing.Size(71, 27);
            this.rbtnFace.TabIndex = 2;
            this.rbtnFace.Text = "Faces";
            this.rbtnFace.UncheckedState.BorderThickness = 0;
            // 
            // rbtnPaint
            // 
            this.rbtnPaint.AutoSize = true;
            this.rbtnPaint.CheckedState.BorderColor = System.Drawing.Color.DodgerBlue;
            this.rbtnPaint.CheckedState.BorderThickness = 0;
            this.rbtnPaint.CheckedState.FillColor = System.Drawing.Color.DodgerBlue;
            this.rbtnPaint.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rbtnPaint.ForeColor = System.Drawing.Color.White;
            this.rbtnPaint.Location = new System.Drawing.Point(10, 115);
            this.rbtnPaint.Name = "rbtnPaint";
            this.rbtnPaint.Size = new System.Drawing.Size(128, 27);
            this.rbtnPaint.TabIndex = 3;
            this.rbtnPaint.Text = "Paint Figures";
            this.rbtnPaint.UncheckedState.BorderThickness = 0;
            // 
            // btnColor
            // 
            this.btnColor.BorderRadius = 4;
            this.btnColor.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(55)))));
            this.btnColor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnColor.ForeColor = System.Drawing.Color.White;
            this.btnColor.Location = new System.Drawing.Point(10, 150);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(200, 35);
            this.btnColor.TabIndex = 4;
            this.btnColor.Text = "Select Color";
            // 
            // cmbMode
            // 
            this.cmbMode.BackColor = System.Drawing.Color.Transparent;
            this.cmbMode.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.cmbMode.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMode.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.cmbMode.FocusedColor = System.Drawing.Color.Empty;
            this.cmbMode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbMode.ForeColor = System.Drawing.Color.White;
            this.cmbMode.ItemHeight = 30;
            this.cmbMode.Location = new System.Drawing.Point(20, 195);
            this.cmbMode.Name = "cmbMode";
            this.cmbMode.Size = new System.Drawing.Size(240, 36);
            this.cmbMode.TabIndex = 4;
            // 
            // lblMode
            // 
            this.lblMode.AutoSize = true;
            this.lblMode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMode.ForeColor = System.Drawing.Color.Gray;
            this.lblMode.Location = new System.Drawing.Point(20, 170);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(53, 20);
            this.lblMode.TabIndex = 5;
            this.lblMode.Text = "MODE";
            // 
            // paneSeparator1
            // 
            this.paneSeparator1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.paneSeparator1.Location = new System.Drawing.Point(20, 150);
            this.paneSeparator1.Name = "paneSeparator1";
            this.paneSeparator1.Size = new System.Drawing.Size(240, 1);
            this.paneSeparator1.TabIndex = 6;
            // 
            // cmbFigures
            // 
            this.cmbFigures.BackColor = System.Drawing.Color.Transparent;
            this.cmbFigures.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.cmbFigures.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbFigures.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFigures.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.cmbFigures.FocusedColor = System.Drawing.Color.Empty;
            this.cmbFigures.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbFigures.ForeColor = System.Drawing.Color.White;
            this.cmbFigures.ItemHeight = 30;
            this.cmbFigures.Location = new System.Drawing.Point(20, 95);
            this.cmbFigures.Name = "cmbFigures";
            this.cmbFigures.Size = new System.Drawing.Size(240, 36);
            this.cmbFigures.TabIndex = 7;
            // 
            // lblFigures
            // 
            this.lblFigures.AutoSize = true;
            this.lblFigures.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFigures.ForeColor = System.Drawing.Color.Gray;
            this.lblFigures.Location = new System.Drawing.Point(20, 70);
            this.lblFigures.Name = "lblFigures";
            this.lblFigures.Size = new System.Drawing.Size(70, 20);
            this.lblFigures.TabIndex = 8;
            this.lblFigures.Text = "FIGURES";
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(280, 60);
            this.lblTitle.TabIndex = 9;
            this.lblTitle.Text = "SHAPE EXPLORER";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picCanvas
            // 
            this.picCanvas.BackColor = System.Drawing.Color.LavenderBlush;
            this.picCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picCanvas.Location = new System.Drawing.Point(0, 0);
            this.picCanvas.Name = "picCanvas";
            this.picCanvas.Size = new System.Drawing.Size(1220, 850);
            this.picCanvas.TabIndex = 0;
            this.picCanvas.TabStop = false;
            // 
            // lblOverlay
            // 
            this.lblOverlay.AutoSize = true;
            this.lblOverlay.BackColor = System.Drawing.Color.Transparent;
            this.lblOverlay.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblOverlay.ForeColor = System.Drawing.Color.Black;
            this.lblOverlay.Location = new System.Drawing.Point(44, 22);
            this.lblOverlay.Name = "lblOverlay";
            this.lblOverlay.Size = new System.Drawing.Size(437, 23);
            this.lblOverlay.TabIndex = 0;
            this.lblOverlay.Text = "ARROWS: Rotate | +/-: Zoom | DEL: Delete | F6: Controls";
            // 
            // ShapeExplorerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(1500, 850);
            this.Controls.Add(this.lblOverlay);
            this.Controls.Add(this.picCanvas);
            this.Controls.Add(this.panelSidebar);
            this.Name = "ShapeExplorerForm";
            this.Text = "3D Shape Explorer";
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).EndInit();
            this.panelSidebar.ResumeLayout(false);
            this.panelSidebar.PerformLayout();
            this.pnlEditControls.ResumeLayout(false);
            this.pnlEditControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        // Existing and new controls
        private System.Windows.Forms.PictureBox imgLogo;
        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Label lblTitle;
        
        private System.Windows.Forms.Label lblFigures;
        private Guna.UI2.WinForms.Guna2ComboBox cmbFigures;
        
        private System.Windows.Forms.Panel paneSeparator1;
     
        private System.Windows.Forms.Label lblMode;
        private Guna.UI2.WinForms.Guna2ComboBox cmbMode;
        
        private System.Windows.Forms.Panel pnlEditControls;
        private Guna.UI2.WinForms.Guna2RadioButton rbtnVertex;
        private Guna.UI2.WinForms.Guna2RadioButton rbtnEdge;
        private Guna.UI2.WinForms.Guna2RadioButton rbtnFace;
        private Guna.UI2.WinForms.Guna2RadioButton rbtnPaint;
        private Guna.UI2.WinForms.Guna2Button btnColor;

        private System.Windows.Forms.Panel paneSeparator2;
        private Guna.UI2.WinForms.Guna2Button btnReset;
        private Guna.UI2.WinForms.Guna2Button btnHelp;

        private System.Windows.Forms.PictureBox picCanvas;
        private System.Windows.Forms.Label lblOverlay;
    }
}
