using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    public bool onlyAppearOnClick;

    private RectTransform rectTransform;
    private RectTransform canvasRect;

    // Start is called before the first frame update
    void Start()
    {
        this.rectTransform = GetComponent<RectTransform>();


    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.position = Input.mousePosition;

        if(Input.GetMouseButtonDown(0))
        {
            if(onlyAppearOnClick)
            {
                this.transform.localScale = Vector3.one * 0.8f;
                this.transform.DOScale(0.8f, 0.1f).OnComplete(() => this.transform.DOScale(1f, 0.05f).OnComplete(()=> this.transform.localScale = Vector3.zero));
            }
            else
            {
                this.transform.DOScale(0.8f, 0.1f).OnComplete(() => this.transform.DOScale(1f, 0.05f));
            }
                
        }
    }
}
