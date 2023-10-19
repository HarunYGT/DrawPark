using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening; 

public class Car : MonoBehaviour
{
    public Route route;
    public Transform bottomTransform;
    public Transform bodyTransform;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] ParticleSystem explosionFX;
    [SerializeField] Rigidbody rb;
    [SerializeField] float danceValue;
    [SerializeField] float durationMultiplier;

    private void Start()
    {
        bodyTransform.DOLocalMoveY(danceValue,.1f).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.Linear); 
    } 
    public void Move(Vector3[] path)
    {
        rb.DOLocalPath(path,2f*durationMultiplier*path.Length)
            .SetLookAt(.01f , false)
            .SetEase(Ease.Linear);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.TryGetComponent(out Car otherCar))
        {
            StopDancingAnim();
            rb.DOKill(true);

            //add explosion FX
            Vector3 hitPoint = collision.contacts[0].point;
            AddExplosionForce(hitPoint);
            explosionFX.Play();

            Game.Instance.OnCarCollision(); 
        }
    }
    private void AddExplosionForce(Vector3 point)
    {
        rb.AddExplosionForce(400f,point,3f);
        rb.AddForceAtPosition(Vector3.up*2f,point,ForceMode.Impulse);
        rb.AddTorque(new Vector3(GetRandomFloat(),GetRandomFloat(),GetRandomFloat()));
    }
    private float GetRandomFloat()
    {
        float angle = 10f;
        float rand = Random.value;
        return rand > .5f ? angle : -angle;
    }
    public void StopDancingAnim()
    {
        bodyTransform.DOKill(true);
    }
    public void SetColor(Color color)
    {
        meshRenderer.sharedMaterials[0].color = color;  
    }
}
