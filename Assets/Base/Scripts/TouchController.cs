using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;

public class TouchController : MonoBehaviour
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

    public TileController touchedTile;
    public TileController lastTouchedTile;


    protected virtual void Start()
    {
        OnDisable();
        LeanTouch.OnFingerDown += OnFingerDown;
        LeanTouch.OnFingerUp += OnFingerUp;
    }

    void OnDisable()
    {
        LeanTouch.OnFingerDown -= OnFingerDown;
        LeanTouch.OnFingerUp -= OnFingerUp;
    }

    void OnFingerDown(LeanFinger finger)
    {
        if (mainSceneManager.isVictory || mainSceneManager.gameHasStarted == false) return;

        Ray raycast = Camera.main.ScreenPointToRay(finger.ScreenPosition);
        RaycastHit raycastHit;

        if (Physics.Raycast(raycast, out raycastHit))
        {
            if (raycastHit.collider.gameObject.layer == 6) //tile
            {
                if (touchedTile != null) //Storing last touched tile
                    lastTouchedTile = touchedTile;

                touchedTile = raycastHit.collider.GetComponent<TileController>();

                //Reset color for other tiles and selected color for the touched one
                if(lastTouchedTile != touchedTile)
                {
                    for (int i = 0; i < mainSceneManager._GridController.listTiles.Count; i++)
                    {
                        if (mainSceneManager._GridController.listTiles[i].spriteTile.color == mainSceneManager._GridController.listTiles[i].selectedColorTile)
                        {
                            mainSceneManager._GridController.listTiles[i].spriteTile.color = Color.white;
                        }
                    }
                }
                touchedTile.spriteTile.color = touchedTile.selectedColorTile;


            }
        }
    }

    void OnFingerUp(LeanFinger finger)
    {
        if (mainSceneManager.isVictory || mainSceneManager.gameHasStarted == false) return;
        //touchedTile = null;
    }
}
