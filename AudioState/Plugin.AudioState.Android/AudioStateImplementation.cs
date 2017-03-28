using Android.App;
using Android.Content;
using Android.Media;
using Plugin.AudioState.Abstractions;
using System;

namespace Plugin.AudioState
{
    /// <summary>
    /// Implementation of <see cref="IAudioState"/>
    /// </summary>
    public class AudioStateImplementation : IAudioState, IDisposable
    {
        private const string MethodGetLatency = "getOutputLatency";
        private const string IntentFilterState = "state";

        /// <summary>
        /// Holds the <see cref="Android.Media.AudioManager"/> in the current <see cref="Context"/>.
        /// </summary>
        protected static readonly AudioManager AudioManager = (AudioManager)Application.Context.GetSystemService(Context.AudioService);

        /// <inheritdoc cref="IAudioState"/>
        public bool IsMusicPlaying => AudioManager.IsMusicActive;

        /// <inheritdoc cref="IAudioState"/>
        public bool IsHeadsetConnected => GetItHeadsetConnected();

        /// <inheritdoc cref="IAudioState"/>
        public double CurrentOutputLatency => GetAudioOutputLatency();

        /// <inheritdoc cref="IAudioState"/>
        public OutputRoute CurrentOutputRoute
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <inheritdoc cref="IAudioState"/>
        public double CurrentOutputVolume(OutputRoute? outputRoute = default(OutputRoute?))
        {
            return AudioManager.GetStreamVolume(Stream.Music);
        }

        private static double GetAudioOutputLatency()
        {
            try
            {
                var latencyMethod = AudioManager?.Class.GetMethod(MethodGetLatency, Java.Lang.Integer.Type);
                return (int)latencyMethod?.Invoke(AudioManager, Stream.Music.GetHashCode());
            }
            catch (MissingMethodException)
            {
                throw new MissingMethodException("Method not found in the AudioManager class. Be sure to target at least API Level 19.");
            }
        }

        private static bool GetItHeadsetConnected()
        {
            var filter = new IntentFilter(AudioManager.ActionHeadsetPlug);
            var status = Application.Context.RegisterReceiver(null, filter);
            return status.GetIntExtra(IntentFilterState, 0) == 1;
        }

        /// <summary>
        /// Disposes the <see cref="Android.Media.AudioManager"/>
        /// </summary>
        public void Dispose()
        {
            AudioManager.Dispose();
        }
    }
}
