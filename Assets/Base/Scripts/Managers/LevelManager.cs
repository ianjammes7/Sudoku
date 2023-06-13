using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu]
public class LevelManager : ScriptableObject
{
    public List<Level> listLevels = new List<Level>();

    public string levelsPath;
    
    public Level GetLevelById(int id)
    {
        return listLevels[id];
    }
}
