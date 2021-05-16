using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private int max_health = 50;
    private int current_health;
    [SerializeField]
    private float accelerationTime = 0.2f;
    [SerializeField]
    private float deaccelertaionTime = 0.2f;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float move_rotate_angle;
    private Rigidbody m_rb;

    private Coroutine move_coroutine;

    [Header("-----Projectile-----")]
    [SerializeField]
    private PlayerProjectile projectile;
    [SerializeField]
    private PlayerProjectile track_projectile;

    [SerializeField]
    private float shoot_interval;
    [SerializeField]
    private AudioSource audio_shoot;
    private float timer;
    [SerializeField]
    private Transform muzzle;

    [SerializeField]
    private GameObject vfx_explosion;
    [SerializeField]
    private int max_energy = 100;
    private int current_energy = 0;
    // Start is called before the first frame update
    void Start()
    {
        current_health = max_health;
        m_rb = GetComponent<Rigidbody>();
        StartCoroutine(IncreseEnergyCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
        InputMagnitude();

        UIManager.Instance.UpdateEnergy((float)current_energy / (float)max_energy);
    }

    void Fire(){
        timer += Time.deltaTime;
        if(Input.GetKey(KeyCode.J) && (timer - shoot_interval > 0)){
            audio_shoot.Play();
            timer = 0;
            var bullet = Instantiate(projectile, muzzle.position, projectile.transform.rotation);
        }
        if(Input.GetKeyDown(KeyCode.K) && (current_energy - 5) >= 0){
            current_energy -= 5;
            audio_shoot.Play();
            var bullet = Instantiate(track_projectile, muzzle.position, track_projectile.transform.rotation);
        }
    }

    void InputMagnitude(){
        float input_x = Input.GetAxisRaw("Horizontal");
        float input_y = Input.GetAxisRaw("Vertical");

        Vector3 velocity = new Vector3(0, input_y, -input_x);
        float velocity_magnitude = velocity.magnitude;
        transform.DORotate(new Vector3(0, 0, 30 * input_y), 1);

        if(velocity_magnitude > 0.1f){
            if(move_coroutine != null)
                StopCoroutine(move_coroutine);
            
            move_coroutine = StartCoroutine(StartMoveCoroutine(velocity.normalized * speed));
        }
        else{
            if (move_coroutine != null)
                StopCoroutine(move_coroutine);
            move_coroutine = StartCoroutine(StopMoveCoroutine(Vector2.zero));
        }
    }

    IEnumerator StartMoveCoroutine(Vector3 velocity){
        float t = 0;
        while (t < accelerationTime){
            t += Time.fixedTime / accelerationTime;
            m_rb.velocity = Vector3.Lerp(m_rb.velocity, velocity, t / accelerationTime);
            yield return null;
        }
    }

    IEnumerator StopMoveCoroutine(Vector3 velocity){
        float t = 0;
        while (t < accelerationTime)
        {
            t += Time.fixedTime / deaccelertaionTime;
            m_rb.velocity = Vector3.Lerp(m_rb.velocity, velocity, t / deaccelertaionTime);
            yield return null;
        }
    }

    IEnumerator IncreseEnergyCoroutine(){
        while(true){
            if(current_energy < max_energy)
                current_energy += 1;
            yield return new WaitForSecondsRealtime(1);
        }
    }

    public void AddEnergy(int energy){
        current_energy += energy;
        if(current_energy > max_energy)
            current_energy = max_energy;
    }
    
    public void Hurt(int damage){
        current_health -= damage;
        UIManager.Instance.UpdateHealth((float)current_health / (float)max_health);
        if(current_health <= 0){
            Instantiate(vfx_explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }


}
