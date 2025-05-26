using UnityEngine;

public class SetPlayerOnInitPosition : MonoBehaviour
{

    public Transform initPosition;
    CharacterMovement playerMove;
    private void Start()
    {

        if (playerMove == null)
        {
            playerMove = FindObjectOfType<CharacterMovement>();
            playerMove.SetNewPosition(initPosition.position);
        }
    }
}
