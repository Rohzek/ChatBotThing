namespace Chatbot.Classes
{
    class MorseCode
    {
        public MorseCode(char Character, string Representation) 
        {
            this.Character = Character;
            this.Representation = Representation;
        }

        char Character { get; set; }
        string Representation { get; set; }
    }
}
