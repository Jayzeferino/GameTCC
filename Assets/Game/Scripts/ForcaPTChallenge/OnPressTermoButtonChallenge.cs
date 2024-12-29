using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnPressTermoButtonChallenge : MonoBehaviour
{
    public float moveAmount = 0.5f;
    public float yOrigin = -0.16f;
    private bool buttonPressed = false;
    private Vector3 currentPosition;
    [SerializeField] private int buttonId;

    private void Start()
    {
        currentPosition = transform.position;

    }

    private void OnCollisionEnter(Collision collider)
    {

        if (collider.gameObject.CompareTag("Player") && !buttonPressed)
        {

            if (transform.position.y > -0.35)
            {
                transform.position = Vector3.MoveTowards(currentPosition, new Vector3(currentPosition.x, currentPosition.y - moveAmount, currentPosition.z), 1 * Time.deltaTime);
                transform.GetChild(1).gameObject.SetActive(true);

                string letter = transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text.ToString();
                GameEventManager.instance.TermoButtonPressed(letter, buttonId);
                buttonPressed = true;
            }

        }

    }




}