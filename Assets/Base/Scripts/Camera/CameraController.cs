using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK.Setup;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float persHorizontalFoV = 90.0f;

    public float orthoSceneWidthIphone = 10f;
    public float orthoSceneWidthIpad = 10f;
    private float defaultOrthoSceneWidthIphone = 10f;
    private float defaultOrthoSceneWidthIpad = 10f;

    Camera _camera;

    bool isOrtho;

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

    void Start()
    {
        _camera = GetComponent<Camera>();
        isOrtho = _camera.orthographic;
    }

    public void Init()
    {
        defaultOrthoSceneWidthIphone = orthoSceneWidthIphone;
        defaultOrthoSceneWidthIpad = orthoSceneWidthIpad;
        ChangeCameraSize();
    }

    void Update()
    {
        if(isOrtho == false)
        {
            float halfWidth = Mathf.Tan(0.5f * persHorizontalFoV * Mathf.Deg2Rad);

            float halfHeight = halfWidth * Screen.height / Screen.width;

            float verticalFoV = 2.0f * Mathf.Atan(halfHeight) * Mathf.Rad2Deg;

            _camera.fieldOfView = verticalFoV;
        }
        else
        {
            if(Utility.GetDeviceType() == ENUM_Device_Type.Phone)
            {
                float unitsPerPixel = orthoSceneWidthIphone / Screen.width;

                float desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;

                _camera.orthographicSize = desiredHalfHeight;
            }
            else
            {
                float unitsPerPixel = orthoSceneWidthIpad / Screen.width;

                float desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;

                _camera.orthographicSize = desiredHalfHeight;
            }
        }

    }
    
    public void ChangeCameraSize()
    {
        orthoSceneWidthIphone = defaultOrthoSceneWidthIphone / 6f * mainSceneManager._GridController.gridSize.x;
        orthoSceneWidthIpad = defaultOrthoSceneWidthIpad / 6f * mainSceneManager._GridController.gridSize.y;

        float aspectRatio = Mathf.Round((float)Mathf.Max(Screen.width, Screen.height) / Mathf.Min(Screen.width, Screen.height) * 10f) / 10f;
        if (aspectRatio >= 1.7f && aspectRatio <= 1.8f)
        {
            orthoSceneWidthIphone += 0.5f;
            orthoSceneWidthIpad += 0.5f;
        }

        if (GameManager.Instance._currentLevel == 0 || GameManager.Instance._currentLevel == 1)
        {
            /*
            Camera.main.GetComponent<CameraController>().orthoSceneWidthIphone = 4f;
            Camera.main.GetComponent<CameraController>().orthoSceneWidthIpad = 5f;
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, -2.1f, Camera.main.transform.position.z);
        */
        }
    }
}
