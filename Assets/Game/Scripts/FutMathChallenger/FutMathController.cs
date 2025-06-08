using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class FutMathController : MonoBehaviour

{
    public int dificulty = 1;
    [SerializeField] TextMeshPro billboardScore;
    [SerializeField] TextMeshPro goalScore;
    [SerializeField] GameObject altar;

    public GameObject ball;
    public GameObject[] lifesUI;
    public GameObject[] thopyWins;
    public List<GameObject> ballInScene = new();

    private List<int> nums;
    public int chances = 0;
    public int lifesTry = 3;
    public int rightAnswer = 0;

    private double resultado;
    private string expressao;

    public bool lastIsOp = false;

    private Regex operatorRegex = new(@"^[+\-*/]$");

    private void Start()
    {
        GameEventManager.instance.OnKickToGoalHandler += KickToGoal;
        GameEventManager.instance.OnEnterResetMathExpressionHandler += ResetTry;
        GerarDificuldade(dificulty);
        goalScore.text = "";
        FindObjectOfType<DisableEnergyUI>().SetUnactive();

    }


    private void Update()
    {
        if (lifesTry == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (rightAnswer == 3)
        {
            altar.SetActive(true);
        }
    }


    private void KickToGoal(string valor, string tag)
    {

        double resultadoNoGol;

        if (tag == "OpBall" && !lastIsOp)
        {
            goalScore.text += valor;
            chances--;
            lastIsOp = true;
        }

        if (tag == "MathBall")
        {
            goalScore.text += valor;
            resultadoNoGol = MathGen.Calculate(goalScore.text);
            goalScore.text = resultadoNoGol.ToString();

            if (resultadoNoGol == resultado && lastIsOp)
            {
                thopyWins[rightAnswer].SetActive(true);
                rightAnswer++;
                NovaTentativa();
            }

            if (resultadoNoGol != resultado && chances == 0)
            {
                lifesUI[lifesTry - 1].SetActive(false);
                lifesTry--;
                NovaTentativa();
            }
            lastIsOp = false;
        }

    }


    private void GerarDificuldade(int dificulty)
    {
        switch (dificulty)
        {
            case 1:
                GerarCampo(1, 1);
                break;

            case 2:
                GerarCampo(1, 2);
                break;

            case 3:
                GerarCampo(2, 2);

                break;

            case 4:
                GerarCampo(3, 2);
                break;

            case 5:
                GerarCampo(1, 3);
                break;

            case 6:
                GerarCampo(2, 3);
                break;

            case 7:
                GerarCampo(3, 3);
                break;

            default:
                throw new ArgumentException("Nível de dificuldade inválido.");
        }
    }
    private void GerarCampo(int n_operadores, int dificuldade)
    {
        char[] operadores = { '+', '-', '*' };

        do
        {
            (expressao, nums) = MathGen.GerarExpressão(operadores, n_operadores, 9, true, dificuldade);
            expressao = expressao.Replace(" ", "");
            resultado = MathGen.Calculate(expressao);

        } while (resultado == 0);

        billboardScore.text = resultado.ToString();
        goalScore.text = "";
        foreach (char operador in operadores)
        {
            SpawnBalls("OpBall", operador.ToString());
            chances++;

        }

        for (int i = 0; i < 13; i++)
        {
            int number = Random.Range(1, 10);

            if (i < expressao.Length - 1)
            {

                if (operatorRegex.IsMatch(expressao[i].ToString()))
                {
                    SpawnBalls("OpBall", expressao[i].ToString());
                    chances++;
                }
                else
                {
                    SpawnBalls("MathBall", number.ToString());
                }

            }
            else
            {
                SpawnBalls("MathBall", number.ToString());
            }

        }
    }

    private void NovaTentativa()
    {
        if (ballInScene.Count > 0)
        {
            foreach (GameObject ball in ballInScene)
            {
                Destroy(ball);
            }
        }
        chances = 0;

        ballInScene.Clear();
        GerarDificuldade(dificulty);
    }

    private void ResetTry(bool success)
    {
        if (success)
        {
            lifesUI[lifesTry - 1].SetActive(false);
            lifesTry--;

            NovaTentativa();
        }

    }
    public void SpawnBalls(string tag, string value)
    {
        float spawnPointX = 64.8f;
        int spawnPointZ = Random.Range(0, 72);
        int spawnPointY = 1;

        Vector3 spawnPosition = new(spawnPointX, spawnPointY, spawnPointZ);

        GameObject ballToInstantiate = Instantiate(ball, spawnPosition, Quaternion.identity);

        ballToInstantiate.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = value.ToString();
        ballToInstantiate.transform.GetChild(0).GetChild(0).gameObject.tag = tag;
        ballInScene.Add(ballToInstantiate);

    }
}
