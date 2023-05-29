using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Level
{
    public List<string> rows = new List<string>();

    public int Height 
    {
        get 
        {
            return rows.Count; 
        } 
    }

    public int Width
    {
        get 
        {
            int maxLength = 0;
            foreach(var r in rows)
            {
                if (r.Length > maxLength) maxLength = r.Length;
            }
            return maxLength;
        }
    }
}

public class Levels : MonoBehaviour
{
    public string filename;
    public Dictionary<int, Level> levels = new Dictionary<int, Level>();
    public TMPro.TMP_Text test;

    private void Awake()
    {
        TextAsset textAsset = (TextAsset)Resources.Load(filename);
        string completeText = textAsset.text;

        string[] lines;
        lines = completeText.Split(new string[] { "\n" }, System.StringSplitOptions.None);

        int levelIndex = 1;
        levels.Add(levelIndex, new Level());

        for (long i = 0; i < lines.LongLength; i++)
        {
            string line = lines[i];
            if(line.StartsWith(";"))
            {
                levelIndex++;
                levels.Add(levelIndex, new Level());
                continue;
            }

            levels[levelIndex].rows.Add(line);
        }
    }
}
