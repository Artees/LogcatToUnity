using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Artees.Diagnostics.LogcatToUnity
{
    /// <inheritdoc />
    /// <summary>
    /// Reads a log of Android system messages using the command-line logcat tool. It always uses
    /// all the buffers (<c>-b default</c>).
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class LogcatToUnity : MonoBehaviour
    {
        /// <summary>
        /// Occurs when a new message is received that matches <see cref="Filter"/>.
        /// </summary>
        public event Action<string[]> OnMessageReceived;

        /// <summary>
        /// The identifier of the Android process (PID), which can be used to filter log messages.
        /// </summary>
        public int Pid
        {
            get { return _logcat.Pid; }
        }

        /// <summary>
        /// A regular expression to filter <see cref="OnMessageReceived"/> events. The default
        /// regular expression is <code>(?&lt;!AudioTrack.*){Pid}</code>
        /// where {<see cref="Pid"/>} is the identifier of the Android process.
        /// </summary>
        public Regex Filter
        {
            set { _logcat.Filter = value; }
        }

        private readonly List<string> _messages = new List<string>();

        private LogcatAndroidWrapper _logcat;

        private void Awake()
        {
            _logcat = new LogcatAndroidWrapper();
            _logcat.OnMessageReceived += ReceiveMessage;
            _logcat.Start();
        }

        private void ReceiveMessage(string message)
        {
            lock (_messages)
            {
                _messages.Add(message);
            }
        }

        private void Update()
        {
            lock (_messages)
            {
                if (_messages.Count == 0) return;
                if (OnMessageReceived != null) OnMessageReceived(_messages.ToArray());
                _messages.Clear();
            }
        }

        private void OnDestroy()
        {
            _logcat.Stop();
            _logcat.OnMessageReceived -= ReceiveMessage;
        }

        /// <summary>
        /// Send an INFO log message that display in the logcat tool.
        /// </summary>
        /// <param name="messageTag">Used to identify the source of a log message.</param>
        /// <param name="message">The message you would like logged.</param>
        /// <seealso href="https://developer.android.com/reference/android/util/Log.html
        /// #i(java.lang.String,%20java.lang.String)"/>
        public void Log(string messageTag, string message)
        {
            _logcat.Log(messageTag, message);
        }

        /// <summary>
        /// Runs logcat, dumps the log and exits logcat. Does not trigger the
        /// <see cref="OnMessageReceived"/> event.
        /// </summary>
        /// <returns>Log messages.</returns>
        public string[] Read()
        {
            return _logcat.Read();
        }

        /// <summary>
        /// Clears all of the log buffers.
        /// </summary>
        public void Clear()
        {
            _logcat.Clear();
        }
    }
}