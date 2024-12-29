using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followTheBall : MonoBehaviour
{

    public Transform BallTranform;

    void Update()
    {

        gameObject.transform.SetPositionAndRotation(BallTranform.position + new Vector3(0, (BallTranform.localScale.y * 1f), 0), Quaternion.Euler(0, 0, 0));
    }
}
