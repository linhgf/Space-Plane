using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField]
    private float speed;


    private float min_x;
    private float max_x;

    void Start()
    {
        max_x = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).z + 30;
        min_x = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)).z - 30;
    }

    // Update is called once per frame
    void Update()
    {
        RockMove();
        CheckLife();
    }

    void RockMove(){
        transform.Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime, Space.World);
        transform.Rotate(new Vector3(0, 1, 0), 0.5f);
    }

    private void CheckLife()
    {
        if (transform.position.z > max_x || transform.position.z < min_x)
        {
            Destroy(gameObject);
        }
    }
}
