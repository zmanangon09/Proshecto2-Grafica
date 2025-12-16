using System;
using System.Collections.Generic;
using System.Linq;

namespace _3D_SHAPE_EXPLORER.Models
{
    /// <summary>
    /// Esfera aproximada mediante una icosfera (subdivisión de un icosaedro).
    /// Esta es una técnica común en computación gráfica para aproximar superficies curvas.
    /// </summary>
    public class Sphere : Shape3D
    {
        private int subdivisions = 1; // Nivel de subdivisión (1 = icosaedro básico, 2+ = más suave)

        public override void GenerateShape()
        {
            float radius = 50f;
            Points.Clear();
            Edges.Clear();
            Faces.Clear();

            // Crear icosaedro base
            float t = (1.0f + (float)Math.Sqrt(5.0)) / 2.0f;

            // Vértices del icosaedro
            var vertices = new List<Point3D>
            {
                Normalize(new Point3D(-1,  t,  0), radius),
                Normalize(new Point3D( 1,  t,  0), radius),
                Normalize(new Point3D(-1, -t,  0), radius),
                Normalize(new Point3D( 1, -t,  0), radius),
                
                Normalize(new Point3D( 0, -1,  t), radius),
                Normalize(new Point3D( 0,  1,  t), radius),
                Normalize(new Point3D( 0, -1, -t), radius),
                Normalize(new Point3D( 0,  1, -t), radius),
                
                Normalize(new Point3D( t,  0, -1), radius),
                Normalize(new Point3D( t,  0,  1), radius),
                Normalize(new Point3D(-t,  0, -1), radius),
                Normalize(new Point3D(-t,  0,  1), radius),
            };

            // Caras del icosaedro (20 triángulos)
            var faces = new List<List<int>>
            {
                // 5 caras alrededor del punto 0
                new List<int> { 0, 11, 5 },
                new List<int> { 0, 5, 1 },
                new List<int> { 0, 1, 7 },
                new List<int> { 0, 7, 10 },
                new List<int> { 0, 10, 11 },

                // 5 caras adyacentes
                new List<int> { 1, 5, 9 },
                new List<int> { 5, 11, 4 },
                new List<int> { 11, 10, 2 },
                new List<int> { 10, 7, 6 },
                new List<int> { 7, 1, 8 },

                // 5 caras alrededor del punto 3
                new List<int> { 3, 9, 4 },
                new List<int> { 3, 4, 2 },
                new List<int> { 3, 2, 6 },
                new List<int> { 3, 6, 8 },
                new List<int> { 3, 8, 9 },

                // 5 caras adyacentes
                new List<int> { 4, 9, 5 },
                new List<int> { 2, 4, 11 },
                new List<int> { 6, 2, 10 },
                new List<int> { 8, 6, 7 },
                new List<int> { 9, 8, 1 },
            };

            // Subdividir para obtener más triángulos (esfera más suave)
            for (int i = 0; i < subdivisions; i++)
            {
                var newFaces = new List<List<int>>();
                var midpointCache = new Dictionary<long, int>();

                foreach (var face in faces)
                {
                    int a = GetMiddlePoint(face[0], face[1], vertices, midpointCache, radius);
                    int b = GetMiddlePoint(face[1], face[2], vertices, midpointCache, radius);
                    int c = GetMiddlePoint(face[2], face[0], vertices, midpointCache, radius);

                    newFaces.Add(new List<int> { face[0], a, c });
                    newFaces.Add(new List<int> { face[1], b, a });
                    newFaces.Add(new List<int> { face[2], c, b });
                    newFaces.Add(new List<int> { a, b, c });
                }

                faces = newFaces;
            }

            Points = vertices;
            Faces = faces;

            // Generar aristas a partir de las caras
            var edgeSet = new HashSet<(int, int)>();
            foreach (var face in Faces)
            {
                for (int i = 0; i < face.Count; i++)
                {
                    int a = face[i];
                    int b = face[(i + 1) % face.Count];
                    var edge = a < b ? (a, b) : (b, a);
                    edgeSet.Add(edge);
                }
            }
            Edges = edgeSet.ToList();

            OriginalPoints = Points.Select(p => new Point3D(p.X, p.Y, p.Z)).ToList();
        }

        private Point3D Normalize(Point3D p, float radius)
        {
            float length = (float)Math.Sqrt(p.X * p.X + p.Y * p.Y + p.Z * p.Z);
            return new Point3D(
                p.X / length * radius,
                p.Y / length * radius,
                p.Z / length * radius
            );
        }

        private int GetMiddlePoint(int p1, int p2, List<Point3D> vertices, Dictionary<long, int> cache, float radius)
        {
            bool firstIsSmaller = p1 < p2;
            long smallerIndex = firstIsSmaller ? p1 : p2;
            long greaterIndex = firstIsSmaller ? p2 : p1;
            long key = (smallerIndex << 32) + greaterIndex;

            if (cache.TryGetValue(key, out int ret))
            {
                return ret;
            }

            var point1 = vertices[p1];
            var point2 = vertices[p2];
            var middle = new Point3D(
                (point1.X + point2.X) / 2f,
                (point1.Y + point2.Y) / 2f,
                (point1.Z + point2.Z) / 2f
            );

            // Normalizar al radio de la esfera
            middle = Normalize(middle, radius);

            int index = vertices.Count;
            vertices.Add(middle);
            cache.Add(key, index);

            return index;
        }

        public override Shape3D Clone()
        {
            var clone = new Sphere();
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
