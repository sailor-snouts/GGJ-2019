using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBarController : MonoBehaviour
{
    private float cursorSpeed = 0.1f;
    float minGreenPosition = -2.5f;
    float maxGreenPosition = 2.5f;
    float barWidth = 10.5f;
    [SerializeField]
    GameObject frame;
    [SerializeField]
    GameObject mask;
    [SerializeField]
    GameObject cursor;
    enum TravelDirections { Left, Right };
    TravelDirections direction = TravelDirections.Right;
    public InteractableBase interactable;

    public void Reset()
    {
        cursorSpeed = Random.Range(0.2f, 0.5f);
        SetGreenZonePosition();
    }

    private void OnEnable()
    {
        Reset();
    }

    void Update()
    {
        HandleTravelDirectionChange();
        Travel();
        Interact();
    }

    void SetGreenZonePosition()
    {
        float greenZoneX = Random.Range(minGreenPosition, maxGreenPosition);
        mask.transform.position = new Vector3(transform.position.x + greenZoneX, mask.transform.position.y, mask.transform.position.z);
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
            cursor.transform.position -= new Vector3(cursorSpeed, 0, 0);
        }
        else
        {
            cursor.transform.position += new Vector3(cursorSpeed, 0, 0);
        }
    }

    void Interact()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2"))
        {
            Vector2 greenZonePosition = mask.GetComponent<Transform>().position;
            float greenZoneStart = greenZonePosition.x - mask.GetComponent<Transform>().localScale.x;
            float greenZoneEnd = greenZonePosition.x + mask.GetComponent<Transform>().localScale.x;
            if (cursor.transform.position.x >= greenZoneStart && cursor.transform.position.x <= greenZoneEnd)
            {
                StressMeterController.DecrementStressLevel();
                interactable.gameWon();
            }
            else
            {
                Reset();
            }
        }
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
        return cursor.transform.position.x >= this.transform.position.x + barWidth/2;
    }

    bool IsAtLeftBoundary()
    {
        return cursor.transform.position.x <= this.transform.position.x - barWidth/2;
    }
}
