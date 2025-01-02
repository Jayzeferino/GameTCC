using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ForcaController : MonoBehaviour
{
    [SerializeField] GameObject Buttons;
    [SerializeField] TextMeshPro challengeText;
    [SerializeField] Transform Player;
    [SerializeField] TextMeshPro temaText;
    [SerializeField] GameObject key;
    public List<GameObject> lifesUI;
    public int limiteErros = 5;
    public int vidas = 3;
    public int tryWord = 0;
    private string palavraDesafio;
    private string palavraDesafioSemAcento;
    private string palavraOfuscada;
    private string temaEscolhido;

    void Start()
    {
        InitButtons();
        IniciarLevel();
        GameEventManager.instance.OnTermoButtonPressedHandler += ButtonPressed;
    }

    private void IniciarLevel()
    {
        tryWord = 0;
        temaEscolhido = EscolheTema();
        (palavraDesafio, palavraOfuscada) = BuscaPalavraEmArquivo($"palavras/{temaEscolhido}");
        temaText.text = temaEscolhido;
        challengeText.text = palavraOfuscada;
        palavraDesafioSemAcento = RemoverAcentuacao(palavraDesafio);

        limiteErros = (int)Math.Ceiling((decimal)(palavraDesafio.Length / 2));
    }
    private void ReiniciarLevel()
    {
        Player.GetComponent<CharacterMovement>().SetNewPosition(new Vector3(-4f, 1.584463f, -39.71f));
        IniciarLevel();
        RevertButtonsMaterials();
        InitButtons();

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
        if (tryWord == limiteErros)
        {
            ReiniciarLevel();
            lifesUI[vidas - 1].SetActive(false);
            vidas--;
        }

        if (palavraOfuscada == palavraDesafio)
        {

            key.SetActive(true);
            Buttons.SetActive(false);
        }
    }

    public bool CheckAndModifyPalavraOfuscada(string letraApertada)
    {
        bool hasLetter = false;

        StringBuilder palavraAlterada = new(palavraOfuscada);

        for (int i = 0; i < palavraDesafio.Length; i++)
        {
            if (palavraDesafioSemAcento[i].ToString() == letraApertada.ToLower())
            {
                palavraAlterada[i] = palavraDesafio[i];
                hasLetter = true;
            }
        }


        palavraOfuscada = palavraAlterada.ToString();
        Debug.Log($"letraApertada:{letraApertada} palavraDesafio:{palavraDesafio}  palavraOfuscada:{palavraOfuscada}");

        return hasLetter;

    }

    private void InitButtons()
    {
        char letter = 'A';
        List<int> buttonsIndex = new();

        for (int i = 0; i <= 25; i++)
        {
            buttonsIndex.Add(i);
        }

        Shuffle(buttonsIndex);

        foreach (var button in buttonsIndex)
        {
            Buttons.transform.GetChild(button).transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = letter.ToString();
            letter++;
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
        string wordFilePath = "Assets/Resources/palavras/temas.txt";

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
        bool hasLetter = CheckAndModifyPalavraOfuscada(letraApertada: letter);
        if (hasLetter)
        {
            challengeText.text = palavraOfuscada;

        }
        else
        {
            tryWord += 1;
        }



        GameEventManager.instance.ChangedButtonColor(hasLetter, id);
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


