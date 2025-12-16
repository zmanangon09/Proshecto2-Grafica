using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace _3D_SHAPE_EXPLORER.Utils
{
    /// <summary>
    /// Clase que gestiona los tooltips y mensajes de ayuda para la interfaz de usuario.
    /// Proporciona información clara sobre la funcionalidad de cada control.
    /// </summary>
    public static class TooltipManager
    {
        /// <summary>
        /// Diccionario con las descripciones de cada control
        /// </summary>
        private static readonly Dictionary<string, string> Tooltips = new Dictionary<string, string>
        {
            // Selector de figuras
            { "gunacmbFigures", "?? SELECTOR DE FIGURAS\n\nSelecciona una figura 3D para agregarla a la escena.\n\nFiguras disponibles:\n• Cubo - Hexaedro regular de 6 caras\n• Cilindro - Superficie curva con dos bases circulares\n• Prisma Dodecagonal - Prisma de 12 lados\n• Octaedro - Poliedro de 8 caras triangulares\n• Pirámide - Tetraedro con base cuadrada" },
            
            // Selector de modo
            { "gunacmbMode", "??? MODO DE TRABAJO\n\n• Modo Objeto: Selecciona y transforma figuras completas\n• Modo Edición: Selecciona y modifica vértices, aristas o caras individuales" },
            
            // Radio buttons de edición
            { "gunarbtnVertexes", "?? SELECCIONAR VÉRTICES\n\nHaz clic en un vértice para seleccionarlo.\n\nUna vez seleccionado, puedes:\n• Trasladarlo con J/L (X), I/K (Y), U/O (Z)\n• Escalarlo con W/S\n\nEl vértice seleccionado se resalta en azul." },
            
            { "gunarbtnEdges", "?? SELECCIONAR ARISTAS\n\nHaz clic cerca de una arista para seleccionarla.\n\nUna vez seleccionada, puedes:\n• Trasladar ambos vértices con J/L (X), I/K (Y), U/O (Z)\n• Escalar con W/S\n\nLa arista seleccionada se resalta en rojo." },
            
            { "gunarbtnFaces", "?? SELECCIONAR CARAS\n\nHaz clic en una cara para seleccionarla.\n\nUna vez seleccionada, puedes:\n• Trasladar todos sus vértices con J/L (X), I/K (Y), U/O (Z)\n• Escalar con W/S\n\nLa cara seleccionada se resalta en púrpura." },
            
            { "gunarbtnPaintFigures", "?? PINTAR FIGURAS\n\nPermite cambiar el color de las figuras.\n\n1. Activa esta opción\n2. Selecciona un color con el botón de paleta\n3. Haz clic en cualquier figura para pintarla\n\nTodas las caras de la figura tomarán el color seleccionado." },
            
            // Botón de color
            { "gunbtnSelectColor", "?? SELECTOR DE COLOR\n\nAbre una paleta de colores predefinidos.\n\nSelecciona el color que deseas usar para pintar las figuras.\n\nEl botón mostrará el color actualmente seleccionado." },
            
            // Canvas
            { "picCanvas", "??? ÁREA DE VISUALIZACIÓN 3D\n\n• Clic izquierdo: Seleccionar figura/componente\n• El modo de edición determina qué se puede seleccionar\n\n?? CONTROLES DE CÁMARA:\n• Flechas ? ? ? ?: Rotar vista\n• + / -: Zoom\n• F1-F5: Vistas predefinidas\n• Home: Reiniciar cámara" }
        };

        /// <summary>
        /// Configura los tooltips para todos los controles del formulario
        /// </summary>
        public static void SetupTooltips(Form form, ToolTip toolTip)
        {
            toolTip.AutoPopDelay = 15000;  // 15 segundos
            toolTip.InitialDelay = 500;     // 0.5 segundos
            toolTip.ReshowDelay = 200;
            toolTip.ShowAlways = true;
            toolTip.IsBalloon = false;
            
            SetupControlTooltips(form.Controls, toolTip);
        }

        private static void SetupControlTooltips(Control.ControlCollection controls, ToolTip toolTip)
        {
            foreach (Control control in controls)
            {
                if (Tooltips.ContainsKey(control.Name))
                {
                    toolTip.SetToolTip(control, Tooltips[control.Name]);
                }
                
                // Recursivamente configurar tooltips para controles anidados
                if (control.HasChildren)
                {
                    SetupControlTooltips(control.Controls, toolTip);
                }
            }
        }

        /// <summary>
        /// Obtiene el tooltip para un control específico
        /// </summary>
        public static string GetTooltip(string controlName)
        {
            return Tooltips.ContainsKey(controlName) ? Tooltips[controlName] : "";
        }

        /// <summary>
        /// Obtiene todos los tooltips como un diccionario
        /// </summary>
        public static Dictionary<string, string> GetAllTooltips()
        {
            return new Dictionary<string, string>(Tooltips);
        }
    }
}
