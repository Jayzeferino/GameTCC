using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playgame : MonoBehaviour
{
    // Start is called before the first frame updatePl
    public void PlayGame()
    {
        // Load the game scene

        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMap");
    }
}
