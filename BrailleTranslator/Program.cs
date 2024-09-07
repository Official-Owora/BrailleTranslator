using System;
using System.Collections.Generic;

class BrailleTranslator
{
    static Dictionary<char, string> englishToBraille = new Dictionary<char, string>
    {
        {'a', "O....."}, {'b', "O.O..."}, {'c', "OO...."}, {'d', "OO.O.."}, {'e', "O..O.."},
        {'f', "OOO..."}, {'g', "OOOO.."}, {'h', "O.OO.."}, {'i', ".OO..."}, {'j', ".OOO.."},
        {'k', "O...O."}, {'l', "O.O.O."}, {'m', "OO..O."}, {'n', "OO.OO."}, {'o', "O..OO."},
        {'p', "OOO.O."}, {'q', "OOOOO."}, {'r', "O.OOO."}, {'s', ".OO.O."}, {'t', ".OOOO."},
        {'u', "O...OO"}, {'v', "O.O.OO"}, {'w', ".OOO.O"}, {'x', "OO..OO"}, {'y', "OO.OOO"},
        {'z', "O..OOO"}, {' ', "......"}, 
        {'1', "O....."}, {'2', "O.O..."}, {'3', "OO...."}, {'4', "OO.O.."}, {'5', "O..O.."},
        {'6', "OOO..."}, {'7', "OOOO.."}, {'8', "O.OO.."}, {'9', ".OO..."}, {'0', ".OOO.."},
        {'#', ".O.OOO"}, 
        {'^', ".....O"}  
    };

    static Dictionary<string, char> brailleToEnglish = new Dictionary<string, char>
    {
        {"O.....", 'a'}, {"O.O...", 'b'}, {"OO....", 'c'}, {"OO.O..", 'd'}, {"O..O..", 'e'},
        {"OOO...", 'f'}, {"OOOO..", 'g'}, {"O.OO..", 'h'}, {".OO...", 'i'}, {".OOO..", 'j'},
        {"O...O.", 'k'}, {"O.O.O.", 'l'}, {"OO..O.", 'm'}, {"OO.OO.", 'n'}, {"O..OO.", 'o'},
        {"OOO.O.", 'p'}, {"OOOOO.", 'q'}, {"O.OOO.", 'r'}, {".OO.O.", 's'}, {".OOOO.", 't'},
        {"O...OO", 'u'}, {"O.O.OO", 'v'}, {".OOO.O", 'w'}, {"OO..OO", 'x'}, {"OO.OOO", 'y'},
        {"O..OOO", 'z'}, {"......", ' '}, 
        {".O.OOO", '#'}, 
        {".....O", '^'}  
    };

    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide a string to translate.");
            return;
        }

        string input = args[0];
        string output = "";

        if (IsBraille(input))
        {
            output = BrailleToEnglish(input);
        }
        else
        {
            output = EnglishToBraille(input);
        }

        Console.WriteLine(output);
    }

    static bool IsBraille(string input)
    {
        return input.Contains("O") || input.Contains(".");
    }

    static string BrailleToEnglish(string braille)
    {
        string result = "";
        bool isCapital = false;
        bool isNumber = false;

        for (int i = 0; i < braille.Length; i += 6)
        {
            string symbol = braille.Substring(i, 6);

            if (symbol == ".....O")
            {
                isCapital = true;
                continue;
            }

            if (symbol == ".O.OOO")
            {
                isNumber = true;
                continue;
            }

            if (brailleToEnglish.ContainsKey(symbol))
            {
                char character = brailleToEnglish[symbol]; 

                if (isNumber && character >= 'a' && character <= 'j')
                {
                    character = (char)(character - 'a' + '1');
                }

                if (isCapital)
                {
                    character = char.ToUpper(character);
                    isCapital = false;
                }

                result += character;

                if (character == ' ')
                {
                    isNumber = false;
                }
            }
        }

        return result;
    }

    static string EnglishToBraille(string english)
    {
        string result = "";

        foreach (char character in english.ToLower())
        {
            if (char.IsUpper(character))
            {
                result += ".....O"; 
            }

            if (char.IsDigit(character))
            {
                result += ".O.OOO"; 
            }

            if (englishToBraille.ContainsKey(character))
            {
                result += englishToBraille[character];
            }
        }

        return result;
    }
}