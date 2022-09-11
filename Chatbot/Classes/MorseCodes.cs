using System;
using System.Collections.Generic;
using System.Text;

namespace Chatbot.Classes
{
    class MorseCodes
    {
        public static MorseCode Apostrophe = new MorseCode('\'', ".----.");
        public static MorseCode ParenthesisLeft = new MorseCode('(', "-.--.-");
        public static MorseCode ParenthesisRight = new MorseCode(')', "-.--.-");
        public static MorseCode Comma = new MorseCode(',', "--..--");
        public static MorseCode Dash = new MorseCode('-', "-....-");
        public static MorseCode Period = new MorseCode('.', ".-.-.-");
        public static MorseCode ForwardSlash = new MorseCode('/', "-..-.");
        public static MorseCode Zero = new MorseCode('0', "-----");
        public static MorseCode One = new MorseCode('1', ".----");
        public static MorseCode Two = new MorseCode('2', "..---");
        public static MorseCode Three = new MorseCode('3', "...--");
        public static MorseCode Four = new MorseCode('4', "....-");
        public static MorseCode Five = new MorseCode('5', ".....");
        public static MorseCode Six = new MorseCode('6', "-....");
        public static MorseCode Seven = new MorseCode('7', "--...");
        public static MorseCode Eight = new MorseCode('8', "---..");
        public static MorseCode Nine = new MorseCode('9', "----.");
        public static MorseCode Colon = new MorseCode(':', "---...");
        public static MorseCode SemiColon = new MorseCode(';', "-.-.-.");
        public static MorseCode QuestionMark = new MorseCode('?', "..--..");
        public static MorseCode A = new MorseCode('A', ".-");
        public static MorseCode B = new MorseCode('B', "-...");
        public static MorseCode C = new MorseCode('C', "-.-.");
        public static MorseCode D = new MorseCode('D', "-..");
        public static MorseCode E = new MorseCode('E', ".");
        public static MorseCode F = new MorseCode('F', "..-.");
        public static MorseCode G = new MorseCode('G', "--.");
        public static MorseCode H = new MorseCode('H', "....");
        public static MorseCode I = new MorseCode('I', "..");
        public static MorseCode J = new MorseCode('J', ".---");
        public static MorseCode K = new MorseCode('K', "-.-");
        public static MorseCode L = new MorseCode('L', ".-..");
        public static MorseCode M = new MorseCode('M', "--");
        public static MorseCode N = new MorseCode('N', "-.");
        public static MorseCode O = new MorseCode('O', "---");
        public static MorseCode P = new MorseCode('P', ".--.");
        public static MorseCode Q = new MorseCode('Q', "--.-");
        public static MorseCode R = new MorseCode('R', ".-.");
        public static MorseCode S = new MorseCode('S', "...");
        public static MorseCode T = new MorseCode('T', "-");
        public static MorseCode U = new MorseCode('U', "..-");
        public static MorseCode V = new MorseCode('V', "...-");
        public static MorseCode W = new MorseCode('W', ".--");
        public static MorseCode X = new MorseCode('X', "-..-");
        public static MorseCode Y = new MorseCode('Y', "-.--");
        public static MorseCode Z = new MorseCode('Z', "--..");
        public static MorseCode Underscore = new MorseCode('_', "..--.-");

        List<MorseCode> MorseCodesList = new List<MorseCode>
        {
            Apostrophe,
            ParenthesisLeft,
            ParenthesisRight,
            Comma,
            Dash,
            Period,
            ForwardSlash,
            One,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Colon,
            SemiColon,
            QuestionMark,
            A,
            B,
            C,
            D,
            E,
            F,
            G,
            H,
            I,
            J,
            K,
            L,
            M,
            N,
            O,
            P,
            Q,
            R,
            S,
            T,
            U,
            V,
            W,
            X,
            Y,
            Z,
            Underscore
        };
    }
}