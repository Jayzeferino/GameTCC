using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DetectGoal : MonoBehaviour
{
    private string lastTag;

    private void OnTriggerEnter(Collider ball)
    {
        if (ball.gameObject.CompareTag("OpBall"))
        {
            string valor = ball.transform.GetChild(0).GetComponent<TMP_Text>().text.ToString();
            Destroy(ball);
            lastTag = ball.gameObject.tag;
            GameEventManager.instance.BallInGoal(valor, lastTag);
        }


        if (ball.gameObject.CompareTag("MathBall"))
        {

            string valor = ball.transform.GetChild(0).GetComponent<TMP_Text>().text.ToString();
            Destroy(ball);
            lastTag = ball.gameObject.tag;
            GameEventManager.instance.BallInGoal(valor, lastTag);

        }
    }
}
