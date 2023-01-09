using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
    [SerializeField] private List<Transform> pellets;
    [SerializeField] private int noOfPellets;
    [SerializeField] private float radius;
    [SerializeField] private GameObject pellet;

    AudioSource audioSource;

    private void Start()
    {
        audioSource= GetComponent<AudioSource>();
    }

    public void Shoot() {
        pellets.Clear();
        audioSource.Play();
        for (int i = 0; i < noOfPellets; i++) {
            GameObject p = Instantiate(pellet, transform.position, transform.rotation);
            p.transform.localScale = transform.localScale;
            pellets.Add(p.transform);
        }
        SetPelletRotations(pellets);
    }
    
    void SetPelletRotations(List<Transform> p) {
        int center = Mathf.CeilToInt((float) p.Count / 2);
        
        for (int i = 0; i < center; i++) {
            p[i].Rotate(Vector3.forward * (radius * (center - i)));
        }

        for (int i = center; i < p.Count; i++) {
            p[i].Rotate(Vector3.forward * (radius * -1 * (i - center)));
        }
    }
    
}
