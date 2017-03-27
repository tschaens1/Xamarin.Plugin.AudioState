using Android.App;
using Plugin.AudioState.Abstractions;
using Android.Content;
using Android.Media;
using System;

namespace Plugin.AudioState
{
    /// <summary>
    /// Implementation of IAudioState
    /// </summary>
    public class AudioStateImplementation : IAudioState, IDisposable
    {
        private const string GetOutputLatencyMethodName = "getOutputLatency";

        /// <summary>
        /// Holds the <see cref="AudioManager"/> in the current <see cref="Context"/>
        /// </summary>
        protected static readonly AudioManager AudioManager = (AudioManager)Application.Context.GetSystemService(Context.AudioService);

        /// <inheritdoc/>
        public bool IsMusicPlaying => AudioManager.IsMusicActive;

        /// <inheritdoc/>
        public bool IsHeadsetConnected => AudioManager.WiredHeadsetOn;

        /// <inheritdoc/>
        public double CurrentOutputLatency => GetAudioOutputLatency();

        /// <inheritdoc/>
        public double CurrentOutputVolume(OutputRoute? outputRoute = null)
        {
            return AudioManager.GetStreamVolume(Stream.Music);
        }

        /// <inheritdoc/>
        public OutputRoute CurrentOutputRoute => throw new NotImplementedException();

        /// <inheritdoc/>
        private double GetAudioOutputLatency()
        {
            try
            {
                var m = AudioManager?.Class.GetMethod(GetOutputLatencyMethodName, Java.Lang.Integer.Type);
                return (int)m?.Invoke(AudioManager, Stream.Music.GetHashCode());
            }
            catch (MissingMethodException)
            {
                throw new MissingMethodException("Method not found in the AudioManager class. Be sure to target at least API Level 19.");
            }
        }

        /// <summary>
        /// Disposes the <see cref="AudioManager"/>
        /// </summary>
        public void Dispose()
        {
            AudioManager.Dispose();
        }
    }
}