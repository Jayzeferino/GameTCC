using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowNumberInFloor : MonoBehaviour
{
  [SerializeField] TextMeshPro numberText;
  public int number = 3;
  private void Update()
  {
    numberText.text = number.ToString();
  }

  public void SetNumber(int number)
  {
    this.number = number;
  }
}
