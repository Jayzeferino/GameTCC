using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallColliderController : MonoBehaviour
{
    public bool allowNum = false;
    public bool allowOp = true;

    private double result;

    private void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.CompareTag("MathOperation") && allowOp == true)
        {
            char operation = collider.gameObject.GetComponent<OperationInteractableObject>().GetOperation();

            transform.GetChild(0).GetComponent<TMP_Text>().text += operation;

            Destroy(collider.gameObject);

            allowOp = false;

            allowNum = true;

        }


        if (collider.gameObject.CompareTag("MathBall") && allowNum == true)
        {
            string number = collider.transform.GetChild(0).GetComponent<TMP_Text>().text;

            gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text += number;

            result = MathGen.Calculate(gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text);

            float scale = (float)result * 0.2f;
            if (scale < 1)
            {
                scale = 1;
            }

            if (scale > 6)
            {
                scale = 6;
            }

            gameObject.transform.localScale *= scale;

            allowOp = true;

            allowNum = false;

            collider.gameObject.SetActive(false);
        }

    }
}
