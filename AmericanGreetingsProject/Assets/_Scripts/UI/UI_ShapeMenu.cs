using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Random = UnityEngine.Random;

public class UI_ShapeMenu : MonoBehaviour
{
    #region Singleton
    public static UI_ShapeMenu Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    [Header("References/General")]
    public AudioSource audioSource_changeShape;
    public GameShape theShapeObject;

    [Header("References/Text")]
    public TextMeshProUGUI text_shape;

    [Header("Adjustable")]
    public GameShape.Shape defaultShape = GameShape.Shape.Hexagon;

    // ________________________________________________________ Unity Functions

    private void Start()
    {
        SetShape(defaultShape, false);
    }

    // ________________________________________________________ Public Functions

    public void SetShape(GameShape.Shape newShape, bool playAudio)
    {
        text_shape.text = $"The {newShape}";
        theShapeObject.SetShape(newShape);

        if (playAudio)
        {
            if (audioSource_changeShape.isPlaying)
            {
                audioSource_changeShape.Stop();
            }
            audioSource_changeShape.Play();
        }
    }

    // ________________________________________________________ Events

    public void OnClick_BecomeRandomShape()
    {
        int shapesCount = Enum.GetValues(typeof(GameShape.Shape)).Length;

        GameShape.Shape randomShape = (GameShape.Shape)Random.Range(0, shapesCount);

        SetShape(randomShape, true);
    }

    public void OnClick_Quit()
    {
        Application.Quit();
    }

}
