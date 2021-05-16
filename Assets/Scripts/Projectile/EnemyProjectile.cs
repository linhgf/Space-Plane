using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile{

    [SerializeField]
    private GameObject hit_vfx;
    private void OnTriggerEnter(Collider other) {
        if (other.transform.tag.Equals("Player"))
        {
            var hit = Instantiate(hit_vfx, transform.position, hit_vfx.transform.rotation);
            Vector3 direction = (transform.position - other.transform.position).normalized;
            Quaternion rotate = Quaternion.LookRotation(direction);
            hit.transform.rotation = rotate;
            other.gameObject.GetComponent<PlayerController>().Hurt(damage);
            Destroy(gameObject);
        }
    }

}
