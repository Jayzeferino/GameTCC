using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class ForcaController : MonoBehaviour
{
    [SerializeField] GameObject Buttons;
    [SerializeField] TextMeshPro challengeText;
    [SerializeField] CharacterMovement Player;
    [SerializeField] Transform entrada;
    [SerializeField] TextMeshPro temaText;
    [SerializeField] GameObject key;
    public List<GameObject> lifesUI;
    DificultyLvManager dificuldadeLvManager;
    public int limiteErros = 5;
    public int vidas = 3;
    public int tryWord = 0;
    public int dificulty = 1;
    private int buttonsIndexCount = 0;
    private string palavraDesafio;
    private string palavraDesafioSemAcento;
    private string palavraOfuscada;
    private string temaEscolhido;
    private List<string> listSyllables;
    public AudioClip buttonPressedFx;
    public AudioClip failClip;
    public AudioClip successClip;

    void Awake()
    {
        dificuldadeLvManager = GetComponent<DificultyLvManager>();
        if (PlayerPrefs.HasKey($"Dificuldade_{SceneManager.GetActiveScene().name}") && PlayerPrefs.HasKey("last_save"))
        {
            dificulty = PlayerPrefs.GetInt($"Dificuldade_{SceneManager.GetActiveScene().name}", 1);
            dificuldadeLvManager.SetChallengeLevel(dificulty);
        }
    }
    void Start()
    {
        listSyllables = new();
        IniciarLevel();
        InitButtons();
        GameEventManager.instance.OnTermoButtonPressedHandler += ButtonPressed;
        if (Player == null)
        {
            Player = FindObjectOfType<CharacterMovement>();
        }
        FindObjectOfType<DisableUIForMiniGames>().SetUnactive();

    }

    private void IniciarLevel()
    {
        tryWord = 0;
        temaEscolhido = EscolheTema();
        dificulty = Math.Clamp(dificulty, 1, 10);
        (palavraDesafio, palavraOfuscada) = BuscaPalavraEmArquivo($"palavras/LV{dificulty}/{temaEscolhido}");
        temaText.text = temaEscolhido;
        challengeText.text = palavraOfuscada;
        palavraDesafioSemAcento = RemoverAcentuacao(palavraDesafio);
        int novoLimiteDeErros = (int)Math.Ceiling((decimal)(palavraDesafio.Length / 2));

        if (novoLimiteDeErros > limiteErros)
        {
            limiteErros = novoLimiteDeErros;
        }
    }
    private void ReiniciarLevel()
    {
        listSyllables = new();
        buttonsIndexCount = 0;
        Player.GetComponent<CharacterMovement>().SetNewPosition(entrada.transform.position);
        IniciarLevel();
        RevertButtonsMaterials();
        InitButtons();

    }

    private void FimDesafio()
    {
        EnterChallengesManager.Instance.UpdatePriorityPT();
        SceneManager.LoadScene("MainMap");
    }

    private void RevertButtonsMaterials()
    {
        for (int i = 0; i <= 25; i++)
        {

            Buttons.transform.GetChild(i).GetComponent<OnPressTermoButtonChallenge>().ResetButton();

        }
    }
    private void Update()
    {
        if (tryWord >= limiteErros)
        {
            ReiniciarLevel();
            lifesUI[vidas - 1].SetActive(false);
            vidas--;
        }

        if (vidas == 0)
        {
            UIController.Instance.PlayUIFx(failClip);
            FimDesafio();
        }

        if (palavraOfuscada == palavraDesafio)
        {
            key.SetActive(true);
            Buttons.SetActive(false);

        }
    }

    // public bool CheckAndModifyPalavraOfuscada(string letraApertada)
    // {
    //     bool hasLetter = false;

    //     StringBuilder palavraAlterada = new(palavraOfuscada);

    //     letraApertada = letraApertada.ToString();

    //     if (palavraDesafioSemAcento.Contains(letraApertada))
    //     {
    //         Debug.Log($"letraApertada:{letraApertada} tem o Index {palavraDesafioSemAcento.IndexOf(letraApertada)}");

    //         for (int i = 0; i < letraApertada.Length; i++)
    //         {
    //             if (letraApertada[i] == palavraDesafioSemAcento[palavraDesafioSemAcento.IndexOf(letraApertada) + i])
    //             {
    //                 palavraAlterada[i] = palavraDesafio[i];
    //                 hasLetter = true;
    //             }
    //         }
    //     }


    //     // for (int i = 0; i < palavraDesafio.Length; i++)
    //     // {
    //     //     if (palavraDesafioSemAcento[i].ToString() == letraApertada.ToLower())
    //     //     {
    //     //         palavraAlterada[i] = palavraDesafio[i];
    //     //         hasLetter = true;
    //     //     }

    //     // }


    //     palavraOfuscada = palavraAlterada.ToString();
    //     Debug.Log($"letraApertada:{letraApertada} palavraDesafio:{palavraDesafio}  palavraOfuscada:{palavraOfuscada}");

    //     return hasLetter;

    // }

    public bool CheckAndModifyPalavraOfuscada(string entradaApertada)
    {
        bool foundMatch = false;
        StringBuilder palavraAlteradaBuilder = new StringBuilder(palavraOfuscada);

        string entradaFormatada = RemoverAcentuacao(entradaApertada.ToLower());

        for (int i = 0; i < palavraDesafioSemAcento.Length; i++)
        {
            if (i + entradaFormatada.Length <= palavraDesafioSemAcento.Length &&
                palavraDesafioSemAcento.Substring(i, entradaFormatada.Length) == entradaFormatada)
            {
                for (int j = 0; j < entradaFormatada.Length; j++)
                {
                    palavraAlteradaBuilder[i + j] = palavraDesafio[i + j];
                }
                foundMatch = true;
            }
        }

        palavraOfuscada = palavraAlteradaBuilder.ToString();

        Debug.Log($"Entrada: '{entradaApertada}' | Desafio: '{palavraDesafio}' | Ofuscada: '{palavraOfuscada}' | Encontrado: {foundMatch}");

        return foundMatch;
    }

    private void InitButtons()
    {
        List<int> buttonsIndex = new();

        for (int i = 0; i <= 25; i++)
        {
            buttonsIndex.Add(i);
            buttonsIndexCount++;
        }

        Shuffle(buttonsIndex);

        if (dificulty < 2)
        {
            SepararPalavraEAdicionaNaLista(palavraDesafio);
            BuscarListaDePalavras(1);
            while (listSyllables.Count > 1)
            {
                SepararPalavraEAdicionaNaLista(listSyllables[^1]);
                listSyllables.RemoveAt(listSyllables.Count - 1);
            }
        }
        else if (dificulty < 3)
        {
            SepararPalavraEAdicionaNaLista(palavraDesafio);
            BuscarListaDePalavras(2);
            while (listSyllables.Count > 1)
            {
                SepararPalavraEAdicionaNaLista(listSyllables[^1]);
                listSyllables.RemoveAt(listSyllables.Count - 1);
            }

        }
        else if (dificulty < 4)
        {
            SepararPalavraEAdicionaNaLista(palavraDesafio);
            BuscarListaDePalavras(4);
            while (listSyllables.Count > 1)
            {
                SepararPalavraEAdicionaNaLista(listSyllables[^1]);
                listSyllables.RemoveAt(listSyllables.Count - 1);
            }
        }
        else if (dificulty < 5)
        {
            SepararPalavraEAdicionaNaLista(palavraDesafio);
            BuscarListaDePalavras(5);
            while (listSyllables.Count > 1)
            {
                SepararPalavraEAdicionaNaLista(listSyllables[^1]);
                listSyllables.RemoveAt(listSyllables.Count - 1);

            }
        }
        else
        {
            char letter = 'A';
            foreach (var button in buttonsIndex)
            {
                Buttons.transform.GetChild(button).transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = letter.ToString();
                letter++;
            }
        }

    }

    private void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            (list[randomIndex], list[i]) = (list[i], list[randomIndex]);
        }
    }

    private (string, string) BuscaPalavraEmArquivo(string caminhoArquivo)
    {

        TextAsset arquivoTexto = Resources.Load<TextAsset>(caminhoArquivo);

        if (arquivoTexto != null)
        {
            string[] dicionario = arquivoTexto.text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            if (dicionario.Length > 0)
            {
                System.Random rand = new();
                int indiceAleatorio = rand.Next(dicionario.Length);

                string palavraAleatorio = dicionario[indiceAleatorio].Trim();
                string palavraOfuscada = "";

                for (int i = 0; i < palavraAleatorio.Length; i++)
                {

                    if (palavraAleatorio[i] != ' ' || palavraAleatorio[i] != '-')
                    {
                        palavraOfuscada += "█";
                    }

                    if (palavraAleatorio[i] == ' ')
                    {
                        palavraOfuscada += ' ';
                    }
                    if (palavraAleatorio[i] == '-')
                    {
                        palavraOfuscada += '-';


                    }
                }

                return (palavraAleatorio.ToLower(), palavraOfuscada);
            }
            else
            {
                return ("", "");
            }
        }
        else
        {
            return ("", "");
        }
    }

    private string EscolheTema()
    {
        // Caminho para o arquivo que contém as palavras (nomes de arquivos)
        string wordFilePath = $"Assets/Resources/palavras/LV{dificulty}/temas.txt";

        // Verificar se o arquivo com a lista de palavras existe
        if (!File.Exists(wordFilePath))
        {
            Console.WriteLine("O arquivo de lista de palavras não foi encontrado.");
            return "";
        }

        // Ler todas as palavras do arquivo
        string[] words = File.ReadAllLines(wordFilePath);

        if (words.Length == 0)
        {
            Console.WriteLine("O arquivo de lista de palavras está vazio.");
            return "";
        }

        // Selecionar uma palavra aleatória

        string randomWord = words[Random.Range(0, words.Length)];

        return randomWord;
    }
    private void ButtonPressed(string letter, int id)
    {
        bool hasLetter = CheckAndModifyPalavraOfuscada(letter);
        if (hasLetter)
        {
            challengeText.text = palavraOfuscada;
        }
        else
        {
            tryWord += 1;
        }
        GameEventManager.instance.ChangedButtonColor(hasLetter, id);


        UIController.Instance.PlayUIFx(buttonPressedFx);
        if (hasLetter)
        {
            UIController.Instance.PlayUIFx(successClip);
        }

    }

    // private void SepararPalavraEAdicionaNaLista(string palavra)
    // {
    //     List<string> syllables = SyllableSeparator.SeparateSyllables(palavra);
    //     foreach (string sil in syllables)
    //     {
    //         if (buttonsIndexCount > 0 && sil.Count() >= 2)
    //         {
    //             Buttons.transform.GetChild(buttonsIndexCount - 1).transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = sil;
    //             buttonsIndexCount--;
    //         }
    //     }
    // }

    private void SepararPalavraEAdicionaNaLista(string palavra)
    {
        List<string> syllables = SyllableSeparator.SeparateSyllables(palavra);

        foreach (string sil in syllables)
        {
            // Se ainda há botões para preencher
            if (buttonsIndexCount > 0)
            {
                // Opcional: ignora sílabas vazias
                if (!string.IsNullOrWhiteSpace(sil))
                {
                    // Acessa o botão correspondente e define o texto
                    TMP_Text textComponent = Buttons.transform
                        .GetChild(buttonsIndexCount - 1)
                        .GetChild(1)
                        .GetChild(0)
                        .GetComponent<TMP_Text>();

                    textComponent.text = sil;
                    buttonsIndexCount--;
                }
            }
        }
    }

    private void BuscarListaDePalavras(int palavras)
    {
        string palavra;
        for (int i = 0; i < palavras; i++)
        {
            (palavra, _) = BuscaPalavraEmArquivo($"palavras/LV10/{temaEscolhido}");
            listSyllables.Add(palavra.Trim());
        }
    }

    private string RemoverAcentuacao(string palavra)
    {
        if (string.IsNullOrEmpty(palavra))
            return palavra;

        // Normaliza a string para o formato FormD (decomposição)
        string palavraNormalizada = palavra.Normalize(NormalizationForm.FormD);

        // Filtra os caracteres que não são acentos (diacríticos)
        StringBuilder sb = new StringBuilder();

        foreach (char c in palavraNormalizada)
        {
            UnicodeCategory categoria = CharUnicodeInfo.GetUnicodeCategory(c);

            // Só mantém os caracteres que não são de categoria "NonSpacingMark" (que são os acentos)
            if (categoria != UnicodeCategory.NonSpacingMark)
            {
                sb.Append(c);
            }
        }

        // Normaliza de volta para o formato FormC (composição) e retorna a palavra sem acentos
        return sb.ToString().Normalize(NormalizationForm.FormC);
    }

}


