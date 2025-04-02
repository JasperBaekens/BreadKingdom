using UnityEngine;

public class SurfacePainter : MonoBehaviour
{
    public RenderTexture paintTexture; // The texture to store painted regions
    public Camera paintCamera; // Camera for rendering
    public Material surfaceMaterial; // The material on the surface
    public Transform sphere; // The sphere that moves
    public float brushSize = 0.1f; // Size of the painted area
    private int texSize = 1024; // Resolution of the paint texture
    private Texture2D readBackTexture;

    void Start()
    {
        // Assign the render texture to the material
        surfaceMaterial.SetTexture("_PaintTex", paintTexture);

        // Initialize the readback texture for calculating the painted percentage
        readBackTexture = new Texture2D(texSize, texSize, TextureFormat.RGBA32, false);
    }

    void Update()
    {
        PaintUnderSphere();
    }

    void PaintUnderSphere()
    {
        // Convert world position to viewport position
        Vector3 viewportPos = paintCamera.WorldToViewportPoint(sphere.position);

        if (viewportPos.z > 0) // Ensure the sphere is in front of the camera
        {
            RenderTexture.active = paintTexture;
            GL.PushMatrix();
            GL.LoadPixelMatrix(0, texSize, texSize, 0);

            // Set brush color (e.g., red)
            Color brushColor = new Color(1, 0, 0, 1);
            Texture2D tempTex = new Texture2D(1, 1);
            tempTex.SetPixel(0, 0, brushColor);
            tempTex.Apply();

            // Calculate centered position
            float brushPixelSize = brushSize * texSize;
            float centeredX = (viewportPos.x * texSize) - (brushPixelSize / 2);
            float centeredY = ((1 - viewportPos.y) * texSize) - (brushPixelSize / 2);

            // Draw at the corrected position
            Graphics.DrawTexture(new Rect(centeredX, centeredY, brushPixelSize, brushPixelSize), tempTex);

            GL.PopMatrix();
            RenderTexture.active = null;



            // Draw at the converted position
            //Graphics.DrawTexture(new Rect(viewportPos.x * texSize, (1 - viewportPos.y) * texSize, brushSize * texSize, brushSize * texSize), tempTex);
            //GL.PopMatrix();
            //RenderTexture.active = null;
        }
    }

    public float GetPaintedPercentage()
    {
        // Read pixels from render texture
        RenderTexture.active = paintTexture;
        readBackTexture.ReadPixels(new Rect(0, 0, texSize, texSize), 0, 0);
        readBackTexture.Apply();
        RenderTexture.active = null;

        // Count non-transparent pixels
        Color[] pixels = readBackTexture.GetPixels();
        int paintedCount = 0;
        foreach (Color pixel in pixels)
        {
            if (pixel.r > 0) // If red channel is painted
                paintedCount++;
        }

        return (float)paintedCount / pixels.Length * 100f; // Return percentage
    }
}
