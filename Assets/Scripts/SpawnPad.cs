using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPad : MonoBehaviour
{
    [SerializeField] GameObject alienObject;

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

        seconds = UnityEngine.Random.Range(3, 7);
        print("Spawner: Start waiting for: " + seconds);
        yield return new WaitForSeconds(seconds);
        SpawnGreenDude();

        StartCoroutine(countDown());
    }
}
