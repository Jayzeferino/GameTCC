using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerMap : MonoBehaviour
{
    private string atualScene;


    private void Awake()
    {
        atualScene = SceneManager.GetActiveScene().name;
    }
    private void Start()
    {
        if (atualScene == "MainMap")
        {
            WorldSaveGameManager.instance.SetLandInWorld();
        }
    }

}
