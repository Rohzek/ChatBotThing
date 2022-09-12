namespace Chatbot.Classes
{
    // A basic wrapper class for making the morse code entries objects
    class MorseCode
    {
        public MorseCode(char Character, string Representation)
        {
            this.Character = Character; // The letter E.G.: A, B, C etc
            this.Representation = Representation; // The morse code equivalent E.G. ...---...
        }

        char Character { get; set; }
        string Representation { get; set; }
    }
}