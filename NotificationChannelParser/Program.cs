using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NotificationChannelParser
{
    public class Program
    {
        private static readonly HashSet<string> ValidChannels = new HashSet<string>
        {
            "BE",    // Backend
            "FE",    // Frontend
            "QA",    // Quality Assurance
            "URGENT" // Urgent notifications
        };

        public static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\nEnter notification title (or 'exit' to quit):");
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input) || input.ToLower() == "exit")
                    break;

                string channels = ParseNotificationChannels(input);
                Console.WriteLine(channels);
            }
        }

        public static string ParseNotificationChannels(string title)
        {
            if (string.IsNullOrEmpty(title))
                return "No channels found";

            var regex = new Regex(@"\[(.*?)\]");
            var matches = regex.Matches(title);

            var channels = matches
                .Select(m => m.Groups[1].Value.ToUpper()) // Convert to uppercase for comparison
                .Where(tag => ValidChannels.Contains(tag))
                .OrderBy(tag => tag) // Optional: Sort channels alphabetically
                .ToList();

            if (!channels.Any())
                return "No valid channels found";

            return $"Receive channels: {string.Join(", ", channels)}";
        }
    }

}