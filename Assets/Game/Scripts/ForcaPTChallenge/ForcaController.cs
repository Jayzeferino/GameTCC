using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class ForcaController : MonoBehaviour
{
    [SerializeField] List<GameObject> Buttons;
    [SerializeField] TextMeshPro challengeText;
    private int limiteErros = 5;
    private int tryWord = 0;
    private string palavraDesafio;
    private string palavraDesafioSemAcento;
    private string palavraOfuscada;
    void Start()
    {
        InitButtons();
        (palavraDesafio, palavraOfuscada) = BuscaPalavraEmArquivo("palavras/adjetivos");
        challengeText.text = palavraOfuscada;
        palavraDesafioSemAcento = RemoverAcentuacao(palavraDesafio);

        limiteErros = (int)Math.Ceiling((decimal)(palavraDesafio.Length / 2));

        GameEventManager.instance.OnTermoButtonPressedHandler += ButtonPressed;
    }

    private void Update()
    {
        if (tryWord == limiteErros)
        {

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
            Buttons[button].transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = letter.ToString();
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
            string[] adjetivos = arquivoTexto.text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            if (adjetivos.Length > 0)
            {
                System.Random rand = new();
                int indiceAleatorio = rand.Next(adjetivos.Length);

                string palavraAleatorio = adjetivos[indiceAleatorio];
                string palavraOfuscada = "";
                for (int i = 0; i < palavraAleatorio.Length; i++)
                {
                    palavraOfuscada += "█";
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


