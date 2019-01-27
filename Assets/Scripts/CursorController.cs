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
        clampMax = bar.GetComponent<SpriteRenderer>().bounds.size.x/2f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPosition = transform.position;
        float travelAmount = currentPosition.x + cursorSpeed;

        handleTravelDirectionChange(currentPosition);

        if (isTravellingLeft()) {
            this.transform.position = travelLeft(currentPosition);
        }
        else
        {
            this.transform.position = travelRight(currentPosition);
        }

    }

    void handleTravelDirectionChange(Vector2 currentPosition)
    {
        if (isTravellingRight() && currentPosition.x >= clampMax)
        {
            direction = TravelDirections.Left;
        }

        if (isTravellingLeft() && currentPosition.x <= -clampMax)
        {
            direction = TravelDirections.Right;
        }
    }

    Vector2 travelRight(Vector2 currentPosition)
    {
        return new Vector2(Mathf.Clamp(currentPosition.x + cursorSpeed, -clampMax, clampMax), currentPosition.y);
    }

    Vector2 travelLeft(Vector2 currentPosition)
    {
        return new Vector2(Mathf.Clamp(currentPosition.x - cursorSpeed, -clampMax, clampMax), currentPosition.y);
    }

    bool isTravellingRight()
    {
        return direction == TravelDirections.Right;
    }

    bool isTravellingLeft()
    {
        return direction == TravelDirections.Left;
    }
}
