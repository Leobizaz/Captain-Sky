using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject[] enemiesToSpawn;
    bool once;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !once)
        {
            once = true;
            StartCoroutine(Spawn());

        }
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds wait = new WaitForSeconds(Random.Range(0.3f, 1f));
        foreach (GameObject enemy in enemiesToSpawn)
        {
            enemy.SetActive(true);
            yield return wait;
        }
    }

}
