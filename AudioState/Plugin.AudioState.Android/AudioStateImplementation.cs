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
        #region Properties

        /// <summary>
        /// Holds the <see cref="Android.Media.AudioManager"/> in the current <see cref="Context"/>.
        /// </summary>
        protected static readonly AudioManager AudioManager = (AudioManager)Application.Context.GetSystemService(Context.AudioService);

        #endregion

        #region Public methods inherited from IAudioState

        /// <inheritdoc cref="IAudioState"/>
        public bool IsMusicPlaying => AudioManager.IsMusicActive;

        /// <inheritdoc cref="IAudioState"/>
        public bool IsHeadsetConnected => GetItHeadsetConnected();

        /// <inheritdoc cref="IAudioState"/>
        public double CurrentOutputLatency => GetAudioOutputLatency();

        /// <inheritdoc cref="IAudioState"/>
        public OutputRoute CurrentOutputRoute => GetCurrentOutputRoute();

        /// <inheritdoc cref="IAudioState"/>
        public double CurrentOutputVolume(OutputRoute? outputRoute = default(OutputRoute?))
        {
            Stream stream;

            if (OutputRoute.HeadphoneJack.Equals(outputRoute))
            {
                stream = Stream.Music;
            }
            else if (OutputRoute.Default.Equals(outputRoute))
            {
                stream = Stream.Ring;
            }
            else
            {
                stream = Stream.Alarm;
            }

            return AudioManager.GetStreamVolume(stream);
        }

        #endregion

        #region Private helper methods

        private static double GetAudioOutputLatency()
        {
            try
            {
                using (var latencyMethod = AudioManager?.Class.GetMethod("getOutputLatency", Java.Lang.Integer.Type))
                {
                    return (int)latencyMethod?.Invoke(AudioManager, Stream.Music.GetHashCode());
                }
            }
            catch (MissingMethodException)
            {
                throw new MissingMethodException("Method not found in the AudioManager class. Be sure to target at least API Level 19.");
            }
        }

        private static bool GetItHeadsetConnected()
        {
            Intent status;

            using (var headsetFilter = new IntentFilter(AudioManager.ActionHeadsetPlug))
            {
                status = Application.Context.RegisterReceiver(null, headsetFilter);
            }

            return status.GetIntExtra("state", 0) == 1;
        }

        private OutputRoute GetCurrentOutputRoute()
        {
            if (AudioManager.SpeakerphoneOn)
            {
                return OutputRoute.InternalSpeaker;
            }
            else if (AudioManager.BluetoothScoOn || AudioManager.BluetoothA2dpOn)
            {
                return OutputRoute.ExternalSpeaker;
            }
            else if (IsHeadsetConnected)
            {
                return OutputRoute.HeadphoneJack;
            }

            return OutputRoute.Unknown;
        }

        #endregion

        /// <summary>
        /// Disposes the <see cref="Android.Media.AudioManager"/>
        /// </summary>
        public void Dispose()
        {
            AudioManager?.Dispose();
        }
    }
}