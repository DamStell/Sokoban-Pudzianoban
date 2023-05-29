using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitGame : MonoBehaviour
{
    private void Start()
    {

    }

    private void Update()
    {

    }

    public void StartGame(int level)
    {
        PlayerPrefs.SetInt("lvltoload", level);
        SceneManager.LoadScene(2);
    }
}