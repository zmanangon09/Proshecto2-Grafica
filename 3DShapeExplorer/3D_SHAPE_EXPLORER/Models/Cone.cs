using System;
using System.Collections.Generic;
using System.Linq;

namespace _3D_SHAPE_EXPLORER.Models
{
    /// <summary>
    /// Cono 3D con base circular y vértice en la parte superior.
    /// Se construye usando segmentos para aproximar la base circular.
    /// </summary>
    public class Cone : Shape3D
    {
        public override void GenerateShape()
        {
            int segments = 20;      // Número de segmentos para la base circular
            float radius = 50f;     // Radio de la base
            float height = 100f;    // Altura del cono

            Points.Clear();
            Edges.Clear();
            Faces.Clear();

            // Vértice superior del cono (apex)
            Points.Add(new Point3D(0, height / 2, 0));

            // Puntos de la base circular
            for (int i = 0; i < segments; i++)
            {
                float angle = (float)(2 * Math.PI * i / segments);
                float x = radius * (float)Math.Cos(angle);
                float z = radius * (float)Math.Sin(angle);
                Points.Add(new Point3D(x, -height / 2, z));
            }

            // Crear caras laterales (triángulos desde el apex a cada segmento de la base)
            for (int i = 0; i < segments; i++)
            {
                int current = i + 1;
                int next = (i + 1) % segments + 1;
                
                // Cara lateral triangular
                Faces.Add(new List<int> { 0, next, current });
            }

            // Crear la cara de la base (polígono circular)
            var baseFace = new List<int>();
            for (int i = 1; i <= segments; i++)
            {
                baseFace.Add(i);
            }
            Faces.Add(baseFace);

            // Crear aristas
            // Aristas desde el apex a cada punto de la base
            for (int i = 1; i <= segments; i++)
            {
                Edges.Add((0, i));
            }
            
            // Aristas de la base circular
            for (int i = 1; i <= segments; i++)
            {
                int next = (i % segments) + 1;
                Edges.Add((i, next));
            }

            OriginalPoints = Points.Select(p => new Point3D(p.X, p.Y, p.Z)).ToList();
        }

        public override Shape3D Clone()
        {
            var clone = new Cone();
            clone.OriginalPoints = this.OriginalPoints.Select(p => new Point3D(p.X, p.Y, p.Z)).ToList();
            clone.Points = clone.OriginalPoints.Select(p => new Point3D(p.X, p.Y, p.Z)).ToList();
            clone.Faces = this.Faces.Select(face => new List<int>(face)).ToList();
            clone.Edges = new List<(int, int)>(this.Edges);
            clone.IsPainted = this.IsPainted;
            clone.PaintColor = this.PaintColor;
            this.CopyTransformationsTo(clone);
            return clone;
        }
    }
}
