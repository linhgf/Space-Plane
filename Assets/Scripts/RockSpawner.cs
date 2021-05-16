using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{

    [SerializeField]
    private RockFactorySO rockFactory;

    [SerializeField]
    private float interval;
    private float timer;

    private float min_y;
    private float max_y;
    // Start is called before the first frame update
    void Start()
    {
        timer = interval;
        Camera main_camera = Camera.main;
        Vector3 bottom_left = main_camera.ViewportToWorldPoint(new Vector3(0f, 0f));
        Vector3 top_right = main_camera.ViewportToWorldPoint(new Vector3(1f, 1f));
        min_y = bottom_left.y;
        max_y = top_right.y;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer - interval > 0){
            timer = 0;
            SpawnRock();
        }
    }

    void SpawnRock(){
        Rock rock = rockFactory.Create();

        float rand_y = Random.Range(min_y, max_y);
        float rand_size = Random.Range(1f, 2f);
        rock.transform.position = transform.position + new Vector3(0, rand_y, 0);
        rock.transform.localScale = rock.transform.localScale * rand_size;
    }
}
