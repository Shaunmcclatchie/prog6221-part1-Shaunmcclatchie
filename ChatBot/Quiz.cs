using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatBot
{
    public static class Quiz
    {
        const int TypingDelay = 25;

        // the begging of the quiz if called
        public static void StartQuiz()
        {
            bool passed = false;
            while (!passed)
            {
                // variables for quiz
                int score = 0;
                var questions = GetQuestions();

                // quiz title and colors set 
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                TypeWriteLine("\n========== Cyber Security Quiz ==========\n", TypingDelay);
                Console.ResetColor();

                // loop created to loop all questions
                for (int i = 0; i < questions.Count; i++)
                {
                    var q = questions[i];
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    TypeWriteLine($"Question {i + 1}: {q.QuestionText}", TypingDelay);
                    Console.ResetColor();

                    char label = 'A';
                    foreach (var opt in q.Options)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"   {label}. {opt}");
                        label++;
                    }

                    // gets the users input and compares if correct
                    char answer = GetValidAnswer();
                    if (char.ToUpper(answer) == char.ToUpper(q.CorrectAnswer))
                        score++;

                    Console.WriteLine();
                }

                // print to the user once the quiz is done showing result
                Console.ForegroundColor = ConsoleColor.Magenta;
                TypeWriteLine("========== Quiz Result ==========", TypingDelay);
                Console.ResetColor();

                // if statements to handle their score and what happens based on that

                if (score <= 2)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    TypeWriteLine($"\nYou scored {score} of 5. You did not pass. Try again.", TypingDelay);
                    Console.ResetColor();
                    PromptToContinue();
                }
                else if (score == 3)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    TypeWriteLine($"\nYou scored 3 of 5. You passed!", TypingDelay);
                    Console.ResetColor();
                    passed = true;
                }
                else // 4 or 5
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    TypeWriteLine($"\nCongratulations! {score}/5 – you earned a Distinction! Amazing work", TypingDelay);
                    Console.ResetColor();
                    passed = true;
                }
            }
        }

        // Defines the 5 quiz questions
        private static List<QuizQuestion> GetQuestions() => new List<QuizQuestion>
    {
        new QuizQuestion {
            QuestionText = "Good password practice?",
            Options = new List<string> {
                "Use birthdate",
                "Mix letters, numbers, symbols",
                "Repeat a simple word",
                "Use common words"
            },
            CorrectAnswer = 'B'
        },
        new QuizQuestion {
            QuestionText = "Phishing email indicator?",
            Options = new List<string> {
                "Misspelled sender domain",
                "Verified domain, good grammar",
                "Known IT dept. email",
                "Regular newsletter"
            },
            CorrectAnswer = 'A'
        },
        new QuizQuestion {
            QuestionText = "HTTPS means?",
            Options = new List<string> {
                "Site is outdated",
                "Encrypted communication",
                "No transactions allowed",
                "Virus‑free site"
            },
            CorrectAnswer = 'B'
        },
        new QuizQuestion {
            QuestionText = "What is a password manager?",
            Options = new List<string> {
                "Tool to generate & store passwords",
                "Auto‑changes passwords daily",
                "Tracks browsing activities",
                "Manages file permissions"
            },
            CorrectAnswer = 'A'
        },
        new QuizQuestion {
            QuestionText = "Safe practice to avoid phishing?",
            Options = new List<string> {
                "Click all unsolicited links",
                "Verify senders & URLs",
                "Share passwords with colleagues",
                "Ignore software updates"
            },
            CorrectAnswer = 'B'
        }
    };

        // method to validate wether the answer is correct or not
        private static char GetValidAnswer()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Your answer (A-D): ");
                var input = Console.ReadLine()?.Trim();

                if (!string.IsNullOrEmpty(input) && input.Length == 1 &&
                    "ABCDabcd".Contains(input))
                {
                    Console.ResetColor();
                    return input[0];
                }

                Console.ForegroundColor = ConsoleColor.Red;
                TypeWriteLine("Invalid. Enter A, B, C or D.", TypingDelay);
                Console.ResetColor();
            }
        }

        // Pauses before retrying again to help flow
        private static void PromptToContinue()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            TypeWriteLine("Press any key to retry...", TypingDelay);
            Console.ResetColor();
            Console.ReadKey();
        }

        // creates conversational feel with delayed typing effects
        private static void TypeWrite(string text, int delay)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
        }

        private static void TypeWriteLine(string text, int delay)
        {
            TypeWrite(text, delay);
            Console.WriteLine();
        }

        
        private class QuizQuestion
        {
            public string QuestionText { get; set; }
            public List<string> Options { get; set; }
            public char CorrectAnswer { get; set; }
        }
    }
}