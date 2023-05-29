using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelElement
{
    public string character;
    public GameObject prefab;
}

public class LevelBuilder : MonoBehaviour
{
    public int currentLevel = 1;
    public List<LevelElement> levelElements;
    private Level level;
    public TMPro.TMP_Text levelText;

    GameObject GetPrefab(char c)
    {
        LevelElement levelElement = levelElements.Find(le => le.character == c.ToString());
        if (levelElement != null)
            return levelElement.prefab;
        else
            return null;
    }

    public void NextLevel()
    {
        currentLevel++;
        if(currentLevel >= GetComponent<Levels>().levels.Count)
        {
            currentLevel--;
        }
        Build();
    }

    public void PreviousLevel()
    {
        currentLevel--;
        if (currentLevel < 1)
        {
            currentLevel++;
        }
        Build();
    }

    public void RestartLevel()
    {
        Build();
    }

    public void LoadLevel(int level)
    {
        currentLevel = level;
        Build();
    }

    public void Build()
    {
        level = GetComponent<Levels>().levels[currentLevel];
        levelText.text = currentLevel.ToString();

        int x = 0;
        int y = 0;

        foreach(var row in level.rows)
        {
            foreach(var ch in row)
            {
                GameObject prefab = GetPrefab(ch);
                if(prefab)
                {
                    Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
                }
                x++;
            }
            y++;
            x = 0;
        }
    }
}
