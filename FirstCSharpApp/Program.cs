using System;

static class Program
{
    static void Main()
    {
        string[] phrase = Console.ReadLine().ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        byte numberOfValidWords = Convert.ToByte(Console.ReadLine());
        string[] validWords = new string[numberOfValidWords];
        string[] matches = new string[phrase.Length];
        bool textCorect = true;

        for (byte i = 0; i < numberOfValidWords; i++)
        {
            validWords[i] = Console.ReadLine();
        }

        for (byte i = 0; i < phrase.Length; i++)
        {
            int exactMatch = Array.IndexOf(validWords, phrase[i]);
            if (exactMatch != -1)
            {
                matches[i] = validWords[exactMatch];
                continue;
            }

            matches[i] = Match(phrase[i], validWords, out bool fullMatch);
            if (!fullMatch)
            {
                textCorect = false;
            }
        }

        if (textCorect)
        {
            Console.WriteLine("Text corect!");
            return;
        }

        for (byte i = 0; i < phrase.Length; i++)
        {
            if (phrase[i] == matches[i])
            {
                continue;
            }

            if (matches[i].Length == 0)
            {
                Console.WriteLine($"{phrase[i]}: (nu sunt sugestii)");
                continue;
            }

            Console.WriteLine($"{phrase[i]}: {matches[i]}");
        }
    }

    private static string Match(string word, string[] validWords, out bool fullMatch)
    {
        string result = "";
        sbyte matchIndex = (sbyte)Array.IndexOf(validWords, word);

        if (matchIndex != -1)
        {
            fullMatch = true;
            return word;
        }

        fullMatch = false;

        for (byte i = 0; i <= word.Length; i++)
        {
            string switchedLetters;
            string removedLetter;

            if (i + 1 < word.Length)
            {
                switchedLetters = SwitchLetters(word, i);
                IsValid(validWords, switchedLetters, ref result);
            }

            if (i < word.Length)
            {
                removedLetter = word.Remove(i, 1);
                IsValid(validWords, removedLetter, ref result);
            }

            for (char j = 'a'; j <= 'z'; j++)
            {
                string addedLetter = word;
                addedLetter = addedLetter.Insert(i, j.ToString());
                IsValid(validWords, addedLetter, ref result);
                if (i == word.Length)
                {
                    continue;
                }

                string replacedLetter = word.Remove(i, 1);
                replacedLetter = replacedLetter.Insert(i, j.ToString());
                IsValid(validWords, replacedLetter, ref result);
            }
        }

        return result.TrimEnd();
    }

    private static void IsValid(string[] validWords, string guess, ref string result)
    {
        if (Array.IndexOf(validWords, guess) == -1 || result.Contains(guess))
        {
            return;
        }

        result += guess + " ";
    }

    private static string SwitchLetters(string word, byte i)
    {
        const byte removedCharacters = 2;
        string insertion = word[i + 1].ToString() + word[i].ToString();
        return word.Remove(i, removedCharacters).Insert(i, insertion);
    }
}