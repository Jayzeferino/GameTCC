using UnityEngine; // Certifique-se de que UnityEngine está sendo usado para Mathf

public static class MapLevelCalculator
{
    /// <summary>
    /// Calcula o nível do mapa com base nos pontos de exploração acumulados.
    /// Utiliza uma curva de raiz quadrada para simular que cada nível é mais difícil de alcançar.
    /// </summary>
    /// <param name="explorationPointsAccumulated">Os pontos de exploração acumulados no mapa.</param>
    /// <param name="initialMapLevel">O nível inicial do mapa (geralmente 1).</param>
    /// <param name="levelingCoefficient">Coeficiente que ajusta a velocidade de subida de nível (maior = mais rápido).</param>
    /// <param name="curveOffset">Valor de offset para ajustar o início da curva (evita raiz de 0 e suaviza o início).</param>
    /// <returns>O nível atual do mapa como um inteiro.</returns>
    public static int GetMapLevel(float explorationPointsAccumulated, int initialMapLevel = 1, float levelingCoefficient = 0.5f, float curveOffset = 10f)
    {
        // Garante que o nível nunca seja menor que o nível inicial do mapa
        if (explorationPointsAccumulated <= 0)
        {
            return initialMapLevel;
        }

        // Calcula o nível usando a raiz quadrada para simular dificuldade crescente.
        // Mathf.FloorToInt arredonda para baixo para garantir um nível inteiro.
        int calculatedLevel = Mathf.FloorToInt(levelingCoefficient * Mathf.Sqrt(explorationPointsAccumulated + curveOffset)) + initialMapLevel;

        // Garante que o nível não diminua e seja pelo menos o nível inicial do mapa.
        return Mathf.Max(initialMapLevel, calculatedLevel);
    }
}