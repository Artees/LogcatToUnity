using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Artees.Diagnostics.LogcatToUnity
{
    internal class LogcatAndroidWrapper
    {
        public event Action<string> OnMessageReceived;

        public readonly int Pid;

        public Regex Filter { private get; set; }

        private readonly AndroidJavaObject _androidObject =
            new AndroidJavaObject("artees.diagnostics.logcattounityplugin.LogcatToUnity");

        private readonly LogcatProxy _proxy;

        public LogcatAndroidWrapper()
        {
            Pid = _androidObject.Call<int>("getPid");
            Filter = new Regex(string.Format("(?<!AudioTrack.*){0}", Pid));
            _proxy = new LogcatProxy(ReceiveMessage);
        }

        /// <summary>
        /// Starts monitoring the log.
        /// </summary>
        public void Start()
        {
            _proxy.IsActive = true;
            _androidObject.Call("start", _proxy);
            if (!Application.isEditor) return;
            ReceiveMessage(string.Format("({0}): Logcat output will be shown ", Pid));
            ReceiveMessage(string.Format("({0}): here when running on an Android device", Pid));
        }

        private void ReceiveMessage(string message)
        {
            if (!Filter.IsMatch(message)) return;
            if (OnMessageReceived != null) OnMessageReceived(message);
        }

        /// <summary>
        /// Stops monitoring the log.
        /// </summary>
        public void Stop()
        {
            _proxy.IsActive = false;
        }

        public string[] Read()
        {
            var log = _androidObject.Call<string[]>("read");
            return log ?? new string[0];
        }

        public void Clear()
        {
            _androidObject.Call("clear");
        }

        public void Log(string messageTag, string message)
        {
            _androidObject.Call("log", messageTag, message);
        }
    }
}