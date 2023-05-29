using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            LoadLevel();
        }
    }

    public void LoadLevel()
    {
        StartCoroutine(Load("Game"));
    }

    IEnumerator Load(string levelName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);   
        SceneManager.LoadScene(levelName);
    }
}
