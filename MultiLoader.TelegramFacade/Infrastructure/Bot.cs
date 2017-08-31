﻿using Telegram.Bot;

namespace MultiLoader.TelegramFacade.Infrastructure
{
    public static class Bot
    {
        public static TelegramBotClient Api { get; private set; }

        public static void Init(string token) =>  Api = new TelegramBotClient(token);
            
    }
}
