using TMPro;
using UnityEngine;

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

    public void ResetButton()
    {
        buttonPressed = false;
        float moveAmount = 0.5f;
        currentPosition = transform.position;
        transform.position = Vector3.MoveTowards(currentPosition, new Vector3(currentPosition.x, currentPosition.y + moveAmount, currentPosition.z), 1 * Time.deltaTime);
        Material material = transform.GetChild(0).GetComponent<Renderer>().material;
        material.color = new Color(1f, 1f, 1f, 1f);
        material.DisableKeyword("_EMISSION");
        material.SetColor("_EmissionColor", material.color);
    }
    private void OnCollisionEnter(Collision collider)
    {

        if (collider.gameObject.CompareTag("Player") && !buttonPressed)
        {

            if (transform.position.y > -0.35)
            {
                transform.position = Vector3.MoveTowards(currentPosition, new Vector3(currentPosition.x, currentPosition.y - moveAmount, currentPosition.z), 1 * Time.deltaTime);
                //efeito
                string letter = transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text.ToString();
                GameEventManager.instance.TermoButtonPressed(letter, buttonId);
                buttonPressed = true;
            }

        }

    }




}