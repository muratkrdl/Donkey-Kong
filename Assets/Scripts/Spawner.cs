using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject barrelPrefab;
    [SerializeField] Transform spawnPoint;

    [SerializeField] float spawnTimeMax = 2f;

    void Start() 
    {
        StartCoroutine(SpawnBarrel());    
    }

    IEnumerator SpawnBarrel()
    {
        while(true)
        {
            float newTime = Random.Range(2, spawnTimeMax);

            var barrel = Instantiate(barrelPrefab);
            barrel.transform.position = spawnPoint.position;
            
            yield return new WaitForSeconds(newTime);
        }
    }

}
