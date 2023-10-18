using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class Game : MonoBehaviour
{
    public static Game Instance;

    [HideInInspector] public List<Route> readyRoutes = new();
    private int totalRoutes;
    public static int successPark;
 
    private void Awake()
    {
        Instance = this;
    } 
    private void Start()
    {
        totalRoutes = transform.GetComponentsInChildren<Route>().Length;
        successPark = 0;
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
