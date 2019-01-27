using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBarController : MonoBehaviour
{
    float clampMax;
    private float cursorSpeed = 0.1f;
    [SerializeField]
    GameObject frame;
    [SerializeField]
    GameObject mask;
    [SerializeField]
    GameObject cursor;
    enum TravelDirections { Left, Right };
    TravelDirections direction = TravelDirections.Right;

    void Start()
    { 
        //This is built off the assumption that frame's position.x is at 0.
        clampMax = (frame.GetComponent<SpriteRenderer>().bounds.size.x/2f) - 0.5f;
        cursorSpeed = Random.Range(0.1f, 0.45f);
        SetGreenZonePosition();
    }

    void Update()
    {
        HandleTravelDirectionChange();
        Travel();
        Interact();
    }

    void SetGreenZonePosition()
    {
        float greenZoneX = Random.Range(0, clampMax);
        //Randomly make range negative
        if (Random.Range(0, 100) <= 50)
        {
            greenZoneX = greenZoneX * -1f;
        }
        mask.transform.position = new Vector3(greenZoneX, mask.transform.position.y, mask.transform.position.z);
    }

    void HandleTravelDirectionChange()
    {
        if (IsTravellingRight() && IsAtRightBoundary())
        {
            direction = TravelDirections.Left;
        }

        if (IsTravellingLeft() && IsAtLeftBoundary())
        {
            direction = TravelDirections.Right;
        }
    }

    void Travel()
    {
        if (IsTravellingLeft())
        {
            cursor.transform.position = TravelLeft();
        }
        else
        {
            cursor.transform.position = TravelRight();
        }
    }

    void Interact()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector2 greenZonePosition = mask.GetComponent<Transform>().position;
            float greenZoneStart = greenZonePosition.x - mask.GetComponent<Transform>().localScale.x;
            float greenZoneEnd = greenZonePosition.x + mask.GetComponent<Transform>().localScale.x;
            if (cursor.transform.position.x >= greenZoneStart && cursor.transform.position.x <= greenZoneEnd)
            {
                Debug.Log("YOU WIN");
                StressMeterController.DecrementStressLevel();
            }
        }
    }

    Vector2 TravelRight()
    {
        return new Vector2(Mathf.Clamp(cursor.transform.position.x + cursorSpeed, -clampMax, clampMax), cursor.transform.position.y);
    }

    Vector2 TravelLeft()
    {
        return new Vector2(Mathf.Clamp(cursor.transform.position.x - cursorSpeed, -clampMax, clampMax), cursor.transform.position.y);
    }

    bool IsTravellingRight()
    {
        return direction == TravelDirections.Right;
    }

    bool IsTravellingLeft()
    {
        return direction == TravelDirections.Left;
    }

    bool IsAtRightBoundary()
    {
        return cursor.transform.position.x >= clampMax;
    }

    bool IsAtLeftBoundary()
    {
        return cursor.transform.position.x <= -clampMax;
    }
}
