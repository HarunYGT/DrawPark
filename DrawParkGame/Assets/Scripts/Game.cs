using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;


public class Game : MonoBehaviour
{
    public static Game Instance;

    [HideInInspector] public List<Route> readyRoutes = new();
    private int totalRoutes;
    public static int successPark;
    
    public UnityAction OnCarCollision;

    private void Awake()
    {
        Instance = this;
    } 
    private void Start()
    {
        totalRoutes = transform.GetComponentsInChildren<Route>().Length;
        successPark = 0;
        OnCarCollision += OnCarCollisionHandler; 
    }

    private void OnCarCollisionHandler()
    {
        Debug.Log("Game Over");
        DOVirtual.DelayedCall(2f, () => 
            {
                int currentLevel = SceneManager.GetActiveScene().buildIndex;
                if(currentLevel < SceneManager.sceneCountInBuildSettings)
                    SceneManager.LoadScene(currentLevel);    
            });
    }

    public void RegisterRoute(Route route)
    {
        readyRoutes.Add(route);

        if(readyRoutes.Count ==totalRoutes)
            MoveAllCars();
    }
    private void MoveAllCars()
    {
        foreach (var route in readyRoutes)
        {
            route.car.Move(route.linePoints);
        }
    }
    public void CheckPark()
    {
        successPark++;
        if(successPark == totalRoutes)
        {
            Debug.Log("You Win");
            int nextLevel = SceneManager.GetActiveScene().buildIndex+1;
            DOVirtual.DelayedCall(1.3f, () => 
            {
                if(nextLevel < SceneManager.sceneCountInBuildSettings)
                {
                    SceneManager.LoadScene(nextLevel);
                }
                else
                {
                    Debug.LogWarning("No Next Level to load");
                }
            });
        }
    }
}
