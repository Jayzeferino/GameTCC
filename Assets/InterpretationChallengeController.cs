using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterpretationChallengeController : MonoBehaviour
{
    [SerializeField] List<GameObject> TreasuresSlots;
    public int[] TreasuresPath;
    public List<int> TreasuresCollected;
    private int treasureCollected = 0;

    // [SerializeField] GameObject audioTipWrong;

    private void Start()
    {
        GameEventManager.instance.OnCollectTreasureHandler += TreasureCollect;

    }

    private void TreasureCollect(Sprite treasureImage, int id, GameObject treasure)
    {

        if (id == TreasuresPath[0])
        {
            TreasuresSlots[treasureCollected].transform.GetChild(0).GetComponent<Image>().sprite = treasureImage;
            TreasuresSlots[treasureCollected].transform.GetChild(3).gameObject.SetActive(false);//Vazio
            TreasuresSlots[treasureCollected].transform.GetChild(1).gameObject.SetActive(false);//Errado
            TreasuresSlots[treasureCollected].transform.GetChild(2).gameObject.SetActive(true);//Certo
            TreasuresCollected.Add(id);
            treasureCollected++;
            treasure.SetActive(false);
        }
        else
        {
            TreasuresSlots[treasureCollected].transform.GetChild(3).gameObject.SetActive(false);//Vazio
            TreasuresSlots[treasureCollected].transform.GetChild(2).gameObject.SetActive(false);//Certo
            TreasuresSlots[treasureCollected].transform.GetChild(1).gameObject.SetActive(true);//Errado

            // GameObject.Instantiate(audioTipWrong);
        }

    }
}
