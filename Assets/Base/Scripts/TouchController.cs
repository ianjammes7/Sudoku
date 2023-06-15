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
        if (mainSceneManager.isVictory || mainSceneManager.gameHasStarted == false || mainSceneManager.pausedGame) return;

        Ray raycast = Camera.main.ScreenPointToRay(finger.ScreenPosition);
        RaycastHit raycastHit;

        if (Physics.Raycast(raycast, out raycastHit))
        {
            if (raycastHit.collider.gameObject.layer == 6) //clicking on a tile
            {
                if (touchedTile != null) //Storing last touched tile
                    lastTouchedTile = touchedTile;

                touchedTile = raycastHit.collider.GetComponent<TileController>();

                mainSceneManager._gridIndicator.SelectAllLineColumn(touchedTile.cellParent);
                mainSceneManager._gridIndicator.SelectAllSquare(touchedTile.cellParent);
                mainSceneManager._gridIndicator.HighlightSameNumberOnGrid(touchedTile);
            }
        }
    }

    void OnFingerUp(LeanFinger finger)
    {
        if (mainSceneManager.isVictory || mainSceneManager.gameHasStarted == false) return;
        //touchedTile = null;
    }
}
