# Logcat to Unity
Sends Android log messages to the Unity game engine.

# Installation
Use [my package registry](https://artees.games/upm).

# Usage
```
_logcat = GetComponent<LogcatToUnity>();
_logcat.OnMessageReceived += Log;
```
The *LogcatToUnityUnity/Assets/Scenes/SampleScene.unity* scene contains an example of displaying the log messages in Unity's UI Text component. It displays only the last few messages, I'm working on a full-fledged runtime console.