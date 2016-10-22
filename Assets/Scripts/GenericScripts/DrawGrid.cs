using UnityEngine;
using System.Collections;

public class DrawGrid : MonoBehaviour
{
    public bool showMain = true;

    public int gridSizeX = 100;
    public int gridSizeY = 100;
    public int gridSizeZ;

    public float largeStep;

    public float startX;
    public float startY;
    public float startZ;

    public float transformX = -50f;
    public float transformY = -50f;
    public float transformZ;

    private float offsetY = 0;

    public Material lineMaterial;

    private Color mainColor = new Color(0.8f, 0.8f, 0.8f, 1f);

    void Start()
    {

    }

    void Update()
    {

    }

    void CreateLineMaterial()
    {

        if (!lineMaterial)
        {
            Debug.Log("No material");
            return;
        }
    }

    void OnPostRender()
    {
        CreateLineMaterial();
        // set the current material
        lineMaterial.SetPass(0);

        GL.Begin(GL.LINES);

        if (largeStep <= 0)
            largeStep = 0.5f;

        if (showMain)
        {
            GL.Color(mainColor);

            // Layers
            for (float j = 0; j <= gridSizeY; j += largeStep)
            {
                // X axis lines
                for (float i = 0; i <= gridSizeZ; i += largeStep)
                {
                    GL.Vertex3(startX + transformX, j + offsetY + transformY, startZ + i + transformZ);
                    GL.Vertex3(gridSizeX + transformX, j + offsetY + transformY, startZ + i + transformZ);
                }
            }

            // Y axis lines
            for (float i = 0; i <= gridSizeZ; i += largeStep)
            {
                for (float k = 0; k <= gridSizeX; k += largeStep)
                {
                    GL.Vertex3(startX + k + transformX, startY + offsetY + transformY, startZ + i + transformZ);
                    GL.Vertex3(startX + k + transformX, gridSizeY + offsetY + transformY, startZ + i + transformZ);
                }
            }
        }
        
        GL.End();
    }
}