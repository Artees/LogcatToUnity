# Logcat to Unity
[![openupm](https://img.shields.io/npm/v/games.artees.logcat-to-unity?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/games.artees.logcat-to-unity/)

Sends Android log messages to the Unity game engine.

# Installation
Install the package **games.artees.logcat-to-unity** using [my package registry](https://artees.games/upm).

Or install via the [OpenUPM registry](https://openupm.com/packages/games.artees.logcat-to-unity/). (Requires [openupm-cli](https://github.com/openupm/openupm-cli))
```
openupm add games.artees.logcat-to-unity
```

# Usage
```
_logcat = GetComponent<LogcatToUnity>();
_logcat.OnMessageReceived += Log;
```
The *LogcatToUnityUnity/Assets/Scenes/SampleScene.unity* scene contains an example of displaying the log messages in Unity's UI Text component. It displays only the last few messages, I'm working on a full-fledged runtime console.
