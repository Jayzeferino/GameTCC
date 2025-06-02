using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class SyllableSeparator
{
    public static List<string> SeparateSyllables(string word)
    {
        List<string> syllables = new List<string>();
        word = word.ToLowerInvariant(); // Trabalha com minúsculas para aplicação consistente das regras

        // Detecção básica de vogais (considerando vogais acentuadas comuns em português)
        string vowels = "aeiouáàãéêíóôõúü";
        string consonants = "bcdfghjklmnpqrstvwxyz"; // Consoantes básicas

        string currentRemaining = word;

        while (currentRemaining.Length > 0)
        {
            // Caso 1: Consoante-Vogal (CV) ou Vogal (V)
            if (currentRemaining.Length >= 2 &&
                consonants.Contains(currentRemaining[0]) &&
                vowels.Contains(currentRemaining[1]))
            {
                // Tenta encontrar um padrão CVC (consoante-vogal-consoante)
                if (currentRemaining.Length >= 3 && consonants.Contains(currentRemaining[2]))
                {
                    // Verifica se a próxima sílaba potencial começaria com vogal (formando CVC-V)
                    // ou se estamos lidando com um encontro consonantal que deve permanecer junto.
                    // Esta é a parte mais complexa e onde a heurística pode falhar.
                    // Para CVC, geralmente a última consoante pertence à sílaba atual.
                    syllables.Add(currentRemaining.Substring(0, 3));
                    currentRemaining = currentRemaining.Substring(3);
                }
                else // Caso simples CV
                {
                    syllables.Add(currentRemaining.Substring(0, 2));
                    currentRemaining = currentRemaining.Substring(2);
                }
            }
            // Caso 2: Somente Vogal (V) no início ou encontro de vogais
            else if (vowels.Contains(currentRemaining[0]))
            {
                // Tenta agrupar uma vogal sozinha ou com uma consoante que a siga
                if (currentRemaining.Length >= 2 && consonants.Contains(currentRemaining[1]))
                {
                    // Padrão VC
                    syllables.Add(currentRemaining.Substring(0, 2));
                    currentRemaining = currentRemaining.Substring(2);
                }
                else // Somente vogal ou hiato (V-V)
                {
                    syllables.Add(currentRemaining.Substring(0, 1));
                    currentRemaining = currentRemaining.Substring(1);
                }
            }
            // Caso 3: Duas consoantes seguidas (ou mais) - tentar manter duas letras juntas
            else if (currentRemaining.Length >= 2 &&
                     consonants.Contains(currentRemaining[0]) &&
                     consonants.Contains(currentRemaining[1]))
            {
                // Lida com casos como "bl", "tr", "ss", "rr", "nh", "ch", "lh"
                // Aqui, a complexidade aumenta devido aos dígrafos e encontros consonantais.
                // Para simplificar, tenta pegar até a próxima vogal.

                int nextVowelIndex = -1;
                for (int i = 0; i < currentRemaining.Length; i++)
                {
                    if (vowels.Contains(currentRemaining[i]))
                    {
                        nextVowelIndex = i;
                        break;
                    }
                }

                if (nextVowelIndex != -1)
                {
                    syllables.Add(currentRemaining.Substring(0, nextVowelIndex + 1));
                    currentRemaining = currentRemaining.Substring(nextVowelIndex + 1);
                }
                else // Se não encontrar vogal, adiciona o restante
                {
                    syllables.Add(currentRemaining);
                    currentRemaining = "";
                }
            }
            // Caso 4: Adiciona o restante como uma sílaba se nenhuma regra anterior se aplicou
            else
            {
                syllables.Add(currentRemaining);
                currentRemaining = ""; // Finaliza o loop
            }

            // Regra especial para "ss" e "rr" (sempre se separam)
            // Esta é uma pós-processamento simples.
            if (syllables.Count > 0)
            {
                string lastSyllable = syllables.Last();
                if (lastSyllable.Length >= 2)
                {
                    char lastChar = lastSyllable[lastSyllable.Length - 1];
                    char secondLastChar = lastSyllable[lastSyllable.Length - 2];

                    if ((lastChar == 's' && secondLastChar == 's') ||
                        (lastChar == 'r' && secondLastChar == 'r'))
                    {
                        syllables.RemoveAt(syllables.Count - 1);
                        syllables.Add(lastSyllable.Substring(0, lastSyllable.Length - 1));
                        syllables.Add(lastSyllable.Substring(lastSyllable.Length - 1, 1));
                    }
                }
            }
        }

        // Limpa sílabas vazias que podem ter sido geradas
        return syllables.Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
    }

}