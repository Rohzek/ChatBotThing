using System;
using System.Collections.Generic;

namespace Chatbot
{
    /**
     * Object representation of the Association DB objects
     */
    public partial class Association
    {
        public int Id { get; set; }
        public int WordId { get; set; }
        public int SentenceId { get; set; }
        public double Weight { get; set; }
    }
}
