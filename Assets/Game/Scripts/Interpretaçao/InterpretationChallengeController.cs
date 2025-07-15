using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InterpretationChallengeController : MonoBehaviour
{

    public GameObject key;
    CharacterMovement player;

    [Header("Slots de Tesouros no mapa")]
    [SerializeField] List<GameObject> TreasuresSlots;
    public List<int> TreasuresPath;
    public int treasuresToBeCollected = 3;
    public int treasureCollected = 0;
    private bool keydropped = false;
    public AudioClip collectTreasureSound;
    public AudioClip successSound;
    private Historia selectedHistory;

    [Header("DicasUI")]
    [SerializeField] GameObject historyPanelText;
    [SerializeField] TextMeshProUGUI dicaText;
    [SerializeField] TextMeshProUGUI erroDicaText;
    public float timeTipText = 5f;

    private InputActions inputActions;
    private void Awake()
    {
        player = FindObjectOfType<CharacterMovement>();
        inputActions = new InputActions();
        inputActions.Enable();
    }
    private void Start()
    {
        GameEventManager.instance.OnCollectTreasureHandler += TreasureCollect;
        FindObjectOfType<DisableUIForMiniGames>().SetUnactive();
        JsonReader();

    }
    private void Update()
    {
        if (inputActions.Game.ShowHistory.WasPressedThisFrame())
        {
            historyPanelText.SetActive(true);
        }

        if (inputActions.Game.ShowHistory.WasReleasedThisFrame())
        {
            historyPanelText.SetActive(false);

        }

        if (treasuresToBeCollected == treasureCollected && keydropped == false)
        {
            key.transform.position = player.transform.position + new Vector3(0, 0.7f, 1f);
            key.SetActive(true);
            keydropped = true;
            UIController.Instance.PlayUIFx(successSound);
        }

    }
    private void TreasureCollect(Sprite treasureImage, int id, GameObject treasure)
    {
        if (id == TreasuresPath[treasureCollected])
        {
            TreasuresSlots[treasureCollected].transform.GetChild(0).GetComponent<Image>().sprite = treasureImage;
            TreasuresSlots[treasureCollected].transform.GetChild(3).gameObject.SetActive(false);//Vazio
            TreasuresSlots[treasureCollected].transform.GetChild(1).gameObject.SetActive(false);//Errado
            TreasuresSlots[treasureCollected].transform.GetChild(2).gameObject.SetActive(true);//Certo
            treasureCollected++;
            string text = historyPanelText.transform.GetChild(treasureCollected).GetComponent<TMP_Text>().text;
            historyPanelText.transform.GetChild(treasureCollected).GetComponent<TMP_Text>().text = $"<s>{text}</s>";
            treasure.SetActive(false);
            UIController.Instance.PlayUIFx(collectTreasureSound);
        }
        else
        {
            TreasuresSlots[treasureCollected].transform.GetChild(3).gameObject.SetActive(false);//Vazio
            TreasuresSlots[treasureCollected].transform.GetChild(2).gameObject.SetActive(false);//Certo
            TreasuresSlots[treasureCollected].transform.GetChild(1).gameObject.SetActive(true);//Errado
            StartCoroutine(DisplayTipText(timeTipText));
        }

    }


    IEnumerator DisplayTipText(float displayDuration)
    {
        erroDicaText.gameObject.SetActive(true);
        if (treasureCollected == 0)
        {
            dicaText.text = selectedHistory.Dicas.tesouro1[Random.Range(0, selectedHistory.Dicas.tesouro1.Count)];
        }
        if (treasureCollected == 1)
        {
            dicaText.text = selectedHistory.Dicas.tesouro2[Random.Range(0, selectedHistory.Dicas.tesouro2.Count)];
        }
        if (treasureCollected == 2)
        {
            dicaText.text = selectedHistory.Dicas.tesouro3[Random.Range(0, selectedHistory.Dicas.tesouro3.Count)];
        }

        yield return new WaitForSeconds(displayDuration);
        dicaText.text = "";
        erroDicaText.gameObject.SetActive(false);
    }


    private void JsonReader()
    {
        // Caminho para o arquivo JSON
        TextAsset jsonFile = Resources.Load<TextAsset>("Historias/historias");

        if (jsonFile != null)
        {
            // Deserializa o JSON no objeto C#
            HistoriasRoot historias = JsonUtility.FromJson<HistoriasRoot>(jsonFile.text);
            int IdHistoria = Random.Range(0, historias.Historias.Count);
            foreach (var historia in historias.Historias)
            {
                if (historia.ID == IdHistoria)
                {
                    historyPanelText.transform.GetChild(0).GetComponentInChildren<TMP_Text>().text = historia.Texto.titulo;
                    historyPanelText.transform.GetChild(1).GetComponentInChildren<TMP_Text>().text = historia.Texto.tesouro1;
                    historyPanelText.transform.GetChild(2).GetComponentInChildren<TMP_Text>().text = historia.Texto.tesouro2;
                    historyPanelText.transform.GetChild(3).GetComponentInChildren<TMP_Text>().text = historia.Texto.tesouro3;
                    historyPanelText.transform.GetChild(4).GetComponentInChildren<TMP_Text>().text = historia.Texto.final;
                    TreasuresPath = historia.OrdemTesouros;
                    selectedHistory = historia;
                }
            }
        }
        else
        {
            Debug.LogError("Arquivo JSON não encontrado na pasta Resources.");
        }
    }

}
