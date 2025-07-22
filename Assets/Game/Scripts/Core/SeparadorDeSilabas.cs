using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class SyllableSeparator
{
    public static List<string> SeparateSyllables(string word)
    {
        List<string> syllables = new List<string>();
        word = word.ToLowerInvariant();

        string vowels = "aeiouáàãéêíóôõúü";
        string consonants = "bcdfghjklmnpqrstvwxyz";

        int i = 0;
        while (i < word.Length)
        {
            int remaining = word.Length - i;

            // Caso 1: Duas vogais seguidas (ex: "ai", "ou")
            if (remaining >= 2 && vowels.Contains(word[i]) && vowels.Contains(word[i + 1]))
            {
                syllables.Add(word.Substring(i, 2));
                i += 2;
            }
            // Caso 2: Duas consoantes + vogal (ex: "bra", "tro", "clo")
            else if (remaining >= 3 &&
                     consonants.Contains(word[i]) &&
                     consonants.Contains(word[i + 1]) &&
                     vowels.Contains(word[i + 2]))
            {
                syllables.Add(word.Substring(i, 3));
                i += 3;
            }
            // Caso 3: Pega duas letras normais (CV, VC, etc)
            else if (remaining >= 2)
            {
                syllables.Add(word.Substring(i, 2));
                i += 2;
            }
            // Caso 4: Última letra
            else
            {
                syllables.Add(word.Substring(i, 1));
                i += 1;
            }
        }

        // Pós-processamento para separar "ss" ou "rr"
        for (int j = 0; j < syllables.Count; j++)
        {
            string sil = syllables[j];
            if (sil.Length >= 2)
            {
                int len = sil.Length;
                char last = sil[len - 1];
                char secondLast = sil[len - 2];

                if ((last == 's' && secondLast == 's') ||
                    (last == 'r' && secondLast == 'r'))
                {
                    syllables[j] = sil.Substring(0, len - 1);
                    syllables.Insert(j + 1, last.ToString());
                    j++; // pula a nova sílaba inserida
                }
            }
        }

        return syllables.Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
    }
}