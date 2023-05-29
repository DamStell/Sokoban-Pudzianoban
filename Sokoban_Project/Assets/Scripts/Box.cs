using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private bool canMove = true;
    public bool correct;

    public bool Move(Vector2 direction)
    {
        if (canMove == false) 
            return false;

        if (Blocked(transform.position, direction))
        {
            return false;
        }
        else
        {
            Vector2 startPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 endPos = new Vector2(transform.position.x, transform.position.y) + direction;

            float timeStarted = Time.time;
            switch (direction.x)
            {
                case -1:
                    transform.Rotate(Vector3.forward*-45);
                    break;
                case 1:
                    transform.Rotate(Vector3.forward*45);
                    break;

            }
            switch (direction.y)
            {
                case -1:
                    transform.Rotate(Vector3.back*-45);
                    break;
                case 1:
                    transform.Rotate(Vector3.back*45);
                    break;

            }
            StartCoroutine(LerpPosition(startPos, endPos, 0.2f));
            return true;
        }
    }

    IEnumerator LerpPosition(Vector2 startPosition, Vector2 targetPosition, float duration)
    {
        canMove = false;
        float time = 0;

        SoundManager.PlaySound("ballmove");
        while (time < duration)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        canMove = true;

        TestFinish();
    }

    public bool Blocked(Vector3 position, Vector2 direction)
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
                return true;
        }

        return false;
    }

    void TestFinish()
    {
        GameObject[] slots = GameObject.FindGameObjectsWithTag("Slot");
        foreach(var slot in slots)
        {
            if(transform.position.x == slot.transform.position.x && transform.position.y == slot.transform.position.y)
            {
                correct = true;
                return;
            }
        }
        correct = false;
    }

    bool isLevelComplete()
    {
        Box[] boxes = FindObjectsOfType<Box>();
        foreach (var box in boxes)
        {
            if (!box.correct)
                return false;
        }
        return true;
    }
}
