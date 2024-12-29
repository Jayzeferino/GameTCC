using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GenerateChallenge : MonoBehaviour
{
    [SerializeField] ChallengerController challengerController;
    [SerializeField] GameObject floorRight;
    [SerializeField] GameObject floorLeft;

    [SerializeField] TextMeshPro challengeText;

    private void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.CompareTag("Player"))
        {

            (string expression, double result, double wrong) = challengerController.GerarDesafio();
            challengeText.text = expression;
            RandomFloor(Random.Range(0, 2), result, wrong);
            GetComponent<Collider>().enabled = false;
            transform.GetChild(0).GetComponent<Collider>().enabled = false;
        }
    }
    private void RandomFloor(int num, double result, double wrong)
    {
        switch (num)
        {
            case 0:
                floorRight.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = result.ToString();
                floorLeft.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = wrong.ToString();
                floorLeft.GetComponent<Rigidbody>().isKinematic = false;
                break;
            case 1:
                floorRight.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = wrong.ToString();
                floorLeft.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = result.ToString();
                floorRight.GetComponent<Rigidbody>().isKinematic = false;
                break;

        }

    }

}
