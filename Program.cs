using System;
using NAudio.Wave;
using System.Collections.Generic;
using System.Threading;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        // Specify the path to the audio file
        string filePath = @"C:\Users\RC_Student_lab\source\repos\PROG 6221-PART 1\audio\welcome.wav";

        try
        {
            // Create an audio file reader to load the WAV file
            using (var audioFile = new AudioFileReader(filePath))
            using (var outputDevice = new WaveOutEvent())
            {
                // Initialize the player with the audio file
                outputDevice.Init(audioFile);
                // Play the audio file
                outputDevice.Play();

                // Notify the user that the audio is playing
                Console.WriteLine("Playing audio... Press any key to continue.");
                // Wait for the user to press a key before continuing
                Console.ReadKey();
            }
        }
        catch (Exception ex)
        {
            // Print the error message
            Console.WriteLine($"Error: {ex.Message}");
        }

        // Display ASCII art
        DisplayAsciiArt();

        // Welcome message
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("==============================================");
        Console.WriteLine("          Welcome to NETA GPT                ");
        Console.WriteLine("==============================================");
        Thread.Sleep(1000);

        Console.ResetColor();

        string userName;
        do
        {
            Console.Write("Please enter your name (at least 5 characters with a special character): ");
            userName = Console.ReadLine();
        } while (!IsValidName(userName));

        Console.WriteLine($"\nHello, {userName}! How can I assist you today?");

        // Dictionary containing questions and their corresponding responses
        var responses = new Dictionary<string, string>
        {
            { "how are you", "I'm just a program, but I'm here to help you!" },
            { "what's your purpose", "I'm here to provide information about cybersecurity, passwords, phishing, and safe browsing." },
            { "what can i ask you about", "You can ask me about password safety, phishing scams, malware, and social engineering." },
            { "what is phishing", "Phishing is a cyberattack where scammers trick you into revealing sensitive information, often through fake emails or websites." },
            { "how can i create a strong password", "Use a mix of uppercase, lowercase, numbers, and symbols. Avoid using common words or personal info." },
            { "what are safe browsing habits", "Avoid clicking unknown links, enable two-factor authentication, and always verify website URLs before entering personal info." },
            { "what is malware", "Malware is malicious software designed to harm, exploit, or otherwise compromise your computer or network." },
            { "what is social engineering", "Social engineering is a tactic used by hackers to manipulate individuals into divulging confidential information." }
        };

        // Display the questions that can be asked
        Console.WriteLine("\nHere are the questions you can ask:");
        foreach (var question in responses.Keys)
        {
            Console.WriteLine($"- {question}");
        }

        while (true)
        {
            Console.Write("\nYou: ");
            string userInput = Console.ReadLine()?.ToLower();

            if (string.IsNullOrWhiteSpace(userInput))
            {
                Respond("It looks like you didn't type anything. Try asking about cybersecurity, like 'What is phishing?'");
                continue;
            }

            if (responses.TryGetValue(userInput, out string response))
            {
                Respond(response);
            }
            else if (userInput == "exit")
            {
                Respond("Goodbye! Stay safe online!");
                break;
            }
            else
            {
                Respond("I didn't quite understand that. Could you rephrase? Try asking about phishing or password safety.");
            }
        }
    }

    static void DisplayAsciiArt()
    {
        Console.WriteLine(@"
     .-') _   ('-.   .-') _      ('-.                        _ (`-.  .-') _    
    ( OO ) )_(  OO) (  OO) )    ( OO ).-.                   ( (OO  )(  OO) )   
,--./ ,--,'(,------./     '._   / . --. /        ,----.    _.`     \/     '._  
|   \ |  |\ |  .---'|'--...__)  | \-.  \        '  .-./-')(__...--''|'--...__) 
|    \|  | )|  |    '--.  .--'.-'-'  |  |       |  |_( O- )|  /  | |'--.  .--' 
|  .     |/(|  '--.    |  |    \| |_.'  |       |  | .--, \|  |_.' |   |  |    
|  |\    |  |  .--'    |  |     |  .-.  |      (|  | '. (_/|  .___.'   |  |    
|  | \   |  |  `---.   |  |     |  | |  |       |  '--'  | |  |        |  |    
`--'  `--'  `------'   `--'     `--' `--'        `------'  `--'        `--'    
        ");
    }

    static void Respond(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        foreach (char letter in message)
        {
            Console.Write(letter);
            Thread.Sleep(50);
        }
        Console.WriteLine();
        Console.ResetColor();
    }

    // Method to validate the user's name
    static bool IsValidName(string name)
    {
        return name.Length >= 5 && Regex.IsMatch(name, @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");
    }
}