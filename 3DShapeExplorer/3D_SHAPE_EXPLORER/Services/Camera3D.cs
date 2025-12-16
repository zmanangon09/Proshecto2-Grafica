using System;
using System.Drawing;
using _3D_SHAPE_EXPLORER.Models;

namespace _3D_SHAPE_EXPLORER.Services
{
    /// <summary>
    /// Sistema de cámara orbital 3D que permite observar la escena desde diferentes ángulos.
    /// Soporta rotación orbital (alrededor de un punto), zoom y paneo.
    /// </summary>
    public class Camera3D
    {
        // Ángulos de rotación orbital (en grados)
        public float Yaw { get; set; } = 0f;      // Rotación horizontal
        public float Pitch { get; set; } = 0f;    // Rotación vertical
        
        // Distancia de la cámara al punto objetivo
        public float Distance { get; set; } = 500f;
        
        // Punto objetivo (hacia donde mira la cámara)
        public float TargetX { get; set; } = 0f;
        public float TargetY { get; set; } = 0f;
        public float TargetZ { get; set; } = 0f;

        // Límites
        private const float MinDistance = 100f;
        private const float MaxDistance = 2000f;
        private const float MaxPitch = 89f;
        private const float MinPitch = -89f;

        // Velocidades de movimiento
        public float RotationSpeed { get; set; } = 2f;
        public float ZoomSpeed { get; set; } = 20f;
        public float PanSpeed { get; set; } = 5f;

        /// <summary>
        /// Rota la cámara horizontalmente (izquierda/derecha)
        /// </summary>
        public void RotateYaw(float delta)
        {
            Yaw += delta * RotationSpeed;
            // Normalizar el ángulo entre 0 y 360
            Yaw = Yaw % 360f;
            if (Yaw < 0) Yaw += 360f;
        }

        /// <summary>
        /// Rota la cámara verticalmente (arriba/abajo)
        /// </summary>
        public void RotatePitch(float delta)
        {
            Pitch += delta * RotationSpeed;
            // Limitar el pitch para evitar gimbal lock
            Pitch = Math.Max(MinPitch, Math.Min(MaxPitch, Pitch));
        }

        /// <summary>
        /// Acerca o aleja la cámara (zoom)
        /// </summary>
        public void Zoom(float delta)
        {
            Distance -= delta * ZoomSpeed;
            Distance = Math.Max(MinDistance, Math.Min(MaxDistance, Distance));
        }

        /// <summary>
        /// Mueve el punto objetivo de la cámara (paneo)
        /// </summary>
        public void Pan(float deltaX, float deltaY)
        {
            // Calcular vectores right y up basados en la orientación de la cámara
            float yawRad = DegreesToRadians(Yaw);
            
            // Vector derecha (perpendicular a la dirección de vista)
            float rightX = (float)Math.Cos(yawRad);
            float rightZ = (float)Math.Sin(yawRad);
            
            // Mover el target
            TargetX += rightX * deltaX * PanSpeed;
            TargetZ += rightZ * deltaX * PanSpeed;
            TargetY += deltaY * PanSpeed;
        }

        /// <summary>
        /// Reinicia la cámara a su posición inicial
        /// </summary>
        public void Reset()
        {
            Yaw = 0f;
            Pitch = 0f;
            Distance = 500f;
            TargetX = 0f;
            TargetY = 0f;
            TargetZ = 0f;
        }

        /// <summary>
        /// Transforma un punto 3D según la posición de la cámara
        /// </summary>
        public Point3D TransformPoint(Point3D point)
        {
            // 1. Trasladar al origen relativo al target
            float x = point.X - TargetX;
            float y = point.Y - TargetY;
            float z = point.Z - TargetZ;

            // 2. Aplicar rotación Yaw (horizontal)
            float yawRad = DegreesToRadians(-Yaw);
            float x1 = x * (float)Math.Cos(yawRad) - z * (float)Math.Sin(yawRad);
            float z1 = x * (float)Math.Sin(yawRad) + z * (float)Math.Cos(yawRad);
            x = x1;
            z = z1;

            // 3. Aplicar rotación Pitch (vertical)
            float pitchRad = DegreesToRadians(-Pitch);
            float y1 = y * (float)Math.Cos(pitchRad) - z * (float)Math.Sin(pitchRad);
            float z2 = y * (float)Math.Sin(pitchRad) + z * (float)Math.Cos(pitchRad);
            y = y1;
            z = z2;

            return new Point3D(x, y, z);
        }

        /// <summary>
        /// Proyecta un punto 3D transformado por la cámara a coordenadas 2D de pantalla
        /// </summary>
        public PointF Project(Point3D point, Size panelSize)
        {
            // Transformar el punto según la cámara
            Point3D transformed = TransformPoint(point);

            // Proyección perspectiva
            float zOffset = transformed.Z + Distance;
            if (zOffset < 1) zOffset = 1; // Evitar división por cero

            float factor = Distance / zOffset;
            float screenX = transformed.X * factor + panelSize.Width / 2f;
            float screenY = -transformed.Y * factor + panelSize.Height / 2f;

            return new PointF(screenX, screenY);
        }

        /// <summary>
        /// Obtiene información del estado actual de la cámara para mostrar en UI
        /// </summary>
        public string GetStatusText()
        {
            return $"?? Cámara: Yaw={Yaw:F0}° | Pitch={Pitch:F0}° | Zoom={Distance:F0}";
        }

        private float DegreesToRadians(float degrees)
        {
            return (float)(Math.PI / 180.0) * degrees;
        }
    }
}
