namespace Plugin.AudioState.Abstractions
{
    /// <summary>
    /// Interface for AudioState
    /// </summary>
    public interface IAudioState
    {
        /// <summary>
        /// Indicates whether any app is currently playing music in the background.
        /// </summary>
        /// <returns><c>true</c> if music is playing, <c>false</c> otherwise.</returns>
        bool IsMusicPlaying { get; }

        /// <summary>
        /// Indicates whether headset is connected to the phone.
        /// </summary>
        /// <returns><c>true</c> if headset is connected, <c>false</c> otherwise.</returns>
        bool IsHeadsetConnected { get; }

        /// <summary>
        /// Returns the current audio output latency.
        /// </summary>
        double CurrentOutputLatency { get; }

        /// <summary>
        /// Gets the current <see cref="OutputRoute"/>.
        /// </summary>
        OutputRoute CurrentOutputRoute { get; }

        /// <summary>
        /// Returns the current audio output volume.
        /// </summary>
        /// <param name="outputRoute">The <see cref="OutputRoute"/> to check the output volume on.</param>
        /// <returns>The current audio output volume on the specified channel.</returns>
        double CurrentOutputVolume(OutputRoute? outputRoute = default(OutputRoute?));
    }
}