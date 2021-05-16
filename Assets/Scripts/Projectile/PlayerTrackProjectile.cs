using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerTrackProjectile : PlayerProjectile
{
    private Transform target;
    private Vector3 controll;

    private void Awake() {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length == 0){
            target = null;
            return;
        }
        target = GameObject.FindGameObjectsWithTag("Enemy")[0].transform;
        controll = transform.position;
        controll.x = controll.x + Random.Range(100, 200);
        controll.y = controll.y + Random.Range(-100, 100);
    }

    protected override IEnumerator ProjectileMove()
    {
        if(target == null){
            while(gameObject.activeSelf){
                transform.Translate(speed * direction * Time.deltaTime);
                yield return null;
            }
        }
        else{
            Vector3[] paths = BezierUtils.Instance.GetBeizerList(transform.position, controll, target.position, 25);
            int i = 0;
            while (i < paths.Length)
            {
                paths[i].x = 0;
                Vector3 direction = (paths[i] - transform.position).normalized;
                float angle = Vector3.Angle(transform.right, direction) * (transform.position.y > paths[i].y ? -1 : 1);
                transform.DORotate(new Vector3(0, 90, angle), 0.1f);
                transform.DOMove(paths[i], 1);
                i++;
                yield return null;
            }
            while (gameObject.activeSelf)
            {
                transform.Translate(speed * direction * Time.deltaTime);
                yield return null;
            }
        }

    }
}
