using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DetectGoal : MonoBehaviour
{
    private string lastTag;
    private bool goalProcessed = false;
    private HashSet<GameObject> processedBalls = new HashSet<GameObject>();

    private void OnTriggerEnter(Collider ball)
    {
        if (!ball.CompareTag("OpBall") && !ball.CompareTag("MathBall"))
            return;

        if (processedBalls.Contains(ball.gameObject))
            return;

        processedBalls.Add(ball.gameObject);

        string valor = ball.transform.GetChild(0).GetComponent<TMP_Text>().text;
        string tag = ball.gameObject.tag;

        GameEventManager.instance.BallInGoal(valor, tag);
        Destroy(ball.gameObject);
    }


}
