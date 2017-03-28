using System;
using Plugin.AudioState.Abstractions;
using Windows.Media.Playback;
using Windows.Phone.Media.Devices;

namespace Plugin.AudioState
{
    /// <summary>
    /// Implementation of <see cref="IAudioState"/>
    /// </summary>
    public class AudioStateImplementation : IAudioState
    {
        protected readonly MediaPlayer mediaPlayer = BackgroundMediaPlayer.Current;
        protected readonly AudioRoutingManager audioRoutingManager = AudioRoutingManager.GetDefault();

        /// <inheritdoc cref="IAudioState"/>
        public bool IsMusicPlaying => mediaPlayer.CurrentState == MediaPlayerState.Playing;

        /// <inheritdoc cref="IAudioState"/>
        public bool IsHeadsetConnected => audioRoutingManager.AvailableAudioEndpoints == AvailableAudioRoutingEndpoints.Earpiece;

        /// <inheritdoc cref="IAudioState"/>
        public double CurrentOutputLatency => throw new NotImplementedException();

        /// <inheritdoc cref="IAudioState"/>
        public OutputRoute CurrentOutputRoute => throw new NotImplementedException();

        /// <inheritdoc cref="IAudioState"/>
        public double CurrentOutputVolume(OutputRoute? outputRoute = default(OutputRoute?)) => throw new NotImplementedException();
    }
}