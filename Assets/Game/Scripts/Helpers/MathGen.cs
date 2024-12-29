using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;

class MathGen
{
    public static double Calculate(string expressao)
    {
        DataTable table = new();
        table.Columns.Add("expressao", typeof(string), expressao);
        DataRow row = table.NewRow();
        table.Rows.Add(row);
        return double.Parse((string)row["expressao"]);
    }

    public static (string, List<int>) GerarExpressão(char[] operacoes, int numeroDeOperaçoes, int maxNum, bool negativeNumbers, int dificuldade)
    {

        List<int> nums = new();
        string expressao = "";
        int numero1, numero2;
        string operacao;
        int fisrtRangeNumber = 0;
        bool parentesis = false;

        if (negativeNumbers)
        {
            fisrtRangeNumber = -maxNum;
        }

        for (int i = 0; i < numeroDeOperaçoes; i++)
        {
            numero1 = Random.Range(fisrtRangeNumber, maxNum);
            operacao = EscolherOperacao(operacoes);
            parentesis = GerarBoolAleatorio();
            nums.Add(numero1);

            if (dificuldade > 2 && parentesis == true && !(i == numeroDeOperaçoes - 2))
            {
                numero2 = Random.Range(fisrtRangeNumber, maxNum);
                expressao += $"{numero1} {operacao} {numero2}";
                expressao = $"({expressao})";
                nums.Add(numero2);
            }
            else
            {
                expressao += $"{numero1} {operacao}";
            }


            if (i == numeroDeOperaçoes - 1)
            {

                numero2 = Random.Range(fisrtRangeNumber, maxNum);
                expressao += $"{numero2}";
                nums.Add(numero2);
            }

        }

        return (expressao, nums);
    }
    private static string EscolherOperacao(char[] operacoes)
    {
        int index = Random.Range(0, operacoes.Length);
        return operacoes[index].ToString();
    }

    private static bool GerarBoolAleatorio()
    {
        return Random.value > 0.5f;
    }
}