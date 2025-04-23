using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Media;
using System.IO;

namespace ChatBot
{
    class Program
    {

        const int TypingDelay = 25;

        static void Main(string[] args)
        {
           
            Console.Title = "Cyber Security ChatBot";
            
            Console.Clear();

           

            // calls the ascii header to be displayed
            DisplayHeader();

            // getting the name of the user
            string userName = GetUserName();

            // Displays the welcome screen with the users name
            WelcomeUser(userName);

            // enters the loop to start the chat bot
            ChatLoop();
        }

        #region Display & Welcome

        // method for the header 
        static void DisplayHeader()
        {

            //plays WAV audio file
            SoundPlayer s = new SoundPlayer(@"C:\Users\shaun\source\repos\ChatBot\VoiceRecPOE.wav");
             s.Play();

            string asciArt = @"C:\Users\shaun\source\repos\ChatBot\AsciiCyberSec.txt";
            string path = File.ReadAllText(asciArt);
            Console.WriteLine(path);
            Console.ResetColor();
        }

    
        
        static void WelcomeUser(string userName)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string('-', 80));
            TypeWriteLine($"Hello {userName}, welcome to the Cyber Security ChatBot!", TypingDelay);
            TypeWriteLine("Your personal assistant for cybersecurity tips, safe practices, and quizzes, please feel free to ask any and all questions.", TypingDelay);
            Console.WriteLine(new string('-', 80));
            Console.WriteLine();
            Console.ResetColor();
        }

      
        // method to create a conversation feel
        // creates a delay of the answers by using .sleep and delay timer set earlier
        static void TypeWriteLine(string text, int delay)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }

        #endregion

        #region Input Validation

     
        // method to get users name while having input validation 
        static string GetUserName()
        {
            string name;
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Please enter your name: ");
                name = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(name) || IsNumeric(name))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Your name cannot be empty or numbers only. Please try again.");
                }
                else
                {
                    break;
                }
            } while (true);
            Console.ResetColor();
            return name;
        }

       
        // method used for validation in getting users name , must be a name not numeric
        static bool IsNumeric(string input)
        {
            foreach (char c in input)
                if (!char.IsDigit(c)) return false;
            return true;
        }

      // method making use of isnumeric method ensuring the users data isnt blank or numeric
        static bool ValidateUserInput(string input)
        {
            return !string.IsNullOrWhiteSpace(input) && !IsNumeric(input);
        }

        #endregion

        #region Chat Loop

        static void ChatLoop()
        {
           // creates the message for if the user wants to exit 
            Console.ForegroundColor = ConsoleColor.Magenta;
            TypeWriteLine("Type 'exit' at any time to end the conversation.", TypingDelay);
            Console.ResetColor();
            Console.WriteLine();

            while (true)
            {
               // asking for the users input - helps create conversation feel 
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("You: ");
                string userInput = Console.ReadLine()?.Trim() ?? "";

                if (userInput.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    TypeWriteLine("Goodbye! thanks for chatting, I really enjoyed chatting with you, Stay safe and secure, always remember your tips.", TypingDelay);
                    Console.ResetColor(); 
                    break;
                }

                // validiaton for unexpeted inputs, lets the user know and asks for another input
                if (!ValidateUserInput(userInput))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    TypeWriteLine("I didn't quite understand that please rephrase.", TypingDelay);
                    Console.ResetColor();
                    continue;
                }

                ProcessQuery(userInput);
            }
        }

       
        // deals with all user input and what to return to the user
        static void ProcessQuery(string query)
        {
            string lower = query.ToLower();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(new string('-', 80));
            Console.ResetColor();

            if (lower.Contains("how are you"))
                Respond("I'm doing great, thank you for asking!I am Herefor you to provide cybersecurity tips, guidance, and a quiz to test you!");
            else if (lower.Contains("what's your purpose") || lower.Contains("your purpose"))
                Respond("My purpose is to make you feel safer online, you can ask me many questions and I happy to help where I can.");
            else if (lower.Contains("what can i ask you"))
                Respond("You can ask me about:\n" +
                        " - Password Safety\n" +
                        " - Phishing\n" +
                        " - Safe Browsing\n" +
                        " - Two-Factor Authentication\n" +
                        " - Encryption Basics\n" +
                        " - VPN Usage\n" +
                        " - Malware Prevention\n" +
                        " - Social Engineering\n" +
                        " - Patch Management\n" +
                        " - Secure Coding\n" +
                        " - Quiz (type 'quiz')");
            else if (lower.Contains("password safety"))
                Respond("Password Safety: Use a mix of upper/lowercase, digits, and symbols; avoid reuse; consider a password manager" +
                    "this creates and stores more complex passwords for you.");
            else if (lower.Contains("phishing"))
                Respond("Phishing: Verify sender addresses, hover before clicking ensure its who you think, and never share credentials via email.");
            else if (lower.Contains("safe browsing"))
                Respond("Safe Browsing: Stick to HTTPS sites, HTTPS is a secure connection, keep your browser updated, and avoid suspicious downloads.");
            else if (lower.Contains("two-factor") || lower.Contains("2fa"))
                Respond("Two-Factor Authentication: Adds a second verification step (SMS, app code, hardware token) to secure your accounts" +
                    "this will make sure no one can acess your acc without your knowledge.");
            else if (lower.Contains("encryption"))
                Respond("Encryption Basics: Protects data by converting it into unreadable ciphertext—ensure end-to-end or disk encryption as needed.");
            else if (lower.Contains("vpn"))
                Respond("VPN Usage: Encrypts your internet traffic and masks your IP—use a trusted VPN provider on public Wi-Fi.");
            else if (lower.Contains("malware"))
                Respond("Malware Prevention: Keep antivirus updated, avoid untrusted attachments, and scan downloads before opening" +
                    "make sure you have a anti-virus software that runs frequent scans of your machine.");
            else if (lower.Contains("social engineering"))
                Respond("Social Engineering: Attackers manipulate you into revealing info—stay skeptical of unsolicited requests and verify identities" +
                    "never share passwords, and if unsure, no matter what verify details.");
            else if (lower.Contains("patch") || lower.Contains("update"))
                Respond("Patch Management: Regularly install OS and software updates to fix security vulnerabilities.");
            else if (lower.Contains("secure coding"))
                Respond("Secure Coding: Validate input, use parameterized queries, and handle errors safely to prevent injections and leaks.");
            else if (lower.Contains("quiz"))
                Quiz.StartQuiz();
            else
                Respond("I didn't quite understand that please rephrase.");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(new string('-', 80));
            Console.ResetColor();
            Console.WriteLine();
        }

        // method for all responses of the bot
        //uses same color and delay every time
      
        static void Respond(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            TypeWriteLine("ChatBot: " + message, TypingDelay);
            Console.ResetColor();
        }

        #endregion
    }
}


