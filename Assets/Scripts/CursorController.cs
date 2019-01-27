using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    float clampMax;
    private float cursorSpeed = 0.1f;
    [SerializeField]
    GameObject bar;
    [SerializeField]
    GameObject greenZone;
    enum TravelDirections { Left, Right };
    TravelDirections direction = TravelDirections.Right;

    // Start is called before the first frame update
    void Start()
    { 
        clampMax = (bar.GetComponent<SpriteRenderer>().bounds.size.x/2f) - 0.5f;
        cursorSpeed = Random.Range(0.1f, 0.45f);
        SetGreenZonePosition();
    }

    // Update is called once per frame
    void Update()
    {
        HandleTravelDirectionChange();
        Travel();
        Interact();
    }

    void SetGreenZonePosition()
    {
        float greenZoneX = Random.Range(0, clampMax);
        if (Random.Range(0, 100) <= 50)
        {
            greenZoneX = greenZoneX * -1f;
        }
        greenZone.transform.position = new Vector3(greenZoneX, greenZone.transform.position.y, greenZone.transform.position.z);
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
            this.transform.position = TravelLeft();
        }
        else
        {
            this.transform.position = TravelRight();
        }
    }

    void Interact()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector2 greenZonePosition = greenZone.GetComponent<Transform>().position;
            float greenZoneStart = greenZonePosition.x - greenZone.GetComponent<Transform>().localScale.x;
            float greenZoneEnd = greenZonePosition.x + greenZone.GetComponent<Transform>().localScale.x;
            if (transform.position.x >= greenZoneStart && transform.position.x <= greenZoneEnd)
            {
                Debug.Log("YOU WIN");
            }
        }
    }

    Vector2 TravelRight()
    {
        return new Vector2(Mathf.Clamp(transform.position.x + cursorSpeed, -clampMax, clampMax), transform.position.y);
    }

    Vector2 TravelLeft()
    {
        return new Vector2(Mathf.Clamp(transform.position.x - cursorSpeed, -clampMax, clampMax), transform.position.y);
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
        return transform.position.x >= clampMax;
    }

    bool IsAtLeftBoundary()
    {
        return transform.position.x <= -clampMax;
    }
}
