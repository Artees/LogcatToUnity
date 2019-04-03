using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Artees.Diagnostics.LogcatToUnity
{
    /// <inheritdoc />
    /// <summary>
    /// Displays Android log messages in Unity's UI <see cref="T:UnityEngine.UI.Text" /> component.
    /// </summary>
    [RequireComponent(typeof(LogcatToUnity))]
    public class LogcatToUnityText : MonoBehaviour
    {
        private readonly List<string> _messages = new List<string>();
        private readonly StringBuilder _stringBuilder = new StringBuilder();

        private Text _text;
        private LogcatToUnity _logcat;

        private void Awake()
        {
            _text = GetComponent<Text>();
            _logcat = GetComponent<LogcatToUnity>();
            _logcat.OnMessageReceived += Log;
        }

        private void Log(string[] messages)
        {
            _messages.AddRange(messages);
            var r = _messages.Count - 15;
            if (r > 0) _messages.RemoveRange(0, r);
            _stringBuilder.Clear();
            foreach (var line in _messages)
            {
                _stringBuilder.AppendLine();
                _stringBuilder.AppendLine();
                _stringBuilder.Append(line);
            }

            _text.text = _stringBuilder.ToString();
        }

        private void OnDestroy()
        {
            _logcat.OnMessageReceived -= Log;
        }
    }
}