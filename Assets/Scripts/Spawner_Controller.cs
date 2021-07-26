using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_Controller : MonoBehaviour
{

    public GameObject[] Enemies;
    public GameObject Path;
    public int waveSize;

    private int initialWaveSize;

    public float spawnRate = 1.0f;
    public bool inProgress = false;

    public int wave = 0;
    private IEnumerator coroutine;
    

    public IEnumerator spawn()
    {
        
        while(waveSize > 0)
        {
            GameObject enemyGO = (GameObject)Instantiate(Enemies[0], Path.transform.position, Path.transform.rotation);
            enemyGO.transform.Rotate(Vector3.up, 180);
            waveSize -= 1;
            yield return new WaitForSeconds(spawnRate);
        }
        //yield break;
    }

    public void clickSpawn()
    {
        if (inProgress == false)
        {
            waveSize = initialWaveSize;
            initialWaveSize += 3;
            inProgress = true;
            wave += 1;
            StartCoroutine(coroutine);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        coroutine = spawn();
        initialWaveSize = waveSize;
    }

    // Update is called once per frame
    void Update()
    {
        if(waveSize == 0)
        {
            inProgress = false;
            StopAllCoroutines();
            
        }
        
    }
}
