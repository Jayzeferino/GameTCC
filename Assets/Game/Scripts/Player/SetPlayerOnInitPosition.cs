using UnityEngine;
using UnityEngine.SceneManagement;

public class SetPlayerOnInitPosition : MonoBehaviour
{

    public Transform initPosition;
    CharacterMovement playerMove;
    public int mapId;
    private void Start()
    {

        if (playerMove == null)
        {
            playerMove = FindObjectOfType<CharacterMovement>();
            playerMove.SetNewPosition(initPosition.position);
            UIController.Instance.PlayMapMusic(mapId);
        }
    }
}
