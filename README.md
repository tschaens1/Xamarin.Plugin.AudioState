# Xamarin.Plugin.AudioState

The AudioState plugin for Xamarin and Xamarin.Forms applications will add some cross-platform audio checks to your application.

## Getting Started

Coming soon.

### Prerequisites

Coming soon.

### Using the API

```csharp

/// <summary>
/// Indicates whether any app is currently playing music in the background
/// </summary>
/// <returns><c>true</c> if music is playing, <c>false</c> otherwise</returns>
bool IsMusicPlaying { get; }

/// <summary>
/// Indicates whether headset is connected to the phone
/// </summary>
/// <returns><c>true</c> if headset is connected, <c>false</c> otherwise</returns>
bool IsHeadsetConnected { get; }

/// <summary>
/// Returns the current audio output latency
/// </summary>
double CurrentOutputLatency { get; }

/// <summary>
/// Returns the current audio output volume
/// </summary>
/// <param name="outputRoute">The <see cref="OutputRoute"/> to check the output volume on</param>
/// <returns>The current audio output volume on the specified (default if null) channel</returns>
double CurrentOutputVolume(OutputRoute? outputRoute = null);

/// <summary>
/// Gets the current <see cref="OutputRoute"/>
/// </summary>
OutputRoute CurrentOutputRoute { get; }

```

## Versioning

The current version is v0.1.0.0, I will try to release the package via NuGet as soon as possible.

## Version history

Coming soon.

## Authors

* **Jan Schölch** - *Initial work* - [tschaens1](https://github.com/tschaens1)

Kudos to [James Montemagno](https://github.com/jamesmontemagno) for providing the awesome plugin project template!

## Contributing

As we all know, students do not have a lot of time and are very busy doing stuff ;-))
So I am very happy about everyone who wants to contribute!
You can do so by following these steps:

1. Just do a Fork
2. Create your feature branch: `git checkout -b my-new-feature`
3. Commit your changes: `git commit -am 'Add some feature'`
4. Push your changes to the branch: `git push origin my-new-feature`
5. Submit a pull request

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.
