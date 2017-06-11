﻿namespace MultiLoader.ConsoleFacade
{
    public class ConsoleArgs
    {
        public string SourceName { get; }
        public string SourceRequest { get; }
        public string SavePath { get; }
        public string ValidationMessage { get; }

        private ConsoleArgs(string sourceName, string sourceRequest, string savePath)
        {
            SourceName = sourceName;
            SourceRequest = sourceRequest;
            SavePath = savePath;
        }

        private ConsoleArgs(string validationMessage) =>
            ValidationMessage = validationMessage;
        

        public static bool ParseArgs(string[] args, out ConsoleArgs consoleArgsResult)
        {
            if (args.Length != 3)
            {
                consoleArgsResult = new ConsoleArgs("Request should be: \n" + 
                                                    "[danbooru] [searchRequest] [savePath]\n" + 
                                                    "[2ch] [board_thread] [savePath]\n" +
                                                    "[anonib] [board_thread] [savePath]\n" +
                                                    "[ImgurSource] [albumId] [savePath]\n");
                return false;
            }

            consoleArgsResult = new ConsoleArgs(args[0], args[1], args[2]);
            return true; 
        }
    }
}