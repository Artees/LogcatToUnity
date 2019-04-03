using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using UnityEngine;

namespace Artees.Diagnostics.LogcatToUnity
{
    internal class LogcatProxy : AndroidJavaProxy
    {
        private readonly Action<string> _onMessageReceived;

        public bool IsActive { private get; set; }
        
        public LogcatProxy(Action<string> onMessageReceived) : 
            base("artees.diagnostics.logcattounityplugin.LogcatProxy")
        {
            _onMessageReceived = onMessageReceived;
        }

        [UsedImplicitly, SuppressMessage("ReSharper", "InconsistentNaming")]
        public void log(string message)
        {
            _onMessageReceived(message);
        }

        [UsedImplicitly, SuppressMessage("ReSharper", "InconsistentNaming")]
        public bool isActive()
        {
            return IsActive;
        }
    }
}