using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject enemy;
    float spawn;
    // Start is called before the first frame update
    void Start()
    {
        spawn = Random.Range(2.0f, 15.0f);
    }

    // Update is called once per frame
    void Update()
    {
        spawn -= Time.deltaTime;
        if(spawn < 0)
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
            spawn = Random.Range(10f, 25.0f);
        }

    }
}
