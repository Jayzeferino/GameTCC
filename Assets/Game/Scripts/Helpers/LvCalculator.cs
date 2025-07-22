using UnityEngine;

// A classe deve ser 'public static'
public static class LevelCalculator
{
    // --- Parâmetros de Nível para MATH ---
    // Você pode definir valores padrão aqui ou passar como parâmetros para a função
    // Se forem passados como parâmetros, essas variáveis não seriam estáticas
    // ou precisariam ser 'const' ou 'readonly'.
    // Para um helper, é mais comum que os parâmetros sejam passados na chamada da função.

    // A função para calcular o nível de MATH
    /// <summary>
    /// Calcula o nível de Math com base nos pontos de experiência acumulados.
    /// Utiliza uma curva de raiz quadrada para simular que cada nível é mais difícil de alcançar.
    /// </summary>
    /// <param name="mathLvAccumulated">Os pontos de Math acumulados do personagem.</param>
    /// <param name="initialLevel">O nível inicial do sistema (geralmente 1).</param>
    /// <param name="coefficient">Coeficiente que ajusta a velocidade de subida de nível (maior = mais rápido).</param>
    /// <param name="offset">Valor de offset para ajustar o início da curva (evita raiz de 0 e suaviza o início).</param>
    /// <returns>O nível de Math atual como um inteiro.</returns>
    public static int GetMathLevel(float mathLvAccumulated, int initialLevel = 1, float coefficient = 0.5f, float offset = 10f)
    {
        // Garante que o nível nunca seja menor que o nível inicial
        if (mathLvAccumulated <= 0)
        {
            return initialLevel;
        }

        // Calcula o nível usando a raiz quadrada para simular dificuldade crescente
        // Mathf.FloorToInt arredonda para baixo para garantir um nível inteiro
        int calculatedLevel = Mathf.FloorToInt(coefficient * Mathf.Sqrt(mathLvAccumulated + offset)) + initialLevel;

        // Garante que o nível não diminua e seja pelo menos o nível inicial
        return Mathf.Max(initialLevel, calculatedLevel);
    }

    // A função para calcular o nível de PT
    /// <summary>
    /// Calcula o nível de PT com base nos pontos de experiência acumulados.
    /// Utiliza uma curva de raiz quadrada para simular que cada nível é mais difícil de alcançar.
    /// </summary>
    /// <param name="ptLvAccumulated">Os pontos de PT acumulados do personagem.</param>
    /// <param name="initialLevel">O nível inicial do sistema (geralmente 1).</param>
    /// <param name="coefficient">Coeficiente que ajusta a velocidade de subida de nível (maior = mais rápido).</param>
    /// <param name="offset">Valor de offset para ajustar o início da curva (evita raiz de 0 e suaviza o início).</param>
    /// <returns>O nível de PT atual como um inteiro.</returns>
    public static int GetPTLevel(float ptLvAccumulated, int initialLevel = 1, float coefficient = 0.5f, float offset = 10f)
    {
        // Garante que o nível nunca seja menor que o nível inicial
        if (ptLvAccumulated <= 0)
        {
            return initialLevel;
        }

        // Calcula o nível usando a raiz quadrada para simular dificuldade crescente
        int calculatedLevel = Mathf.FloorToInt(coefficient * Mathf.Sqrt(ptLvAccumulated + offset)) + initialLevel;

        // Garante que o nível não diminua e seja pelo menos o nível inicial
        return Mathf.Max(initialLevel, calculatedLevel);
    }

    // --- Você pode adicionar outras funções auxiliares aqui, se necessário ---
    // Exemplo: Função para calcular XP necessária para o próximo nível (se você precisar disso)
    /*
    public static float CalculateXPForNextLevel(int currentLevel, float baseXP = 100f, float accelerationFactor = 20f)
    {
        // Exemplo de curva linear acelerada para XP necessária
        if (currentLevel == 0) return baseXP;
        return baseXP + (currentLevel * accelerationFactor);
    }
    */
}