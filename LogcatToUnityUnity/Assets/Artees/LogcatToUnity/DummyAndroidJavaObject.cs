using System;

namespace Artees.LogcatToUnity
{
    public class DummyAndroidJavaObject
    {
        private static void NullAction(string message)
        {
        }

        private readonly string _className;
        private readonly Action<string> _onCall;

        public DummyAndroidJavaObject(string className = "<Java object>", Action<string> onCall = null)
        {
            _onCall = onCall ?? NullAction;
            _className = className;
        }

        public T Call<T>(string methodName, params T[] args)
        {
            Log(methodName, args);
            return default;
        }

        public void Call(string methodName)
        {
            Log(methodName);
        }

        private void Log<T>(string methodName, params T[] args)
        {
            var argsString = string.Join(", ", args);
            Log(methodName, argsString);
        }

        private void Log(string methodName, string argsString = "")
        {
            var message = $"The Java method has been called: {_className}.{methodName}({argsString})";
            _onCall?.Invoke(message);
        }
    }
}