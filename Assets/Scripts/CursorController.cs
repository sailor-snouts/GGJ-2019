using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    float clampMax;
    [SerializeField, Range(0.1f, 0.5f)]
    private float cursorSpeed = 0.1f;
    [SerializeField]
    GameObject bar;
    enum TravelDirections { Left, Right };
    TravelDirections direction = TravelDirections.Right;


    // Start is called before the first frame update
    void Start()
    { 
        clampMax = (bar.GetComponent<SpriteRenderer>().bounds.size.x/2f) - 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPosition = transform.position;
        float travelAmount = currentPosition.x + cursorSpeed;

        HandleTravelDirectionChange(currentPosition);

        if (IsTravellingLeft()) {
            this.transform.position = TravelLeft(currentPosition);
        }
        else
        {
            this.transform.position = TravelRight(currentPosition);
        }

    }

    void HandleTravelDirectionChange(Vector2 currentPosition)
    {
        if (IsTravellingRight() && currentPosition.x >= clampMax)
        {
            direction = TravelDirections.Left;
        }

        if (IsTravellingLeft() && currentPosition.x <= -clampMax)
        {
            direction = TravelDirections.Right;
        }
    }

    Vector2 TravelRight(Vector2 currentPosition)
    {
        return new Vector2(Mathf.Clamp(currentPosition.x + cursorSpeed, -clampMax, clampMax), currentPosition.y);
    }

    Vector2 TravelLeft(Vector2 currentPosition)
    {
        return new Vector2(Mathf.Clamp(currentPosition.x - cursorSpeed, -clampMax, clampMax), currentPosition.y);
    }

    bool IsTravellingRight()
    {
        return direction == TravelDirections.Right;
    }

    bool IsTravellingLeft()
    {
        return direction == TravelDirections.Left;
    }
}
