using AVFoundation;
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
        /// Holds the current <see cref="AVAudioSession"/> as singleton.
        /// </summary>
        protected readonly AVAudioSession Session = AVAudioSession.SharedInstance();

        #endregion

        #region Constructor

        /// <summary>
        /// The constructor
        /// </summary>
        public AudioStateImplementation()
        {
            Session.SetCategory(AVAudioSessionCategory.PlayAndRecord, AVAudioSessionCategoryOptions.MixWithOthers);
            Session.SetActive(true);
        }

        #endregion

        #region Public methods inherited from IAudioState

        /// <inheritdoc cref="IAudioState"/>
        public bool IsMusicPlaying => Session.OtherAudioPlaying;

        /// <inheritdoc cref="IAudioState"/>
        public bool IsHeadsetConnected => OutputRoute.HeadphoneJack.Equals(CurrentOutputRoute);

        /// <inheritdoc cref="IAudioState"/>
        public double CurrentOutputLatency => Session.OutputLatency;

        /// <inheritdoc cref="IAudioState"/>
        public OutputRoute CurrentOutputRoute => GetCurrentOutputRoute();

        /// <inheritdoc cref="IAudioState"/>
        public double CurrentOutputVolume(OutputRoute? outputRoute = null) => Session.OutputVolume;

        #endregion

        #region Private helper methods

        private OutputRoute GetCurrentOutputRoute()
        {
            foreach (var output in Session.CurrentRoute.Outputs)
            {
                var type = output?.PortType?.ToString();

                if (type == null)
                {
                    return OutputRoute.Unknown;
                }

                if (type == AVAudioSession.PortBluetoothLE || type == AVAudioSession.PortBluetoothA2DP || type == AVAudioSession.PortBluetoothHfp)
                {
                    return OutputRoute.ExternalSpeaker;
                }

                if (type == AVAudioSession.PortHeadphones)
                {
                    return OutputRoute.HeadphoneJack;
                }

                if (type == AVAudioSession.PortBuiltInSpeaker)
                {
                    return OutputRoute.InternalSpeaker;
                }
            }

            return OutputRoute.Unknown;
        }

        #endregion

        /// <summary>
        /// Disposes the <see cref="AVAudioSession"/> 
        /// </summary>
        public void Dispose()
        {
            Session?.Dispose();
        }
    }
}