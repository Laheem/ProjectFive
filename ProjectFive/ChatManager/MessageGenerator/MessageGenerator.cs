using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFive.ChatManager.MessageGenerator
{
    static class MessageGenerator
    {

        public static String GenerateGenericMessage(String characterName, String message)
        {
            return $"{characterName} says: {message}";
        }
        public static String GenerateDoMessage(String playerName, String message)
        {
            return $"* {message} (({playerName}))";
        }

        public static String GenerateMeMessage(String playerName, String message)
        {
            return $"* {playerName} {message}";
        }

        public static String GenerateAttemptMessage(String playerName, string attemptedAction)
        {
            int outcome = new Random().Next(0, 2);

            string textOutcome = "failed";
            if (outcome == 0)
            {
                textOutcome = "succeeded";
            }

            return $"[ATTEMPT] {playerName} has attempted to {attemptedAction} and {textOutcome}.";
        }

        public static String GenerateFromPm(String senderName, string message) {
            return $"PM from {senderName}: {message}";
        }

        public static String GenerateToPm(String targetPlayer, string message)
        {
            return $"PM to {targetPlayer}: {message}";
        }

        public static String GenerateRPFromMessage(String senderName, String message)
        {
            return $"[RP from {senderName}]: {message}";
        }

        public static String GenerateRPToMessage(String targetPlayerName, String message)
        {
            return $"[RP to {targetPlayerName}]: {message}";
        }

        public static String GenerateWhisperAreaMessage(String sender, String target)
        {
            return $"* {sender} has whispered something to {target}";
        }

        public static String GenerateWhisperFromMessage(String senderName, String message)
        {
            return $"Whisper from {senderName}: {message}";
        }

        public static String GenerateWhisperToMessage(String targetPlayerName, String message)
        {
            return $"Whisper to {targetPlayerName}: {message}";
        }

        public static String GenerateShoutMessage(String targetPlayerName, String message)
        {
            return $"{targetPlayerName} shouts: {message}!";
        }

        public static String GenerateLowMessage(String targetPlayerName, String message)
        {
            return $"{targetPlayerName} says quietly: {message}";
        }

        public static String GenerateBMessage(String player, String message)
        {
            return $"(({player}: {message}))";
        }
    }
}
