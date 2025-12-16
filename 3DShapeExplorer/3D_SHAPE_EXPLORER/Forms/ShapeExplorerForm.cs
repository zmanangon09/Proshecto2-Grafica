using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _3D_SHAPE_EXPLORER.Forms;
using _3D_SHAPE_EXPLORER.Models;
using _3D_SHAPE_EXPLORER.Services;
using _3D_SHAPE_EXPLORER.Utils;
using Guna.UI2.WinForms;

namespace _3D_SHAPE_EXPLORER
{
    public partial class ShapeExplorerForm : Form
    {
        private SceneManager sceneManager = new SceneManager();
        private ShapeRenderer renderer = new ShapeRenderer();
        private KeyboardController inputController;
        private string lastSelected = "";
        private Color currentPaintColor = Color.Yellow;
        private ContextMenuStrip colorMenu;
        private MouseClickHandler mouseClickHandler;
        private ToolTip mainToolTip;
        private Label lblStatus;
        private Guna2Button gunbtnHelp;
        private Guna2Button gunbtnResetCamera;

        public ShapeExplorerForm()
        {
            InitializeComponent();
            gunacmbFigures.SelectedIndexChanged += GunacmbFigures_SelectedIndexChanged;
            gunacmbMode.SelectedIndexChanged += GunacmbMode_SelectedIndexChanged;

            SetupComboBoxFigures();
            SetupStatusBar();

            SetupComboBoxMode();
            SetupTooltips();
            SetupExtraButtons();

            sceneManager.Initialize();
            inputController = new KeyboardController(this, picCanvas, sceneManager);
            inputController.OnStatusChanged += OnStatusChanged;

            picCanvas.Paint += PanelCanvas_Paint;
            mouseClickHandler = new MouseClickHandler(sceneManager, picCanvas,
                            gunarbtnVertexes, gunarbtnEdges, gunarbtnFaces, gunarbtnPaintFigures, currentPaintColor);
            picCanvas.MouseClick += picCanvas_MouseClick;
            this.Load += ShapeExplorerForm_Load;
        }

        private void SetupExtraButtons()
        {
            // Botón de Reset Cámara
            gunbtnResetCamera = new Guna2Button
            {
                Text = "📷 Reset",
                Size = new Size(85, 32),
                Location = new Point(1300, 9),
                FillColor = Color.FromArgb(50, 50, 55),
                ForeColor = Color.White,
                Font = new Font("Segoe UI Emoji", 9, FontStyle.Bold),
                BorderRadius = 5
            };
            gunbtnResetCamera.Click += gunbtnResetCamera_Click;
            this.Controls.Add(gunbtnResetCamera);

            // Botón de Ayuda
            gunbtnHelp = new Guna2Button
            {
                Text = "❓ F6",
                Size = new Size(70, 32),
                Location = new Point(1395, 9),
                FillColor = Color.FromArgb(30, 60, 100),
                ForeColor = Color.White,
                Font = new Font("Segoe UI Emoji", 9, FontStyle.Bold),
                BorderRadius = 5
            };
            gunbtnHelp.Click += gunbtnHelp_Click;
            this.Controls.Add(gunbtnHelp);

            mainToolTip.SetToolTip(gunbtnResetCamera, "📷 REINICIAR CÁMARA\n\nRestablece la cámara a la posición inicial.\nAtajo: Home");
            mainToolTip.SetToolTip(gunbtnHelp, "❓ AYUDA\n\nMuestra el manual completo de uso.\nAtajo: F6");
        }

        private void gunbtnResetCamera_Click(object sender, EventArgs e)
        {
            sceneManager.Camera.Reset();
            sceneManager.Camera.SetIsometricView();
            lblStatus.Text = "📷 Cámara reiniciada a vista isométrica";
            picCanvas.Invalidate();
        }

        private void gunbtnHelp_Click(object sender, EventArgs e)
        {
            ShowHelp();
        }

        private void SetupTooltips()
        {
            mainToolTip = new ToolTip();
            mainToolTip.AutoPopDelay = 15000;
            mainToolTip.InitialDelay = 400;
            mainToolTip.ReshowDelay = 200;
            mainToolTip.ShowAlways = true;

            // Tooltips informativos para cada control
            mainToolTip.SetToolTip(gunacmbFigures, 
                "🧩 AGREGAR FIGURA\n\nSelecciona una figura 3D para agregarla a la escena.\nFiguras: Cubo, Cilindro, Cono, Esfera, Pirámide, etc.");
            
            mainToolTip.SetToolTip(gunacmbMode, 
                "🎛️ MODO DE TRABAJO\n\n• Objeto: Transforma figuras completas\n• Edición: Modifica vértices, aristas o caras");
            
            mainToolTip.SetToolTip(gunarbtnVertexes, 
                "🟢 VÉRTICES\n\nSelecciona puntos individuales.\nUsa J/L, I/K, U/O para trasladar.\nUsa W/S para escalar.");
            
            mainToolTip.SetToolTip(gunarbtnEdges, 
                "📏 ARISTAS\n\nSelecciona líneas entre vértices.\nTransforma ambos extremos de la arista.");
            
            mainToolTip.SetToolTip(gunarbtnFaces, 
                "🧊 CARAS\n\nSelecciona caras completas.\nTransforma todos los vértices de la cara.");
            
            mainToolTip.SetToolTip(gunarbtnPaintFigures, 
                "🎨 PINTAR\n\nPinta todas las caras de una figura.\n1. Selecciona un color\n2. Haz clic en la figura");
            
            mainToolTip.SetToolTip(gunbtnSelectColor, 
                "🌈 PALETA DE COLORES\n\nAbre el selector de colores para pintar figuras.");
            
            mainToolTip.SetToolTip(picCanvas, 
                "Área 3D - Clic para seleccionar | Flechas: Rotar cámara | +/-: Zoom | F1-F5: Vistas | F6: Ayuda");
        }

     

        private void OnStatusChanged(string message)
        {
            if (lblStatus != null)
            {
                lblStatus.Text = $"✓ {message}";
                
                // Restaurar mensaje por defecto después de 3 segundos
                var timer = new Timer { Interval = 3000 };
                timer.Tick += (s, e) =>
                {
                    lblStatus.Text = "💡 Presiona F6 para ver la ayuda completa | Flechas: Mover cámara | +/-: Zoom | F1-F5: Vistas predefinidas";
                    timer.Stop();
                    timer.Dispose();
                };
                timer.Start();
            }
        }

        private void PanelCanvas_Paint(object sender, PaintEventArgs e)
        {
            bool isInEditMode = gunarbtnVertexes.Checked || gunarbtnEdges.Checked || gunarbtnFaces.Checked;
            renderer.Draw(e.Graphics, sceneManager.Shapes, picCanvas.Size, sceneManager, isInEditMode, currentPaintColor);
        }

        private void ShapeExplorerForm_Load(object sender, EventArgs e)
        {
            gunarbtnVertexes.Visible = false;
            gunarbtnEdges.Visible = false;
            gunarbtnFaces.Visible = false;
            gunarbtnPaintFigures.Visible = false;
 
            gunacmbMode.SelectedIndex = 0;
            gunbtnSelectColor.Visible = false;

            string imagePath = Path.Combine(Application.StartupPath, "Resources", "paint_bucket.png");
            if (File.Exists(imagePath))
            {
                gunbtnSelectColor.Image = Image.FromFile(imagePath);
                gunbtnSelectColor.ImageSize = new Size(24, 24);
                gunbtnSelectColor.Text = "";
                gunbtnSelectColor.TextAlign = HorizontalAlignment.Center;
                gunbtnSelectColor.ImageAlign = HorizontalAlignment.Center;
                gunbtnSelectColor.FillColor = Color.Transparent;
                gunbtnSelectColor.BorderThickness = 0;
            }

            // Manejar tecla F6 para ayuda
            this.KeyPreview = true;
            this.KeyDown += ShapeExplorerForm_KeyDown;
            
            // Título más descriptivo
            this.Text = "3D Shape Explorer - Herramienta de Computación Gráfica 3D";
        }

        private void ShapeExplorerForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                ShowHelp();
            }
        }

        private void ShowHelp()
        {
            var helpForm = new HelpForm();
            helpForm.ShowDialog(this);
        }

        private void picCanvas_Click(object sender, EventArgs e)
        {

        }

        private void picCanvas_MouseClick(object sender, MouseEventArgs e)
        {
            mouseClickHandler.HandleMouseClick(e.Location);
            
            // Actualizar estado según lo seleccionado
            var selected = sceneManager.Shapes.FirstOrDefault(s => s.IsSelected);
            if (selected != null)
            {
                int idx = sceneManager.Shapes.IndexOf(selected) + 1;
                string mode = "";
                if (sceneManager.SelectedVertexIndex.HasValue)
                    mode = $" - Vértice #{sceneManager.SelectedVertexIndex.Value}";
                else if (sceneManager.SelectedEdge != null)
                    mode = $" - Arista";
                else if (sceneManager.SelectedFace != null)
                    mode = " - Cara";
                
                lblStatus.Text = $"✓ Seleccionado: {selected.GetType().Name} #{idx}{mode}";
            }
        }

        private void chkFaces_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnSelectColor_Click(object sender, EventArgs e)
        {
            
        }
        private void SetupStatusBar()
        {
            lblStatus = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Bottom,
                Height = 28,
                BackColor = Color.FromArgb(35, 35, 40),
                ForeColor = Color.FromArgb(200, 200, 200),
                Font = new Font("Segoe UI", 9),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0),
                Text = "💡 Presiona F6 para ver la ayuda completa | Flechas: Mover cámara | +/-: Zoom | F1-F5: Vistas predefinidas"
            };
            this.Controls.Add(lblStatus);
        }
        private void SetupComboBoxMode()
        {
            gunacmbMode.Items.Clear();
            gunacmbMode.Items.Add(new ComboBoxItem("📦 Object Mode", "object"));
            gunacmbMode.Items.Add(new ComboBoxItem("✏️ Edition Mode", "edit"));
            gunacmbMode.SelectedIndex = 0;
            gunacmbMode.Font = new Font("Segoe UI Emoji", 11);
        }

        private void SetupComboBoxFigures()
        {
            gunacmbFigures.Items.Clear();
            gunacmbFigures.Items.Add(new ComboBoxItem("🧩 Select a figure...", ""));
            gunacmbFigures.Items.Add(new ComboBoxItem("🧊 Cube", "Cube"));
            gunacmbFigures.Items.Add(new ComboBoxItem("🧱 Cylinder", "Cylinder"));
            gunacmbFigures.Items.Add(new ComboBoxItem("🔺 Cone", "Cone"));
            gunacmbFigures.Items.Add(new ComboBoxItem("⚪ Sphere", "Sphere"));
            gunacmbFigures.Items.Add(new ComboBoxItem("🔷 Pyramid", "Pyramid"));
            gunacmbFigures.Items.Add(new ComboBoxItem("🔶 Octahedron", "Octahedron"));
            gunacmbFigures.Items.Add(new ComboBoxItem("🧿 Dodecagonal", "DodecagonalPrism"));
            gunacmbFigures.SelectedIndex = 0;
            gunacmbFigures.Font = new Font("Segoe UI Emoji", 11);
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void GunacmbFigures_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gunacmbFigures.SelectedItem is ComboBoxItem selectedItem)
            {
                string value = selectedItem.Value;

                if (string.IsNullOrEmpty(value))
                    return;

                lastSelected = value;
                var shape = ShapeFactory.Create(value);
                if (shape != null)
                {
                    sceneManager.AddShape(shape);
                    lblStatus.Text = $"✓ Figura agregada: {value} (Total: {sceneManager.Shapes.Count})";
                    picCanvas.Invalidate();
                }
            }
        }

        private void GunacmbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gunacmbMode.SelectedItem is ComboBoxItem selectedItem)
            {
                string modeValue = selectedItem.Value;

                if (modeValue == "edit")
                {
                    gunarbtnVertexes.Visible = true;
                    gunarbtnEdges.Visible = true;
                    gunarbtnFaces.Visible = true;
                    gunarbtnPaintFigures.Visible = true;

                    gunbtnSelectColor.Visible = gunarbtnPaintFigures.Checked;
                    lblStatus.Text = "💡 Modo Edición: Selecciona qué componente quieres editar (vértices, aristas o caras)";
                }
                else
                {
                    gunarbtnVertexes.Visible = false;
                    gunarbtnEdges.Visible = false;
                    gunarbtnPaintFigures.Visible = false;
                    gunarbtnFaces.Visible = false;

                    gunarbtnVertexes.Checked = false;
                    gunarbtnEdges.Checked = false;
                    gunarbtnFaces.Checked = false;
                    gunarbtnPaintFigures.Checked = false;

                    sceneManager.SelectedVertexIndex = null;
                    sceneManager.SelectedEdge = null;
                    sceneManager.SelectedFace = null;
                    gunbtnSelectColor.Visible = false;
                    
                    lblStatus.Text = "💡 Modo Objeto: Haz clic en una figura para seleccionarla y transformarla";
                }
                
                picCanvas.Invalidate();
            }
        }

        private void gunarbtnPaintFigures_CheckedChanged(object sender, EventArgs e)
        {
            gunbtnSelectColor.Visible = gunarbtnPaintFigures.Checked;
            if (gunarbtnPaintFigures.Checked)
            {
                lblStatus.Text = "🎨 Modo Pintura: Selecciona un color y haz clic en una figura para pintarla";
            }
        }

        private void gunbtnSelectColor_Click(object sender, EventArgs e)
        {
            var colores = new List<Color> { Color.Black, Color.White, Color.Orange, Color.Gold, Color.Green, Color.Teal,
                                     Color.MidnightBlue, Color.DarkGray, Color.HotPink, Color.MediumPurple,
                                     Color.Red, Color.Blue, Color.Yellow, Color.Cyan, Color.Magenta, Color.Lime };

            var colorForm = new ColorPickerForm(colores);
            var buttonLocation = gunbtnSelectColor.PointToScreen(Point.Empty);
            colorForm.Location = new Point(buttonLocation.X, buttonLocation.Y + gunbtnSelectColor.Height);

            if (colorForm.ShowDialog() == DialogResult.OK)
            {
                currentPaintColor = colorForm.SelectedColor;
                gunbtnSelectColor.BackColor = currentPaintColor;
                mouseClickHandler.UpdatePaintColor(currentPaintColor);
                lblStatus.Text = $"🎨 Color seleccionado: {currentPaintColor.Name}";
            }
        }
    }
}
