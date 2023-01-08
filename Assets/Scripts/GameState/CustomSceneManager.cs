using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviour
{
    [SerializeField] private int initSceneIndex = 0;
    [SerializeField] private int titleSceneIndex = 1;
    [SerializeField] private int mainSceneIndex = 2;
    [SerializeField] private int endSceneIndex = 3;

    public bool IsGameplayScene => SceneManager.GetActiveScene().buildIndex == mainSceneIndex;

    [SerializeField] InputHandler _input;

    public static CustomSceneManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == initSceneIndex)
        {
            SceneManager.LoadScene(titleSceneIndex);
        }
    }

    private void Update()
    {
        if (!_input.FireInput.action.WasPressedThisFrame())
            return;

        if (SceneManager.GetActiveScene().buildIndex == titleSceneIndex)
        {
            SceneManager.LoadScene(mainSceneIndex);
        }
        if (SceneManager.GetActiveScene().buildIndex == endSceneIndex)
        {
            //restart the level
            SceneManager.LoadScene(mainSceneIndex);
        }
    }

    //called when game ends
    public void LoadEndScene()
    {
        SceneManager.LoadScene(endSceneIndex);
    }
}
