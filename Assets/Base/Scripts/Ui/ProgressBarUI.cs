using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
   [Header ("UI references :")]
   [SerializeField] private Image uiFillImage ;
   [SerializeField] private TextMeshProUGUI uiStartText ;
   [SerializeField] private TextMeshProUGUI uiEndText ;

   [Header ("Player & Endline references :")]
   //[SerializeField] private Transform playerTransform;
   //[SerializeField] private Transform endLineTransform;
   
   private Vector3 endLinePosition ;
   private float fullDistance ;
   
   private void Start () {
      //endLinePosition = endLineTransform.position ; //Change this
      //fullDistance = GetDistance();
      uiFillImage.fillAmount = 0;
      SetLevelTexts(GameManager.Instance._currentLevel);
   }
   
   public void SetLevelTexts (int level) {
      uiStartText.text = level.ToString();
      uiEndText.text = (level + 1).ToString();
   }
   
   /*private float GetDistance () {
      return (endLinePosition - playerTransform.position).sqrMagnitude ;
   }*/
   
   private void UpdateProgressFill (float value) {
      uiFillImage.fillAmount = value ;
   }

   private void Update () {
      // check if the player doesn't pass the End Line
      /*if (playerTransform.position.z <= endLinePosition.z) {
         float newDistance = GetDistance();
         float progressValue = Mathf.InverseLerp (fullDistance, 0f, newDistance) ;

         UpdateProgressFill(progressValue);
      }*/
   }
}
