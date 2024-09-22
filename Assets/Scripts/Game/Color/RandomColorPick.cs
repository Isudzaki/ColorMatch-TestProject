using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColorPick : MonoBehaviour
{
    #region Variables
    // Array to store all available ColorData
    [SerializeField]
    private ColorData[] colorOptions;

    // Current color and color name
    [HideInInspector]
    public string currentName;

    private Color currentColor;

    // Reference to the SpriteRenderer component
    private SpriteRenderer objectRenderer;
    #endregion

    #region MonoBehaviourFunctions

    private void Start()
    {
        // Get the object's Renderer
        objectRenderer = GetComponent<SpriteRenderer>();
        // Select a color at the beginning of the game
        ColorPick();
    }
    #endregion

    #region Function to select a random color
    // Function to select a random color from an array
    public void ColorPick()
    {
        if (colorOptions != null && colorOptions.Length > 0)
        {
            // Pick a random color from the array
            ColorData randomColorData = colorOptions[Random.Range(0, colorOptions.Length)];

            // Assign current values ​​to name and color
            currentName = randomColorData.colorName;
            currentColor = randomColorData.color;

            // Assign the selected color to the object
            if (objectRenderer != null)
            {
                objectRenderer.color = currentColor;
            }
        }
    }
    #endregion
}
