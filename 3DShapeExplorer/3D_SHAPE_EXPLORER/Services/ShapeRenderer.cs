using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using _3D_SHAPE_EXPLORER.Models;
using _3D_SHAPE_EXPLORER.Utils;

namespace _3D_SHAPE_EXPLORER.Services
{
    public class ShapeRenderer
    {
        // Se añade el parámetro LightSource light
        public void Draw(Graphics g, List<Shape3D> shapes, Size panelSize, SceneManager sceneManager, bool isInEditMode, Color paintColor, LightSource light)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Dibujar ejes de referencia
            DrawAxes(g, panelSize, sceneManager);

            // Dibujar rejilla de referencia
            DrawGrid(g, panelSize, sceneManager);

            foreach (var shape in shapes)
            {
                shape.ApplyTransformations();
                var projectedPoints = shape.Points
                    .Select(p => Projection3D.ProjectWithCamera(p, panelSize, sceneManager.Camera))
                    .ToList();

                // Pasamos la luz al método de caras
                DrawFaces(g, shape, projectedPoints, sceneManager, light);
                DrawEdges(g, shape, projectedPoints);
                DrawCenter(g, projectedPoints);
                DrawSelection(g, shape, sceneManager, projectedPoints);
            }

            DrawLightSource(g, panelSize, sceneManager, light);
            // Dibujar información de la cámara y la escena
            DrawHUD(g, panelSize, sceneManager);
        }

        private void DrawAxes(Graphics g, Size panelSize, SceneManager sceneManager)
        {
            var camera = sceneManager.Camera;
            float axisLength = 50;

            Point3D origin = new Point3D(-panelSize.Width / 3, -panelSize.Height / 3, 0);
            Point3D xAxis = new Point3D(origin.X + axisLength, origin.Y, origin.Z);
            Point3D yAxis = new Point3D(origin.X, origin.Y + axisLength, origin.Z);
            Point3D zAxis = new Point3D(origin.X, origin.Y, origin.Z + axisLength);

            var pOrigin = Projection3D.ProjectWithCamera(origin, panelSize, camera);
            var pX = Projection3D.ProjectWithCamera(xAxis, panelSize, camera);
            var pY = Projection3D.ProjectWithCamera(yAxis, panelSize, camera);
            var pZ = Projection3D.ProjectWithCamera(zAxis, panelSize, camera);

            using (var penX = new Pen(Color.Red, 2))
            using (var penY = new Pen(Color.LimeGreen, 2))
            using (var penZ = new Pen(Color.DodgerBlue, 2))
            using (var font = new Font("Consolas", 10, FontStyle.Bold))
            {
                g.DrawLine(penX, pOrigin, pX);
                g.DrawLine(penY, pOrigin, pY);
                g.DrawLine(penZ, pOrigin, pZ);

                g.DrawString("X", font, Brushes.Red, pX.X + 5, pX.Y);
                g.DrawString("Y", font, Brushes.LimeGreen, pY.X + 5, pY.Y);
                g.DrawString("Z", font, Brushes.DodgerBlue, pZ.X + 5, pZ.Y);
            }
        }

        private void DrawGrid(Graphics g, Size panelSize, SceneManager sceneManager)
        {
            var camera = sceneManager.Camera;
            int gridSize = 5;
            float spacing = 50;

            using (var gridPen = new Pen(Color.FromArgb(40, 100, 100, 100), 1))
            {
                for (int i = -gridSize; i <= gridSize; i++)
                {
                    var start = new Point3D(-gridSize * spacing, 0, i * spacing);
                    var end = new Point3D(gridSize * spacing, 0, i * spacing);
                    var pStart = Projection3D.ProjectWithCamera(start, panelSize, camera);
                    var pEnd = Projection3D.ProjectWithCamera(end, panelSize, camera);
                    g.DrawLine(gridPen, pStart, pEnd);

                    start = new Point3D(i * spacing, 0, -gridSize * spacing);
                    end = new Point3D(i * spacing, 0, gridSize * spacing);
                    pStart = Projection3D.ProjectWithCamera(start, panelSize, camera);
                    pEnd = Projection3D.ProjectWithCamera(end, panelSize, camera);
                    g.DrawLine(gridPen, pStart, pEnd);
                }
            }
        }

        private void DrawHUD(Graphics g, Size panelSize, SceneManager sceneManager)
        {
            using (var font = new Font("Consolas", 9, FontStyle.Regular))
            using (var bgBrush = new SolidBrush(Color.FromArgb(180, 0, 0, 0)))
            {
                string cameraInfo = sceneManager.Camera.GetCameraInfo();
                string sceneInfo = sceneManager.GetSceneInfo();

                var cameraSize = g.MeasureString(cameraInfo, font);
                var sceneSize = g.MeasureString(sceneInfo, font);

                g.FillRectangle(bgBrush, 5, panelSize.Height - 55, Math.Max(cameraSize.Width, sceneSize.Width) + 10, 50);

                g.DrawString(cameraInfo, font, Brushes.Cyan, 10, panelSize.Height - 50);
                g.DrawString(sceneInfo, font, Brushes.Yellow, 10, panelSize.Height - 30);
            }
        }

        private void DrawCenter(Graphics g, List<PointF> points)
        {
            if (points.Count == 0) return;
            float avgX = points.Average(p => p.X);
            float avgY = points.Average(p => p.Y);
            g.FillEllipse(Brushes.Red, avgX - 3, avgY - 3, 6, 6);
        }

        private void DrawFaces(Graphics g, Shape3D shape, List<PointF> points, SceneManager sceneManager, LightSource light)
        {
            var facesWithDepth = new List<(List<int> face, float depth, int index)>();

            for (int i = 0; i < shape.Faces.Count; i++)
            {
                var face = shape.Faces[i];
                float avgZ = face.Average(idx =>
                {
                    var transformed = Projection3D.TransformWithCamera(shape.Points[idx], sceneManager.Camera);
                    return transformed.Z;
                });
                facesWithDepth.Add((face, avgZ, i));
            }

            foreach (var (face, depth, index) in facesWithDepth.OrderByDescending(f => f.depth))
            {
                var polygon = face.Select(idx => points[idx]).ToArray();

                Color faceColor = shape.IsPainted ? shape.PaintColor : Color.Gray;

                // Cálculo de iluminación usando la fuente de luz dinámica
                float lightFactor = CalculateLightFactor(shape, face, light);

                Color shadedColor = Color.FromArgb(
                    faceColor.A,
                    Math.Min(255, (int)(faceColor.R * lightFactor + light.AmbientColor.R)),
                    Math.Min(255, (int)(faceColor.G * lightFactor + light.AmbientColor.G)),
                    Math.Min(255, (int)(faceColor.B * lightFactor + light.AmbientColor.B))
                );

                using (var fill = new SolidBrush(shadedColor))
                {
                    g.FillPolygon(fill, polygon);
                }
            }
        }

        private float CalculateLightFactor(Shape3D shape, List<int> face, LightSource light)
        {
            if (face.Count < 3) return 0.7f;

            // 1. Calcular Normal de la cara
            var p0 = shape.Points[face[0]];
            var p1 = shape.Points[face[1]];
            var p2 = shape.Points[face[2]];

            float v1x = p1.X - p0.X, v1y = p1.Y - p0.Y, v1z = p1.Z - p0.Z;
            float v2x = p2.X - p0.X, v2y = p2.Y - p0.Y, v2z = p2.Z - p0.Z;

            float nx = v1y * v2z - v1z * v2y;
            float ny = v1z * v2x - v1x * v2z;
            float nz = v1x * v2y - v1y * v2x;

            float length = (float)Math.Sqrt(nx * nx + ny * ny + nz * nz);
            if (length < 0.001f) return 0.4f;
            nx /= length; ny /= length; nz /= length;

            // 2. Calcular vector hacia la fuente de luz (L = LightPos - FaceCenter)
            float centerX = (p0.X + p1.X + p2.X) / 3f;
            float centerY = (p0.Y + p1.Y + p2.Y) / 3f;
            float centerZ = (p0.Z + p1.Z + p2.Z) / 3f;

            float lx = light.Position.X - centerX;
            float ly = light.Position.Y - centerY;
            float lz = light.Position.Z - centerZ;

            float lLen = (float)Math.Sqrt(lx * lx + ly * ly + lz * lz);
            if (lLen > 0) { lx /= lLen; ly /= lLen; lz /= lLen; }

            // 3. Producto punto para intensidad
            float dot = Math.Max(0, nx * lx + ny * ly + nz * lz);

            return (dot * light.Intensity);
        }

        private void DrawEdges(Graphics g, Shape3D shape, List<PointF> points)
        {
            // Usamos shape.EdgeColor para que cambie según el tema actual
            using (Pen edgePen = shape.IsSelected
                ? new Pen(Color.Orange, 2)
                : new Pen(shape.EdgeColor, 1))
            {
                foreach (var face in shape.Faces)
                {
                    for (int i = 0; i < face.Count; i++)
                    {
                        int idx1 = face[i];
                        int idx2 = face[(i + 1) % face.Count];
                        if (idx1 < points.Count && idx2 < points.Count)
                        {
                            g.DrawLine(edgePen, points[idx1], points[idx2]);
                        }
                    }
                }
            }
        }

        private void DrawSelection(Graphics g, Shape3D shape, SceneManager sceneManager, List<PointF> points)
        {
            if (sceneManager.SelectedShapeIndex.HasValue &&
                sceneManager.Shapes.IndexOf(shape) == sceneManager.SelectedShapeIndex.Value)
            {
                // Vértice
                if (sceneManager.SelectedVertexIndex.HasValue)
                {
                    int index = sceneManager.SelectedVertexIndex.Value;
                    if (index >= 0 && index < points.Count)
                    {
                        PointF pt = points[index];
                        g.FillEllipse(Brushes.Blue, pt.X - 6, pt.Y - 6, 12, 12);
                        g.DrawEllipse(Pens.White, pt.X - 6, pt.Y - 6, 12, 12);
                    }
                }

                // Arista
                if (sceneManager.SelectedEdge != null)
                {
                    var (a, b) = sceneManager.SelectedEdge;
                    if (a < points.Count && b < points.Count)
                    {
                        using (Pen redPen = new Pen(Color.Red, 3))
                            g.DrawLine(redPen, points[a], points[b]);
                    }
                }

                // Cara
                if (sceneManager.SelectedFace != null && IsValidFace(sceneManager.SelectedFace, points.Count))
                {
                    var facePoints = sceneManager.SelectedFace.Select(i => points[i]).ToArray();
                    using (var fillBrush = new SolidBrush(Color.FromArgb(100, 128, 0, 255)))
                    using (Pen purplePen = new Pen(Color.Purple, 3))
                    {
                        g.FillPolygon(fillBrush, facePoints);
                        g.DrawPolygon(purplePen, facePoints);
                    }
                }
            }
        }

        private bool IsValidFace(List<int> face, int pointCount)
        {
            return face.Count >= 3 && face.All(i => i >= 0 && i < pointCount);
        }
        // 1. Agrega este método al final de la clase ShapeRenderer
        private void DrawLightSource(Graphics g, Size panelSize, SceneManager sceneManager, LightSource light)
        {
            // Proyectamos la posición 3D de la luz a coordenadas 2D de la pantalla
            PointF projectedLight = Projection3D.ProjectWithCamera(light.Position, panelSize, sceneManager.Camera);

            // Definir el tamaño del indicador
            int size = 15;
            RectangleF rect = new RectangleF(projectedLight.X - size / 2, projectedLight.Y - size / 2, size, size);

            // Dibujar un resplandor (Glow)
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(rect);
                using (PathGradientBrush glowBrush = new PathGradientBrush(path))
                {
                    glowBrush.CenterColor = Color.FromArgb(255, 255, 255, 100); // Blanco amarillento
                    glowBrush.SurroundColors = new Color[] { Color.Transparent };
                    g.FillEllipse(glowBrush, projectedLight.X - 25, projectedLight.Y - 25, 50, 50);
                }
            }

            // Dibujar el centro de la luz
            using (SolidBrush sunBrush = new SolidBrush(Color.Gold))
            {
                g.FillEllipse(sunBrush, rect);
            }

            // Dibujar rayos pequeños (opcional, para estilo visual)
            using (Pen rayPen = new Pen(Color.Gold, 2))
            {
                for (int i = 0; i < 8; i++)
                {
                    double angle = i * Math.PI / 4;
                    float x1 = projectedLight.X + (float)Math.Cos(angle) * 10;
                    float y1 = projectedLight.Y + (float)Math.Sin(angle) * 10;
                    float x2 = projectedLight.X + (float)Math.Cos(angle) * 18;
                    float y2 = projectedLight.Y + (float)Math.Sin(angle) * 18;
                    g.DrawLine(rayPen, x1, y1, x2, y2);
                }
            }
        }
    }
}