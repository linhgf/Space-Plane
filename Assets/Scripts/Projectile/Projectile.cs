using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    protected float speed = 10f;
    [SerializeField]
    protected Vector3 direction;
    [SerializeField]
    protected int damage = 1;

    private void Update() {
        CheckBound();
    }

    private void CheckBound(){
        if(transform.position.z > Viewport.Instance.MaxX + 30 || transform.position.z < Viewport.Instance.MinX - 30){
            Destroy(gameObject);
        }
    }

    private void OnEnable() {
        StartCoroutine(ProjectileMove());
    }

    protected virtual IEnumerator ProjectileMove(){
        while(gameObject.activeSelf){
            transform.Translate(speed * direction * Time.deltaTime);
            yield return null;
        }
    }
}
