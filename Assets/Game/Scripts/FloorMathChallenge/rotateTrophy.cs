using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateTrophy : MonoBehaviour
{
    // Start is called before the first frame update
    float rotationSpeed = 10f;
    void Update()
    {

        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.right);
    }
}
