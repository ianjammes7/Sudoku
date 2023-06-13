using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CellController : MonoBehaviour
{
    [HideInInspector] public CellController parent;
    [HideInInspector] public Vector2Int pos;
    
    public List<CellController> connectedCells = new List<CellController>();
    public List<CellController> diagonalConnected = new List<CellController>();

    public bool occupied;
    
    public TileObject linkedTile;
}