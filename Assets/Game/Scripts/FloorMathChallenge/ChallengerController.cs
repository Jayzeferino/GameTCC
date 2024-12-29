using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class ChallengerController : MonoBehaviour
{
    [SerializeField] GameObject barraDeVida;
    public List<GameObject> FloorsSteps;
    public int dificuldade = 1;
    private bool playerFall = false;
    private int vidas = 3;

    private void Start()
    {
        GameEventManager.instance.OnFallOfBridgeHandler += FailInCalc;
        GameEventManager.instance.OnNextFloorStepHandler += UpdateBeforeFloorsStepsBasedInAtualFloor;
    }
    private void Update()
    {
        if (vidas == 0)
        {
            SceneManager.LoadScene("Island");
        }

        if (playerFall)
        {
            barraDeVida.transform.GetChild(vidas - 1).GetChild(1).gameObject.SetActive(false);
            vidas--;
            playerFall = false;
        }
    }

    private void FailInCalc(bool fail)
    {
        playerFall = fail;
    }

    private void UpdateBeforeFloorsStepsBasedInAtualFloor(int id)
    {
        if (id != 0)
        {
            FloorsSteps[id - 1].GetComponent<Rigidbody>().isKinematic = true;
            FloorsSteps[id - 2].GetComponent<Rigidbody>().isKinematic = true;
        }


    }

    public (string, double, double) GerarDesafio()
    {
        (string expression, double result, double wrong) = GerarOperacao(dificuldade);
        return (expression, result, wrong);
    }
    private (string, double, double) GerarOperacao(int dificuldade)
    {
        string expressao;
        List<int> nums;
        double resultado;
        double wrong;

        switch (dificuldade)
        {
            case 1:
                (expressao, nums) = MathGen.GerarExpressão(new char[] { '+', '-' }, 1, 16, false, dificuldade);
                resultado = MathGen.Calculate(expressao);
                wrong = Random.Range(0, 16);
                break;

            case 2:
                (expressao, nums) = MathGen.GerarExpressão(new char[] { '+', '-' }, 1, 16, true, dificuldade);
                resultado = MathGen.Calculate(expressao);
                wrong = Random.Range(0, 16);
                break;

            case 3:

                (expressao, nums) = MathGen.GerarExpressão(new char[] { '+', '-' }, 2, 16, true, dificuldade);
                resultado = MathGen.Calculate(expressao);
                wrong = Random.Range(0, 16);
                break;

            case 4:
                (expressao, nums) = MathGen.GerarExpressão(new char[] { '+', '-', '*' }, 2, 16, false, dificuldade);
                resultado = MathGen.Calculate(expressao);
                wrong = Random.Range(0, 50);
                break;

            case 5:
                (expressao, nums) = MathGen.GerarExpressão(new char[] { '+', '-', '*', '+', }, 3, 16, true, dificuldade);
                resultado = MathGen.Calculate(expressao);
                wrong = Random.Range(0, 100);
                break;

            case 6:
                (expressao, nums) = MathGen.GerarExpressão(new char[] { '+', '-', '*', '*' }, 3, 30, true, dificuldade);
                resultado = MathGen.Calculate(expressao);
                wrong = Random.Range(0, 500);
                break;

            default:
                throw new ArgumentException("Nível de dificuldade inválido.");
        }

        if (wrong == resultado)
        {

            if (dificuldade == 4)
            {
                wrong = Random.Range(0, 50);

            }

            if (dificuldade == 5)
            {
                wrong = Random.Range(0, 100);

            }

            if (dificuldade == 6)
            {
                wrong = Random.Range(0, 30);

            }
            else
            {
                wrong = Random.Range(0, 16);
            }

        }

        return (expressao, resultado, wrong);
    }
}
