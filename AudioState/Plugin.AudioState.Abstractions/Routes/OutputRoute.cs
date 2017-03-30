namespace Plugin.AudioState.Abstractions
{
    /// <summary>
    /// Abstraction of the audio output routes available on a device.
    /// </summary>
    public enum OutputRoute
    {
        /// <summary>
        /// The default output route, which is usually the device's speaker.
        /// </summary>
        Default,

        /// <summary>
        /// The headphone jack, if available.
        /// </summary>
        HeadphoneJack,

        /// <summary>
        /// The device's speaker, if available.
        /// </summary>
        InternalSpeaker,

        /// <summary>
        /// An external speaker, if connected.
        /// </summary>
        ExternalSpeaker,

        /// <summary>
        /// An unknown output route.
        /// </summary>
        Unknown
    }
}
