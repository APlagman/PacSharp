using System.Collections.Generic;
using System.IO;
using System.Media;

namespace PacSharpApp
{
    class SoundHandler
    {
        private IDictionary<Stream, SoundPlayer> players = new Dictionary<Stream, SoundPlayer>();

        public void Play(Stream resourceLocation)
        {
            new SoundPlayer()
            {
                Stream = resourceLocation
            }.Play();
        }

        public void Loop(Stream resourceLocation)
        {
            if (!players.ContainsKey(resourceLocation))
                players[resourceLocation] = new SoundPlayer() { Stream = resourceLocation };
            players[resourceLocation].PlayLooping();
        }

        public void Stop(Stream resource)
        {
            if (players.ContainsKey(resource))
                players[resource].Stop();
        }
    }
}