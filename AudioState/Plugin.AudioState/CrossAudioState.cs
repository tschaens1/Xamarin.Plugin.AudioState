using Plugin.AudioState.Abstractions;
using System;
using System.Threading;

namespace Plugin.AudioState
{
    /// <summary>
    /// Cross platform AudioState implemenation
    /// </summary>
    public class CrossAudioState
    {
        static Lazy<IAudioState> Implementation = new Lazy<IAudioState>(CreateAudioState, LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Current <see cref="IAudioState"/> instance as lazy singleton.
        /// </summary>
        public static IAudioState Current
        {
            get
            {
                var ret = Implementation.Value;
                if (ret == null)
                {
                    throw NotImplementedInReferenceAssembly();
                }
                return ret;
            }
        }

        static IAudioState CreateAudioState()
        {
#if PORTABLE
            return null;
#else
            return new AudioStateImplementation();
#endif
        }

        internal static Exception NotImplementedInReferenceAssembly()
        {
            return new NotImplementedException("This functionality is not implemented in the portable version of this assembly. You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
        }
    }
}
