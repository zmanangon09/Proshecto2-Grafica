using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace _3D_SHAPE_EXPLORER.Forms
{
    public partial class HelpForm : Form
    {
        private RichTextBox rtbHelp;
        private Guna2Button btnClose;
        private Panel containerPanel;
        private Timer fadeTimer;
        private bool isDarkMode;

        public HelpForm(bool isDarkMode)
        {
            this.isDarkMode = isDarkMode;
            this.Opacity = 0;

            InitializeComponent(); // Esto queda como lo genera Visual Studio

            ApplyThemeColors();    // Aplicamos colores dinámicos aquí
            ApplyCustomLogic();
            SetupFadeIn();
        }

        private void ApplyThemeColors()
        {
            Color bgColor = isDarkMode ? Color.FromArgb(30, 30, 30) : Color.FromArgb(240, 240, 245);
            Color rtbBg = isDarkMode ? Color.FromArgb(25, 25, 25) : Color.White;
            Color textC = isDarkMode ? Color.White : Color.FromArgb(30, 30, 30);
            Color btnBg = isDarkMode ? Color.FromArgb(60, 60, 60) : Color.FromArgb(200, 200, 205);

            this.BackColor = bgColor;
            this.rtbHelp.BackColor = rtbBg;
            this.rtbHelp.ForeColor = textC;
            this.btnClose.FillColor = btnBg;
            this.btnClose.ForeColor = textC;
        }

        private void ApplyCustomLogic()
        {
            this.Icon = SystemIcons.Information;
            rtbHelp.Text = GetHelpText();
            ColorizeHelp();

            btnClose.Click += (s, e) => this.Close();

            // Centrar botón
            int x = (this.ClientSize.Width / 2) - (btnClose.Width / 2);
            int y = this.ClientSize.Height - btnClose.Height - 15;
            btnClose.Location = new Point(x, y);
        }

        private void InitializeComponent()
        {
            this.rtbHelp = new System.Windows.Forms.RichTextBox();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.containerPanel = new System.Windows.Forms.Panel();
            this.containerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbHelp
            // 
            this.rtbHelp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbHelp.Font = new System.Drawing.Font("Consolas", 11F);
            this.rtbHelp.Location = new System.Drawing.Point(0, 0);
            this.rtbHelp.Name = "rtbHelp";
            this.rtbHelp.ReadOnly = true;
            this.rtbHelp.Size = new System.Drawing.Size(580, 600);
            this.rtbHelp.TabIndex = 0;
            this.rtbHelp.Text = "";
            // 
            // btnClose
            // 
            this.btnClose.BorderRadius = 8;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(0, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(107, 37);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "ENTENDIDO";
            // 
            // containerPanel
            // 
            this.containerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.containerPanel.Controls.Add(this.rtbHelp);
            this.containerPanel.Controls.Add(this.btnClose);
            this.containerPanel.Location = new System.Drawing.Point(12, 12);
            this.containerPanel.Name = "containerPanel";
            this.containerPanel.Size = new System.Drawing.Size(580, 600);
            this.containerPanel.TabIndex = 0;
            // 
            // HelpForm
            // 
            this.ClientSize = new System.Drawing.Size(598, 664);
            this.Controls.Add(this.containerPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "HelpForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ayuda y Controles";
            this.containerPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private string GetHelpText()
        {
            return @"
======================================================
      3D SHAPE EXPLORER - GUÍA DE USUARIO
======================================================

[ TECLAS DE CONTROL ]
------------------------------------------------------
• CÁMARA:   Flechas (Girar) / [+] [-] (Zoom)
• ESCALA:   [W] Aumentar / [S] Reducir
• MOVER:    [J][L] Eje X / [I][K] Eje Y / [U][O] Eje Z
• ROTAR:    [A][D] Girar sobre eje Z

[ MODOS DE EDICIÓN ]
------------------------------------------------------
• Vértices: Selección de puntos (AZUL)
• Aristas:  Selección de líneas (ROJO)
• Caras:    Selección de superficies (PÚRPURA)

[ ATAJOS ]
------------------------------------------------------
• F1-F4:    Cambiar vistas laterales
• F5:       Vista Isométrica
• CTRL+T:   Cambiar Modo Oscuro/Blanco

------------------------------------------------------";
        }

        private void ColorizeHelp()
        {
            Color sectionColor = isDarkMode ? Color.Cyan : Color.DodgerBlue;
            HighlightText("[ TECLAS DE CONTROL ]", sectionColor, true);
            HighlightText("[ MODOS DE EDICIÓN ]", sectionColor, true);
            HighlightText("[ ATAJOS ]", sectionColor, true);
        }

        private void HighlightText(string word, Color color, bool bold)
        {
            int start = 0;
            while ((start = rtbHelp.Find(word, start, RichTextBoxFinds.None)) != -1)
            {
                rtbHelp.Select(start, word.Length);
                rtbHelp.SelectionColor = color;
                if (bold) rtbHelp.SelectionFont = new Font(rtbHelp.Font, FontStyle.Bold);
                start += word.Length;
            }
        }

        private void SetupFadeIn()
        {
            fadeTimer = new Timer { Interval = 15 };
            fadeTimer.Tick += (s, e) => {
                if (this.Opacity < 1) this.Opacity += 0.08;
                else fadeTimer.Stop();
            };
            fadeTimer.Start();
        }
    }
}