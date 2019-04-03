package artees.diagnostics.logcattounityplugin;

import android.util.Log;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.ArrayList;

@SuppressWarnings("unused")
public class LogcatToUnity {
    public void start(LogcatProxy proxy) {
        new LogcatTask().execute(proxy);
    }

    public String[] read() {
        try {
            Process process = Runtime.getRuntime().exec("logcat -b all -d -v brief");
            InputStreamReader inputStreamReader = new InputStreamReader(process.getInputStream());
            BufferedReader bufferedReader = new BufferedReader(inputStreamReader);
            ArrayList<String> log = new ArrayList<>();
            String line;
            while ((line = bufferedReader.readLine()) != null) {
                log.add(line);
            }
            return log.toArray(new String[0]);
        } catch (IOException e) {
            return new String[]{e.getMessage()};
        }
    }

    public void clear() {
        try {
            Process clear = new ProcessBuilder().command("logcat", "-b all -c")
                    .redirectErrorStream(true).start();
        } catch (IOException ignored) {
        }
    }

    public void log(String tag, String message) {
        Log.i(tag, message);
    }

    public int getPid() {
        return android.os.Process.myPid();
    }
}
