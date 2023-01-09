using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPointsManager : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float specialPoints = 0.1f;

    public static SpecialPointsManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    
    public float GetSpecialPoints()
    {
        return specialPoints;
    }
    public void SetSpecialPoints(float points)
    {
        specialPoints = points;
    }

    public void AddSpecialPoints(float points)
    {
        specialPoints += points;
    }
}
