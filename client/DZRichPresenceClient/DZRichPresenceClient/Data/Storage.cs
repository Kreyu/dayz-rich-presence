using System;

namespace DZRichPresenceClient.Data
{
    public static class Storage
    {
        public static class LargeImageTexts
        {
            private static Random Rand = new Random();

            private static readonly LargeImageText[] texts =
            {
                new LargeImageText("Months, not years"),
                new LargeImageText("Would you look at that"),
                new LargeImageText("Who's shooting in cherno?"),
                new LargeImageText("Heyy hey, I'm friendly"),
                new LargeImageText("Beans before friends"),
                new LargeImageText("Loot spawned for you"),
                new LargeImageText("/scream"),
                new LargeImageText("Party at Balota"),
                new LargeImageText("Connection with the host has been lost"),
                new LargeImageText("Rotten kiwis everywhere"),
                new LargeImageText("Yikees"),
                new LargeImageText("Hitreg supported"),
                new LargeImageText("Oh boy, that wasn't a desync"),
                new LargeImageText("*licks the battery*"),
                new LargeImageText("I have a funny taste in my mouth"),
                new LargeImageText("My stomach grumbled violently"),
                new LargeImageText("Battle Royale included"),
                new LargeImageText("That's a lot of damage"),
            };

            public static LargeImageText[] GetAll()
            {
                return texts;
            }

            public static LargeImageText GetRandom()
            {
                return texts[Rand.Next(0, texts.Length)];
            }
        }
    }

    public struct LargeImageText
    {
        public string value;

        public LargeImageText(string val)
        {
            value = val;
        }
    }
}
