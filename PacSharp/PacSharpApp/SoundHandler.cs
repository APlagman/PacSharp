using System;
using System.Collections.Generic;
using CSCore.CoreAudioAPI;
using CSCore.SoundOut;

/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp
{
    class SoundHandler : IDisposable
    {
        enum PlayMode { Play, Loop, Stop, Continue };

        private IDictionary<string, MusicPlayer> players = new Dictionary<string, MusicPlayer>();

        public bool Disabled { get; set; } = false;

        public void Play(string resourceLocation, bool loop, bool stopCurrent = false)
        {
            if (stopCurrent && players.ContainsKey(resourceLocation))
                players[resourceLocation].Stop();
            if (!players.ContainsKey(resourceLocation))
            {
                players[resourceLocation] = new MusicPlayer();
                players[resourceLocation].Open(resourceLocation, new MMDeviceEnumerator().GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia), loop);
            }
            else if (!loop)
            {
                if (players[resourceLocation].PlaybackState == PlaybackState.Stopped)
                    players[resourceLocation].Open(resourceLocation, new MMDeviceEnumerator().GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia), loop);
            }

            if (players[resourceLocation].PlaybackState != PlaybackState.Playing)
                players[resourceLocation].Play();
        }

        public void Stop(string resourceLocation)
        {
            if (players.ContainsKey(resourceLocation) && players[resourceLocation].PlaybackState != PlaybackState.Stopped)
                players[resourceLocation].Stop();
        }

        public void Dispose()
        {
            foreach (MusicPlayer player in players.Values)
            {
                player.Stop();
                player.Dispose();
            }
            players.Clear();
        }
    }
}