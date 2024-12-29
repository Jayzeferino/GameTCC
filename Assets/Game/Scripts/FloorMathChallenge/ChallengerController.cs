using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChallengerController : MonoBehaviour
{

    public int dificuldade = 1;

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
