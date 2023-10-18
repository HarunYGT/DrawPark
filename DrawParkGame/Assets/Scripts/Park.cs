using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park : MonoBehaviour
{
    public Route route;
    public SpriteRenderer spriteRenderer;
    [SerializeField] ParticleSystem fx;
    private ParticleSystem.MainModule fxMainModule;
    public Car _car;

    private void Start()
    {
        fxMainModule = fx.main;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Car car))
        {
            if(car.route == route)
            {
                _car.StopDancingAnim();
                Game.Instance.CheckPark();
                StartFx();
            }
        }
    }
    private void StartFx()
    {
        fxMainModule.startColor = route.carColor;
        fx.Play(); 
    }
    public void SetColor(Color color)
    {
        spriteRenderer.color = color;  
    }
}
