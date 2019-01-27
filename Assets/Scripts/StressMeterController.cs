using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressMeterController : MonoBehaviour
{
    //Set this to the number of rooms present in the game.
    static int roomCount = 5;
    static float stressLevel = -1f;
    static float stressReductionAmount;
    [SerializeField]
    GameObject mask;

    void Start()
    {
        //Initialize stress meter on very first run.
        if(stressLevel == -1f)
        {
            stressLevel = mask.transform.localScale.y;
            stressReductionAmount = stressLevel / roomCount;
        }
        //Sync instance of stress meter by setting it to the existing stress level.
        SyncStressMeterMask();
    }

    void Update()
    {
        SyncStressMeterMask();
    }

    void SyncStressMeterMask()
    {
        mask.transform.localScale = new Vector3(mask.transform.localScale.x, stressLevel, mask.transform.localScale.z);
    }

    public static void DecrementStressLevel()
    {
        //Guard against decrementing stress level below 0.
        if(stressLevel >= stressReductionAmount)
        {
            stressLevel = stressLevel - stressReductionAmount;
        }
        else
        {
            stressLevel = 0f;
        }
    }
}

