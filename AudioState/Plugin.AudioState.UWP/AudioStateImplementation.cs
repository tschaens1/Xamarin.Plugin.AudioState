using System;
using Plugin.AudioState.Abstractions;
using Windows.Media.Playback;
using Windows.Phone.Media.Devices;

namespace Plugin.AudioState
{
    /// <summary>
    /// Implementation for Feature
    /// </summary>
    public class AudioStateImplementation : IAudioState
    {
        protected readonly MediaPlayer mediaPlayer = BackgroundMediaPlayer.Current;
        protected readonly AudioRoutingManager audioRoutingManager = AudioRoutingManager.GetDefault();

        /// <inheritdoc/>
        public bool IsMusicPlaying => mediaPlayer.CurrentState == MediaPlayerState.Playing;

        /// <inheritdoc/>
        public bool IsHeadphonesConnected => audioRoutingManager.AvailableAudioEndpoints == AvailableAudioRoutingEndpoints.Earpiece;

        /// <inheritdoc/>
        public double CurrentVolume => mediaPlayer.Volume;

        /// <inheritdoc/>
        public bool IsHeadsetConnected => throw new NotImplementedException();

        /// <inheritdoc/>
        public double CurrentOutputLatency => throw new NotImplementedException();

        /// <inheritdoc/>
        public OutputRoute CurrentOutputRoute { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <inheritdoc/>
        public double CurrentOutputVolume(OutputRoute? outputRoute = default(OutputRoute?))
        {
            throw new NotImplementedException();
        }
    }
}