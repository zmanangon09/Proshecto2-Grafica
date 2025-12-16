using System;

namespace _3D_SHAPE_EXPLORER.Services
{
    /// <summary>
    /// Controlador de cámara 3D orbital que permite observar la escena desde diferentes ángulos.
    /// Implementa rotación orbital alrededor de un punto focal y control de zoom.
    /// </summary>
    public class CameraController
    {
        // Ángulos de rotación de la cámara (en grados)
        public float Yaw { get; set; } = 0f;      // Rotación horizontal
        public float Pitch { get; set; } = 0f;    // Rotación vertical
        
        // Distancia de la cámara al punto focal
        public float Distance { get; set; } = 500f;
        
        // Límites de la cámara
        public float MinDistance { get; set; } = 200f;
        public float MaxDistance { get; set; } = 1500f;
        public float MinPitch { get; set; } = -89f;
        public float MaxPitch { get; set; } = 89f;
        
        // Velocidades de movimiento
        public float RotationSpeed { get; set; } = 2f;
        public float ZoomSpeed { get; set; } = 20f;

        // Punto focal de la cámara (centro de la escena)
        public float FocalPointX { get; set; } = 0f;
        public float FocalPointY { get; set; } = 0f;
        public float FocalPointZ { get; set; } = 0f;

        /// <summary>
        /// Rota la cámara horizontalmente (Yaw)
        /// </summary>
        public void RotateLeft()
        {
            Yaw -= RotationSpeed;
            if (Yaw < -180) Yaw += 360;
        }

        /// <summary>
        /// Rota la cámara horizontalmente (Yaw)
        /// </summary>
        public void RotateRight()
        {
            Yaw += RotationSpeed;
            if (Yaw > 180) Yaw -= 360;
        }

        /// <summary>
        /// Rota la cámara verticalmente hacia arriba (Pitch)
        /// </summary>
        public void RotateUp()
        {
            Pitch = Math.Min(MaxPitch, Pitch + RotationSpeed);
        }

        /// <summary>
        /// Rota la cámara verticalmente hacia abajo (Pitch)
        /// </summary>
        public void RotateDown()
        {
            Pitch = Math.Max(MinPitch, Pitch - RotationSpeed);
        }

        /// <summary>
        /// Acerca la cámara (Zoom In)
        /// </summary>
        public void ZoomIn()
        {
            Distance = Math.Max(MinDistance, Distance - ZoomSpeed);
        }

        /// <summary>
        /// Aleja la cámara (Zoom Out)
        /// </summary>
        public void ZoomOut()
        {
            Distance = Math.Min(MaxDistance, Distance + ZoomSpeed);
        }

        /// <summary>
        /// Reinicia la cámara a su posición inicial
        /// </summary>
        public void Reset()
        {
            Yaw = 0f;
            Pitch = 0f;
            Distance = 500f;
            FocalPointX = 0f;
            FocalPointY = 0f;
            FocalPointZ = 0f;
        }

        /// <summary>
        /// Establece una vista predefinida: Frontal
        /// </summary>
        public void SetFrontView()
        {
            Yaw = 0f;
            Pitch = 0f;
        }

        /// <summary>
        /// Establece una vista predefinida: Superior
        /// </summary>
        public void SetTopView()
        {
            Yaw = 0f;
            Pitch = 89f;
        }

        /// <summary>
        /// Establece una vista predefinida: Lateral derecha
        /// </summary>
        public void SetRightView()
        {
            Yaw = 90f;
            Pitch = 0f;
        }

        /// <summary>
        /// Establece una vista predefinida: Lateral izquierda
        /// </summary>
        public void SetLeftView()
        {
            Yaw = -90f;
            Pitch = 0f;
        }

        /// <summary>
        /// Establece una vista predefinida: Posterior
        /// </summary>
        public void SetBackView()
        {
            Yaw = 180f;
            Pitch = 0f;
        }

        /// <summary>
        /// Establece una vista predefinida: Isométrica
        /// </summary>
        public void SetIsometricView()
        {
            Yaw = 45f;
            Pitch = 35f;
        }

        /// <summary>
        /// Obtiene la información actual de la cámara como texto
        /// </summary>
        public string GetCameraInfo()
        {
            return $"Cámara: Yaw={Yaw:F1}° | Pitch={Pitch:F1}° | Zoom={Distance:F0}";
        }

        /// <summary>
        /// Convierte grados a radianes
        /// </summary>
        private float DegreesToRadians(float degrees)
        {
            return (float)(Math.PI / 180.0) * degrees;
        }

        /// <summary>
        /// Obtiene el ángulo Yaw en radianes
        /// </summary>
        public float GetYawRadians()
        {
            return DegreesToRadians(Yaw);
        }

        /// <summary>
        /// Obtiene el ángulo Pitch en radianes
        /// </summary>
        public float GetPitchRadians()
        {
            return DegreesToRadians(Pitch);
        }
    }
}
