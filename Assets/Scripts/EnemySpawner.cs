using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField]
    private EnemyFactorySO enemyFactory;

    [SerializeField]
    private float interval;

    private float min_y;
    private float max_y;
    // Start is called before the first frame update
    void Start()
    {
        Camera main_camera = Camera.main;
        Vector3 bottom_left = main_camera.ViewportToWorldPoint(new Vector3(0f, 0f));
        Vector3 top_right = main_camera.ViewportToWorldPoint(new Vector3(1f, 1f));
        min_y = bottom_left.y;
        max_y = top_right.y;
        StartCoroutine(SpawnCoroutine());
    }
    

    void SpawnRock()
    {
        EnemyController rock = enemyFactory.Create();

        float rand_y = Random.Range(min_y, max_y);
        rock.transform.position = transform.position + new Vector3(0, rand_y, 0);
    }

    IEnumerator SpawnCoroutine(){
        while(true){
            yield return new WaitForSecondsRealtime(interval);
            SpawnRock();
        }
        
    }
}
