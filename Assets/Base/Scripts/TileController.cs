using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileController : MonoBehaviour
{
    public Renderer cubeMeshRenderer;
    public CellController cellParent;

    public TextMeshPro numberTile;

    public void SetNumber()
    {
        int randomInt = Random.Range(1, 9);
        numberTile.text = randomInt.ToString();
    }
}