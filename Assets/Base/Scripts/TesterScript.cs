using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesterScript : MonoBehaviour
{

    private MainSceneManager _mainSceneManager;
    protected MainSceneManager mainSceneManager
    {
        get
        {
            if (_mainSceneManager != null)
            {
                return _mainSceneManager;
            }
            else
            {
                _mainSceneManager = GameManager.Instance.currentSceneManager as MainSceneManager;
            }
            return _mainSceneManager;
        }

        set
        {
            _mainSceneManager = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G)) 
        {
            foreach (TileController tile in mainSceneManager._GridController.listTiles)
            {
                tile.transform.localScale = Vector3.one * Random.Range(0.5f, 1f);
            }
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            mainSceneManager._GridController.DestroyTileAt( mainSceneManager._GridController.listTiles[Random.Range(0, mainSceneManager._GridController.listTiles.Count)].cellParent);
            mainSceneManager._GridController.UpdateCollum();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            mainSceneManager._GridController.ShuffleGrid();
        }
    }
}
