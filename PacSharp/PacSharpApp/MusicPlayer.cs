using System;
using CSCore;
using CSCore.Codecs;
using CSCore.CoreAudioAPI;
using CSCore.SoundOut;
using CSCore.Streams;

/// <summary>
/// https://github.com/filoe/cscore/blob/master/Samples/AudioPlayerSample/MusicPlayer.cs
/// </summary>
namespace PacSharpApp
{
    class MusicPlayer : IDisposable
    {
        private ISoundOut soundOut;
        private IWaveSource waveSource;

        public event EventHandler<PlaybackStoppedEventArgs> PlaybackStopped;

        public PlaybackState PlaybackState
        {
            get
            {
                if (soundOut != null)
                    return soundOut.PlaybackState;
                return PlaybackState.Stopped;
            }
        }

        public TimeSpan Position
        {
            get
            {
                if (waveSource != null)
                    return waveSource.GetPosition();
                return TimeSpan.Zero;
            }
            set
            {
                if (waveSource != null)
                    waveSource.SetPosition(value);
            }
        }

        public TimeSpan Length
        {
            get
            {
                if (waveSource != null)
                    return waveSource.GetLength();
                return TimeSpan.Zero;
            }
        }

        public int Volume
        {
            get
            {
                if (soundOut != null)
                    return Math.Min(100, Math.Max((int)(soundOut.Volume * 100), 0));
                return 100;
            }
            set
            {
                if (soundOut != null)
                {
                    soundOut.Volume = Math.Min(1.0f, Math.Max(value / 100f, 0f));
                }
            }
        }

        public void Open(string filename, MMDevice device, bool loop)
        {
            CleanupPlayback();

            waveSource =
                CodecFactory.Instance.GetCodec(filename)
                    .ToSampleSource()
                    .ToMono()
                    .ToWaveSource();
            if (loop)
                waveSource = new LoopStream(waveSource);
            else
                waveSource = new CachedSoundSource(waveSource);
            soundOut = new WasapiOut() { Latency = 100, Device = device };
            soundOut.Initialize(waveSource);
            if (PlaybackStopped != null) soundOut.Stopped += PlaybackStopped;
        }

        public void Play()
        {
            if (soundOut != null)
            {
                soundOut.Play();
            }
        }

        public void Pause()
        {
            if (soundOut != null)
            {
                soundOut.Pause();
            }
        }

        public void Stop()
        {
            if (soundOut != null)
            {
                soundOut.Stop();
            }
        }

        private void CleanupPlayback()
        {
            if (soundOut != null)
            {
                soundOut.Dispose();
                soundOut = null;
            }
            if (waveSource != null)
            {
                waveSource.Dispose();
                waveSource = null;
            }
        }

        public void Dispose()
        {
            CleanupPlayback();
        }
    }
}
