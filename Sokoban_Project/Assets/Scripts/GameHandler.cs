using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public LevelBuilder levelBuilder;
    public GameObject finishMenu;
    public Animator transition;
    public TMPro.TMP_Text movesText;
    private Player player;
    public Animator animator;
  


    bool animationTrigger = true;
    bool firstLoad = true;
    private bool canMove;
    private bool inputEnabled = true;
    private bool isKeyDown;

    private void Start()
    {
        int numLevel = PlayerPrefs.GetInt("lvltoload");
        LoadLevel(numLevel);
    }

    void Update()
    {
        if (inputEnabled)
        {
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            moveInput.Normalize();

            if (moveInput.x < 0 || moveInput.y < 0 || moveInput.x > 0 || moveInput.y > 0)
            {
                isKeyDown = true;
            }

            if (isKeyDown)
            {
                if (moveInput.sqrMagnitude > 0.5)
                {
                    if (canMove)
                    {
                        canMove = false;
                        player.Move(moveInput);
                    }
                    else
                    {
                        canMove = true;
                    }
                }
                isKeyDown = false;
            }
            else
            {   
                isKeyDown = false;
            }
        }


        if (isLevelComplete() && inputEnabled)
        {
            inputEnabled = false;
            finishMenu.gameObject.SetActive(true);
            SoundManager.PlaySound("finish");
        }

        if(animationTrigger)
        {
            transition.SetTrigger("Start");
            animationTrigger = false;
        }
    }

    public void LoadLevel(int level)
    {
        animationTrigger = false;
        StartCoroutine(ResetScene(level));
    }

    public void LoadNextLevel()
    {
        animationTrigger = true;
        StartCoroutine(ResetScene(0));
    }

    public void LoadPreviousLevel()
    {
        animationTrigger = true;
        StartCoroutine(ResetScene(-1));
    }

    public void RestartLevel()
    {
        SoundManager.PlaySound("restart");
        animationTrigger = true;
        StartCoroutine(ResetScene(-2));
    }

    IEnumerator ResetScene(int level)
    {
        if(!firstLoad)
        {
            yield return new WaitForSeconds(0.75f);
        }
        firstLoad = false;

        finishMenu.gameObject.SetActive(false);

        if (SceneManager.sceneCount > 1)
        {
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync("Level");
            while(!asyncUnload.isDone)
            {
                yield return null;
            }
            Resources.UnloadUnusedAssets();
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Level", LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Level"));

        if (level == 0)
        {
            levelBuilder.NextLevel();
        }
        else if(level == -1)
        {
            levelBuilder.PreviousLevel();
        }
        else if (level == -2)
        {
            levelBuilder.RestartLevel();
        }
        else
        {
            levelBuilder.LoadLevel(level);
        }


        transition.SetBool("Stop", true);
        inputEnabled = true;
        player = FindObjectOfType<Player>();
      
        player.movesText = this.movesText;
      

    }

    bool isLevelComplete()
    {
        Box[] boxes = FindObjectsOfType<Box>();

        if (boxes.Length == 0)
        {
            return false;
        }
        else
        {
            foreach (var box in boxes)
            {
                if (!box.correct)
                    return false;
            }
            

            SetdeactiveObjects("Box");
            SetdeactiveObjects("Player");
            SetdeactiveObjects("Wall");
            SetdeactiveObjects("Slot");


            return true;
        }
    }

    public void BackToMenu()
    {
        transition.SetTrigger("Start");
        StartCoroutine(BackToMenuAsync());
    }

    IEnumerator BackToMenuAsync()
    {
        yield return new WaitForSeconds(0.85f);
        SceneManager.LoadScene(0);
    }

 

    void DestroyAllObjects(string Tagtodestroy)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(Tagtodestroy);

        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
    }

    void SetdeactiveObjects(string Tagtodeactive)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(Tagtodeactive);

        for (var i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(false);
        }
    }

}
