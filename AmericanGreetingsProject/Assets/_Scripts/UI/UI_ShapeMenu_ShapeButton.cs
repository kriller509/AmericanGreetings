using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ShapeMenu_ShapeButton : MonoBehaviour
{
    [Header("Adjustable")]
    public GameShape.Shape shape;

    public void OnClick()
    {
        UI_ShapeMenu.Instance.SetShape(shape, true);
    }
}
