using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    

    Animator animator;
    private bool canMove = true;
    public TMPro.TMP_Text movesText;
    int moves = 0;

    int rightRunHash = Animator.StringToHash("Run_Right");
    int leftRunHash = Animator.StringToHash("Left_Run");
    int frontRunHash = Animator.StringToHash("Run_Front");
    int backRunHash = Animator.StringToHash("Run_Back");
    int sateInfoRun =0;

    public bool Move(Vector2 direction)
    {
        if (canMove == false)
            return false;

        if (Mathf.Abs(direction.x) < 0.5)
            direction.x = 0;
        else
            direction.y = 0;

        direction.Normalize();

        if (Blocked(transform.position, direction))
        {
            return false;
        }
        else
        {
            Vector2 startPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 endPos = new Vector2(transform.position.x, transform.position.y) + direction;

            float timeStarted = Time.time;
            StartCoroutine(LerpPosition(startPos, endPos, 0.2f));
            return true;
        }
    }

    IEnumerator LerpPosition(Vector2 startPosition, Vector2 targetPosition, float duration)
    {
        canMove = false;
        float time = 0;

        SoundManager.PlaySound("playermove");
        while (time < duration)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        canMove = true;
    }

    bool Blocked(Vector2 position, Vector2 direction)
    {
        Vector2 newPos = new Vector2(position.x, position.y) + direction;

        if ((newPos.x < 0 || newPos.x > 15) || (newPos.y > 11 || newPos.y < 0))
            return true;

        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (var wall in walls)
        {
            if (wall.transform.position.x == newPos.x && wall.transform.position.y == newPos.y)
                return true;
        }

        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
        foreach (var box in boxes)
        {
            if (box.transform.position.x == newPos.x && box.transform.position.y == newPos.y)
            {
               


                Box bx = box.GetComponent<Box>();
                if (bx && bx.Move(direction))
                {
                    if (Input.GetKey(KeyCode.W) || Input.GetKeyDown(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        animator.SetBool("push_back", true);
                    }

                    if (Input.GetKey(KeyCode.S) || Input.GetKeyDown(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        animator.SetBool("push_front", true);
                    }

                    if (Input.GetKey(KeyCode.A) || Input.GetKeyDown(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        animator.SetBool("push_left", true);
                    }
                    if (Input.GetKey(KeyCode.D) || Input.GetKeyDown(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        animator.SetBool("push_right", true);
                    }

                    moves++;
                    movesText.text = moves.ToString();
                    return false;
                }
                else
                {
                    return true;


                }
            }
        }
        
        return false;
    }

    private void OnDestroy()
    {
        movesText.text = "0";
    
    }


    void Start()
    {
        animator = GetComponent<Animator>();

    }


    void Update()
    {

      

       

        if (Input.GetKey(KeyCode.W)|| Input.GetKeyDown(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            sateInfoRun = backRunHash;

            animator.SetBool("push_front", false);
            animator.SetBool("push_left", false);
            animator.SetBool("push_right", false);

            animator.SetBool("idle_right", false);
            animator.SetBool("idle_left", false);
            animator.SetBool("idle_right", false);
            animator.SetBool("idle_back", false);

            animator.SetBool("run_front", false);
            animator.SetBool("run_back", true);
            animator.SetBool("run_left", false);
            animator.SetBool("run_right", false);


        }
 


        if (Input.GetKey(KeyCode.S)|| Input.GetKeyDown(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            sateInfoRun = frontRunHash;

            animator.SetBool("push_back", false);
            animator.SetBool("push_left", false);
            animator.SetBool("push_right", false);

            animator.SetBool("idle_right", false);
            animator.SetBool("idle_left", false);
            animator.SetBool("idle_right", false);
            animator.SetBool("idle_back", false);
       
            animator.SetBool("run_front", true);
            animator.SetBool("run_back", false);
            animator.SetBool("run_left", false);
            animator.SetBool("run_right", false);


        }


        if (Input.GetKey(KeyCode.A)  || Input.GetKeyDown(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            sateInfoRun = leftRunHash;

            animator.SetBool("push_back", false);
            animator.SetBool("push_front", false);
            animator.SetBool("push_right", false);

            animator.SetBool("idle_right", false);
            animator.SetBool("idle_left", false);
            animator.SetBool("idle_right", false);
            animator.SetBool("idle_back", false);
 
            animator.SetBool("run_front", false);
            animator.SetBool("run_back", false);
            animator.SetBool("run_left", true);
            animator.SetBool("run_right", false);


        }



        if (Input.GetKey(KeyCode.D) || Input.GetKeyDown(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            sateInfoRun = rightRunHash;

            animator.SetBool("push_back", false);
            animator.SetBool("push_front", false);
            animator.SetBool("push_left", false);
        
            animator.SetBool("idle_right", false);
            animator.SetBool("idle_front", false);
            animator.SetBool("idle_left", false);
            animator.SetBool("idle_back", false);
        
            animator.SetBool("run_front", false);
            animator.SetBool("run_back", false);
            animator.SetBool("run_left", false);
            animator.SetBool("run_right", true);


        }
    



        if (sateInfoRun == rightRunHash && !(Input.GetKey(KeyCode.D) || Input.GetKeyDown(KeyCode.D)) && !(Input.GetKey(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            animator.SetBool("idle_right", true);
            animator.SetBool("push_back", false);
            animator.SetBool("push_front", false);
            animator.SetBool("push_left", false);
            animator.SetBool("push_right", false);

        }
       
        if (sateInfoRun == backRunHash && !(Input.GetKey(KeyCode.W) || Input.GetKeyDown(KeyCode.W)) && !(Input.GetKey(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            animator.SetBool("idle_back", true);
            animator.SetBool("push_back", false);
            animator.SetBool("push_front", false);
            animator.SetBool("push_left", false);
            animator.SetBool("push_right", false);

        }
      if (sateInfoRun == leftRunHash && !(Input.GetKey(KeyCode.A) || Input.GetKeyDown(KeyCode.A)) && !(Input.GetKey(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            animator.SetBool("idle_left", true);
            animator.SetBool("run_left", false);
            animator.SetBool("push_back", false);
            animator.SetBool("push_front", false);
            animator.SetBool("push_left", false);
            animator.SetBool("push_right", false);

        }
        if (sateInfoRun == frontRunHash && !(Input.GetKey(KeyCode.S) || Input.GetKeyDown(KeyCode.S)) && !(Input.GetKey(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            animator.SetBool("idle_front", true);
            animator.SetBool("run_front", false);
            animator.SetBool("push_back", false);
            animator.SetBool("push_front", false);
            animator.SetBool("push_left", false);
            animator.SetBool("push_right", false);

        }



    }

}
