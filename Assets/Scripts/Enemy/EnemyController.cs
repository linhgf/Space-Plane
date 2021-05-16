using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private int score = 15;

    [SerializeField]
    private GameObject explosion_vfx;
    [SerializeField]
    private int health = 6;
    [SerializeField]
    private float speed = 10f;

    private Rigidbody m_rb;

    [SerializeField]
    private Projectile m_projectile;
    [SerializeField]
    private Vector3 rotate_angle = new Vector3(0, 180, 30);
    [SerializeField]
    private Transform[] muzzle;
    [SerializeField]
    private float move_interval = 5f;
    [SerializeField]
    private float fire_interval = 3f;
    private float move_timer = 5f;
    private float fire_timer = 3f;

    private bool is_arrive = true;

    private Vector3 position;//目的地

    private PlayerController player;


    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        m_rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        if(is_arrive){
            move_timer += Time.deltaTime;
        }
        if (move_timer - move_interval > 0)
        {
            is_arrive = false;
            move_timer = 0;
            position = Viewport.Instance.RandomRightHalfPosition(10, 10);
        }
        Move(position);
        Fire();
    }

    private void Move(Vector3 position){
        float rotate_direction = (position - transform.position).normalized.y;
        float distance = Vector3.Distance(position, transform.position);

        transform.DOMove(position, distance / (speed * Time.deltaTime));
        transform.DORotate(new Vector3(rotate_angle.x, rotate_angle.y, rotate_angle.z * rotate_direction), 1);

        if(Vector3.Distance(position, transform.position) < 1f){
            is_arrive = true;
        }
    }

    private void Fire(){
        fire_timer += Time.deltaTime;
        if(fire_timer - fire_interval > 0){
            fire_timer = 0;
            for (int i = 0; i < muzzle.Length; i++){
                Instantiate(m_projectile, muzzle[i].position, m_projectile.transform.rotation);
            }
        }
    }

    public void Hurt(int damage){
        health -= damage;
        IsAlive();
    }

    public void IsAlive(){
        if(health <= 0){
            Instantiate(explosion_vfx, transform.position, Quaternion.identity);
            transform.DOPause();
            UIManager.Instance.UpdateScore(score);
            CameraManager.Instance.ShakeCamera();
            player.AddEnergy(score);
            Destroy(gameObject);
        }
    }
}
