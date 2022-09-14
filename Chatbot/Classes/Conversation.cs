using Discord;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Chatbot.Classes
{
    public static class Conversation
    {
        static string MentionPattern = "[\\s+]?<@\\d+>[\\s+]?", WordPattern = "[\\w'-]+";
        static string LastSaidSentence = "";

        static Random random = new Random();

        async public static Task<Task> Input(DiscordSocketClient client, SocketUserMessage msg)
        {
            // Read input and respond accordingly
            var sender = msg.Author;
            var channel = msg.Channel;
            var message = msg.Content.ToString();

            var Responses = new List<string>(); // Allocates memory for our responses list
            var Response = ""; // Allocates memory for our chosen response

            /**
             * Because we use mentioning the bot to trigger the conversation, we need to strip out the mention
             * An incoming message will look something like:
             * <@429746839944822794> Hello
             * Where everything between < and > is the unique Discord userID of the bot
             * The regex pattern includes spaces before and after the mention, to handle the mention being the first
             * and the last thing in the message, just in case, even though the main code only checks for it at begining.
             */
            message = Regex.Replace(message, MentionPattern, "");

            // At this point, message is just the text, without the userID
            try
            {
                // Determine the proper response
                // The question/Google thing was first, but I'm not entirely sure how it works so skipping for now
                if (Regex.IsMatch(message.ToLower(), "\\bhow are you\\b"))
                {
                    Responses = new List<string> { "happy.", "sad.", "excited.", "creative.", "rather good about myself today." };
                    Response = $"I'm feeling {Responses[random.Next(Responses.Count)]}";
                }

                else if (Regex.IsMatch(message.ToLower(), "\\bhi\\b") || Regex.IsMatch(message.ToLower(), "\\bhello\\b"))
                {
                    Responses = new List<string> { "Hello", "Howdy", "Hi", "Greetings", "Hi, let's talk.", "Hello there." };
                    Response = Responses[random.Next(Responses.Count)];
                }

                else if (Regex.IsMatch(message.ToLower(), "\\byes\\b"))
                {
                    Responses = new List<string> { "Agreed.", "Indeed.", "Okay.", "I agree.", "Absolutely.", "Only if you're sure.", "Really?" };
                    Response = Responses[random.Next(Responses.Count)];
                }

                else if (Regex.IsMatch(message.ToLower(), "\\bno\\b"))
                {
                    Responses = new List<string> { "Alright then.", "Okay.", "No?", "Okay then.", "Are you sure about that?" };
                    Response = Responses[random.Next(Responses.Count)];
                }

                else if (Regex.IsMatch(message.ToLower(), "\\bwhy not\\b"))
                {
                    Responses = new List<string> { "Why not indeed.", "Maybe.", "Okay." };
                    Response = Responses[random.Next(Responses.Count)];
                }

                else if (Regex.IsMatch(message.ToLower(), "\\bthank\\b"))
                {
                    Responses = new List<string> { "No problem.", "You're Welcome.", "My Pleasure.", "Anytime.", "No trouble at all.", "Happy to help.", "Not a problem." };
                    Response = Responses[random.Next(Responses.Count)];
                }

                else if (Regex.IsMatch(message.ToLower(), "\\bgood\\b"))
                {
                    Responses = new List<string> { "Agreed.", "Indeed.", "Okay.", "I agree.", "Absolutely.", "I'm glad." };
                    Response = Responses[random.Next(Responses.Count)];
                }

                else if (Regex.IsMatch(message.ToLower(), "\\bnice\\b"))
                {
                    Responses = new List<string> { "Agreed.", "Indeed.", "Okay.", "I agree.", "Absolutely.", "I thought so.", "Yes.", "It is indeed.", "It is, yes." };
                    Response = Responses[random.Next(Responses.Count)];
                }

                else if (Regex.IsMatch(message.ToLower(), "\\bwhy are you\\b"))
                {
                    Responses = new List<string> { "Not sure.", "Ask me later.", "Just because.", "That's just the way it is.", "I'll explain a bit more later." };
                    Response = Responses[random.Next(Responses.Count)];
                }

                else if (Regex.IsMatch(message.ToLower(), "\\bsorry\\b"))
                {
                    Responses = new List<string> { "Don't worry.", "That's okay.", "It could be better.", "No need to worry." };
                    Response = Responses[random.Next(Responses.Count)];
                }

                else if (Regex.IsMatch(message.ToLower(), "\\bproblem\\b") && !Regex.IsMatch(message.ToLower(), "\\bno problem\\b"))
                {
                    Response = "A problem shared is a problem two people now have.";
                }

                else if (Regex.IsMatch(message.ToLower(), "\\bjoke\\b"))
                {
                    // Store individual user's joke count somewhere and recall it here
                    int Repeat = 0;
                    var JokeRepeat = new List<string> { "Let me think of another joke for you...", "I have another joke to tell...", "Here's another one for you...", "Fancy another joke then... ", "My other joke wasn't funny enough? Here's another...", "Here's another joke you'll like..." };
                    Responses = new List<string> { "This random guy threw a block of cheese at me the other day. How dairy!", "I bought a broken hoover the other day. It just sits there gathering dust.", "Did you hear the one about the letter that got posted without a stamp? You wouldn't get it.", "Windmills, I'm a big fan.", "Do they make 'Do Not Touch' signs in Braille?" };
                    Response = $"{(Repeat > 0 ? JokeRepeat[random.Next(JokeRepeat.Count)] : "")}{Responses[random.Next(Responses.Count)]}";
                    // Count should go up per user too
                }

                else if (Regex.IsMatch(message.ToLower(), "\\bfact\\b"))
                {
                    // Store individual user's fact count somewhere and recall it here
                    int Repeat = 0;
                    var FactRepeat = new List<string> { "Here's another one for you... ", "Let me think of another fact for you. ", "You like your facts, here's another...", "Here's another fact that you'll like..." };
                    Responses = new List<string> { "King Henry VIII slept with a gigantic axe beside him.", "Polar bears can eat as many as 86 penguins in a single sitting. (If they lived in the same place)", "An eagle can kill a young deer and fly away with it.", "If Pinokio says 'My Noes Will Grow Now', it would cause a paradox.", "During your lifetime, you will produce enough saliva to fill two swimming pools.", "The person who invented the Frisbee was cremated and made into frisbees after he died.", "Billy goats urinate on their own heads to smell more attractive to females.", "Cherophobia is the fear of fun.", "The average woman uses her height in lipstick every 5 years.", "A flock of crows is known as a murder.", "When hippos are upset, their sweat turns red.", "Pteronophobia is the fear of being tickled by feathers.", "Banging your head against a wall burns 150 calories an hour." };
                    Response = $"{(Repeat > 0 ? FactRepeat[random.Next(FactRepeat.Count)] : "")}{Responses[random.Next(Responses.Count)]}";
                    // Count should go up per user too
                }

                else if (Regex.IsMatch(message.ToLower(), "\\bstory\\b"))
                {
                    // Store individual user's story count somewhere and recall it here
                    int Repeat = 0;
                    var StoryRepeat = new List<string> { "Fancied another one of my short stories, eh? Well here goes... ", "Here's another short story... ", "I'm glad you've asked for another story. Here you go... " };
                    Responses = new List<string> { "'Wrong number' said a familiar voice.", "Goodbye Mission Control, thanks for all your help.", "'But I buried you with my own hands', exclaimed the man.", "He looked in the mirror and saw his own reflection blink.", "They delivered the mannequins in bubble wrap. From the main room I begin to hear popping.", "She looked out of her windown and noticed all the stars had gone out.", "The camp trucked us to a greasy river where boys peed in the swim and shot slings at things they could not see. We had arrived.", "I looked out my window and could see someone staring at me. When I went out I could see him standing in my window watching me, and I just stood there and stared back.", "'Don't forget about me.' 'I won't.' breathed the coroner to the corpse.", "It's scary what a smile can hide. Even your closest friend can be hiding the most deepest secrets.", "The devil doesn't come dressed in a red cape and pointy horns. He comes as everything you've ever wished for.", "She always was funny, but one day she stopped telling jokes. That's because it isn't always funny with a knife in your back.", "Her toys are cleaner today. They always clean themselves the night she disappeared.", "I want to go to sleep. But the woman on the ceiling won't let me.", "The weather was a windy today. And I thought I was bad with gas.", "Finding a dead body buried in my garden was terrifying. Realizing it was mine was even worse.", "She thought he would use the breaks. He thought she would cross faster.", "I took out the paper from my pocket and unfolded it. It was blank save for the single tear stain I allowed myself.", "Today, I had my friend for lunch. She tasted very bland.", "I blink and groan in annoyance as I lose another round of our staring contest. His dead soulless eyes remained unblinking no matter what.", "December 31st, 1999, 11:59 PM, and I'm playing my game, not concerned about Y2K. Then my batteries die.", "I don't understand why the kids in my neighborhood are afraid to go into the woods at night. I'm the one that has to walk all the way back home alone.", "While spying on my older sister's sleepover, I saw her playing with a Ouija board and saying 'Jeremy, can you hear us'. I'm Jeremy.", "He came quickly, but saw the sword point flashing too late. He was conquered.", "She did not bother when her purse was snatched. When they opened it, there was only a small piece of paper with choicest abuses.", "I went on a hiking trip last night. I saw a wolf but didn't know they could walk on two legs.", "I don't like to go outside. But the puppet strings leave me no choice." };
                    Response = $"{(Repeat > 0 ? StoryRepeat[random.Next(StoryRepeat.Count)] : "Here's a very short story for you: ")}{Responses[random.Next(Responses.Count)]}";
                    // Count should go up per user too
                }

                else if (Regex.IsMatch(message.ToLower(), "\\bfunny\\b"))
                {
                    Responses = new List<string> { "I thought it was funny.", "It made me laugh." };
                    Response = Responses[random.Next(Responses.Count)];
                }

                else if (Regex.IsMatch(message.ToLower(), "\\bwho made you\\b"))
                {
                    Responses = new List<string> { "I was written by Destiny Maldonado.", "Destiny Maldonado, she's really nice.", "Someone who shall be called Destiny Maldonado.", "A nice lady by the name of Destiny Maldonado." };
                    Response = Responses[random.Next(Responses.Count)];
                }

                else if (Regex.IsMatch(message.ToLower(), "\\byou made by\\b"))
                {
                    Responses = new List<string> { "I was written by Destiny Maldonado.", "Destiny Maldonado, she's a nice girl.", "Someone who shall be called Destiny Maldonado.", "Destiny Maldonado. She's a nice guy. ;P" };
                    Response = Responses[random.Next(Responses.Count)];
                }

                else if (Regex.IsMatch(message.ToLower(), "\\bwho wrote you\\b"))
                {
                    Responses = new List<string> { "I was written by Red.", "Red, she's a nice chap.", "Someone who shall be called Red.", "A nice lass by the name of Red." };
                    Response = Responses[random.Next(Responses.Count)];
                }

                else if (Regex.IsMatch(message.ToLower(), "\\bgood\\b"))
                {
                    Responses = new List<string> { "I thought it was good.", "It was good, wasn't it?", "Indeed." };
                    Response = Responses[random.Next(Responses.Count)];
                }

                // This one seems to be broke AF
                else if (Regex.IsMatch(message.ToLower(), "\\bok\\b") || Regex.IsMatch(message.ToLower(), "\\bokay\\b"))
                {
                    Responses = new List<string> { "Okay.", "Oh, okay." };
                    Response = Responses[random.Next(Responses.Count)];
                }

                else if (Regex.IsMatch(message.ToLower(), "\\byou called\\b"))
                {
                    Responses = new List<string> { "My name is Puca.", "They call me Puca.", "I'm not sure why but they call me Puca.", "P-U-C-A is the name." };
                    Response = Responses[random.Next(Responses.Count)];
                }

                else if (Regex.IsMatch(message.ToLower(), "\\byour name\\b"))
                {
                    Responses = new List<string> { "My name is Puca.", "They call me Puca.", "I'm not sure why but they call me Puca.", "P-U-C-A is the name." };
                    Response = Responses[random.Next(Responses.Count)];
                }

                // News predict? Or something here

                // Hangman here but also on top probably

                // Word association bits here Lines: 383 - 410

                // H(uman) = user input? B(ot) = Bot's words?
                else
                {
                    // This check should probably end up being per user eventually
                    if (LastSaidSentence.Length == 0)
                    {
                        // If nothing previously has been said, repeat the last phrase given
                        LastSaidSentence = message;
                        Response = $"{message}";
                    }
                    else // Figure out what to say back
                    {
                        int Words_Length = 0;
                        Double Weight = 0;
                        Array Words;

                        // 384: Split the bot's last text string into individual words and store into array
                        Words = Regex.Matches(LastSaidSentence, WordPattern).Cast<Match>().Select(m => m.Value).ToArray();

                        // 385: Total length of all words in the array added together?
                        foreach (string word in Words)
                        {
                            Words_Length += word.Length;
                        }
                        // 386: Map all of the words individually to the last user input sentence?
                        // User's sentence => Bot's last words?
                        var Sentence = Program.DB.Sentences.Where(w => w.Sentence1.ToLower().Equals(message.ToLower())).FirstOrDefault();
                        var CheckSentence = Program.DB.Sentences.OrderByDescending(s => s.Id).FirstOrDefault();
                        var Num = 0;

                        if (CheckSentence != null) 
                        {
                            Num = CheckSentence.Id;
                        }

                        if (Sentence == null)
                        {
                            Sentence = new Sentence();
                            
                            Sentence.Id = Num + 1; // Adds one to the current used ID
                            Sentence.Sentence1 = message;
                            Sentence.Used = 0;
                            Program.DB.Add(Sentence);
                        }

                        // 389: length of each word divided by length of total words = each word's weight?
                        var CheckWord = Program.DB.Words.OrderByDescending(w => w.Id).FirstOrDefault();
                        Num = 0;

                        if (CheckWord != null) 
                        {
                            Num = CheckWord.Id;
                        }

                        foreach (string Word in Words)
                        {
                            Weight = (Double)Math.Sqrt((Double)Word.Length / (Double)Words_Length);
                            var DBW = Program.DB.Words.Where(w => w.Word1.ToLower().Equals(Word.ToLower())).FirstOrDefault();

                            if (DBW == null) 
                            {
                                DBW = new Word();
                                DBW.Id = Num += 1; // Adds one to the current used ID
                                DBW.Word1 = Word;

                                var Assoc = new Association();
                                Assoc.SentenceId = Sentence.Id;
                                Assoc.WordId = DBW.Id;
                                Assoc.Weight = Weight;

                                Program.DB.Words.Add(DBW);
                                Program.DB.Associations.Add(Assoc);
                            }
                            //Console.WriteLine($"Bot check first; Word is: {Word}. The calculation is {Word.Length}/{Words_Length} to get {Weight}");
                        }

                        // Do the same stuff with users below:
                        // 394: Split user's last text string into individual words and store into an array
                        Words = Regex.Matches(message, WordPattern).Cast<Match>().Select(m => m.Value).ToArray();

                        // 395: Total length of all words in the array added together?
                        foreach (string word in Words)
                        {
                            Words_Length += word.Length;
                        }

                        // 396: length of each word divided by total length of all words = word weight?
                        foreach (string Word in Words)
                        {
                            Weight = (Double)Math.Sqrt((Double)Word.Length / (Double)Words_Length);
                            //Console.WriteLine($"User check second; Word is: {Word}. The calculation is {Word.Length}/{Words_Length} to get {Weight}");
                        }

                        // 400: Pull a sentence that has a similar weight structure out of memory?

                        /**
                         * Not sure about this one, but this is what we need to work on next
                         * 
                         * 
                         */

                        // 405: Otherwise pull a random stored sentence that's the least used?
                        Sentence = Program.DB.Sentences.Where(s => s.Used == Program.DB.Sentences.Min(x => x.Used)).FirstOrDefault();

                        // 408: Update used again count on whichever sentence was selected
                        if (Sentence != null)
                        {
                            Sentence.Used = Sentence.Used += 1;
                            Response = Sentence.Sentence1;
                        }
                        else 
                        {
                            Response = $"{message}";
                        }
                    }
                }
                LastSaidSentence = Response;
                await Program.DB.SaveChangesAsync();

                // Actually respond
                await msg.ReplyAsync($"{Response}");
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error caught in conversation: {ex.Message}");
                Console.WriteLine($"Error trace: {ex.StackTrace}");
                Console.WriteLine($"Message text says: {message}");

                await msg.ReplyAsync($"Sorry, I spaced out for a second. Can you repeat that please?");
            }

            return Task.CompletedTask;
        }
    }
}
