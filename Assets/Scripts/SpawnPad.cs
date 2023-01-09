using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPad : MonoBehaviour
{
    [SerializeField] GameObject alienObject;

    [SerializeField] int spawnCounter;
    public int spawnCounterTick = 5;

    public float secondsMin = 3;
    public float secondsMax = 7;

    public float secondsMinLimit = 1;
    public float secondsMaxLimit = 2;



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(countDown());
    }

    // Update is called once per frame
    void Update()
    {
        //SpawnGreenDude();
    }

    void SpawnGreenDude()
    {
        Debug.Log("spawnaegieaigeaiguieegakeg");
        Instantiate(alienObject, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
    }

    IEnumerator countDown()
    {
        float seconds;

        seconds = UnityEngine.Random.Range(secondsMin, secondsMax);
        print("Spawner: Start waiting for: " + seconds);
        yield return new WaitForSeconds(seconds);
        spawnCounter++;
        SpawnGreenDude();

        if (spawnCounter >= spawnCounterTick)
        {
            if (secondsMin > secondsMinLimit)
            {
                secondsMin -= 1;
            }

            if (secondsMax > secondsMaxLimit)
            {
                secondsMax -= 1;
            }

            spawnCounter = 0;
        }

        StartCoroutine(countDown());
    }
}
