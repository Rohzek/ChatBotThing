using System;
using System.Collections.Generic;

namespace Chatbot
{
    /**
     * Object representation of the Sentence DB objects
     */
    public partial class Sentence
    {
        public int Id { get; set; }
        public string Sentence1 { get; set; } = null!;
        public int Used { get; set; }
    }
}
