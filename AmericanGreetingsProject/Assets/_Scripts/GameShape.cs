using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameShape : MonoBehaviour, IPointerClickHandler
{
    public enum Shape
    {
        Square = 0,
        Circle = 1,
        Hexagon = 2,
        Diamond = 3,
        Arrow = 4,
        Heart = 5
    };

    [Header("References")]
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer shadowRenderer;

    [Header("Adjustable/Sprites")]
    public Sprite sprite_square;
    public Sprite sprite_circle;
    public Sprite sprite_hexagon;
    public Sprite sprite_diamond;
    public Sprite sprite_arrow;
    public Sprite sprite_heart;

    [Header("Adjustable")]
    public float colorLerpSpeed = 1f;
    public float defaultScale = 0.25f;
    public float expandedScale = 0.4f;
    public float scaleLerpSpeed = 2f;

    [Header("Debugging (Observe Only)")]
    public Color lastColor;
    public Color goalColor;
    public float colorLerpProgress;

    public float lastScale;
    public float goalScale;
    public float scaleLerpProgress;

    // ________________________________________________________ Unity Functions

    private void FixedUpdate()
    {
        // No need to do this again if the result will be the same as it already is
        if (colorLerpProgress < 1)
        {
            colorLerpProgress += (Time.fixedDeltaTime * colorLerpSpeed);
            spriteRenderer.color = Color.Lerp(lastColor, goalColor, colorLerpProgress);
        }

        if (spriteRenderer.transform.localScale.x != goalScale)
        {
            scaleLerpProgress += (Time.fixedDeltaTime * scaleLerpSpeed);

            float newScale = Mathf.Lerp(lastScale, goalScale, scaleLerpProgress);
            transform.localScale = new Vector3(newScale, newScale, newScale);
        }
    }

    // ________________________________________________________ Public Functions

    public void SetShape(GameShape.Shape newShape)
    {
        spriteRenderer.sprite = GetShapeSprite(newShape);
        shadowRenderer.sprite = spriteRenderer.sprite;

        SetAsExpanded();

        // Why do we need a shadow?
        // Because certain colors might be hard to see against the background
    }

    public void SetColor(Color newColor)
    {
        colorLerpProgress = 0;

        lastColor = goalColor;
        goalColor = newColor;
    }

    public void SetAsExpanded()
    {
        scaleLerpProgress = 0;

        transform.localScale = new Vector3(expandedScale, expandedScale, expandedScale);
        lastScale = expandedScale;
        goalScale = defaultScale;
    }

    // ________________________________________________________ Interface Functions

    public void OnPointerClick(PointerEventData eventData)
    {
        // Double click
        if (eventData.clickCount == 2)
        {
            SetToRandomColor();
        }
    }

    // ________________________________________________________ Private Functions

    private void SetToRandomColor()
    {
        Color randomColor = new Color
            (
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f)
            );

        SetColor(randomColor);
    }

    private Sprite GetShapeSprite(GameShape.Shape shape)
    {
        switch(shape)
        {
            default:
                Debug.LogError("Failed to find sprite for shape (" + shape + "), defaulting to hexagon");
                return sprite_hexagon;

            case Shape.Square:
                return sprite_square;

            case Shape.Circle:
                return sprite_circle;

            case Shape.Hexagon:
                return sprite_hexagon;

            case Shape.Diamond:
                return sprite_diamond;

            case Shape.Arrow:
                return sprite_arrow;

            case Shape.Heart:
                return sprite_heart;
        }
    }


}
