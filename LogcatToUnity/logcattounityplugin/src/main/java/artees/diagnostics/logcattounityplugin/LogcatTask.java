package artees.diagnostics.logcattounityplugin;

import android.os.AsyncTask;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;

public class LogcatTask extends AsyncTask<LogcatProxy, String, Void> {
    @Override
    protected Void doInBackground(LogcatProxy... proxies) {
        LogcatProxy proxy = proxies[0];
        try {
            Process process = Runtime.getRuntime().exec("logcat -b all -v brief");
            InputStreamReader inputStreamReader = new InputStreamReader(process.getInputStream());
            BufferedReader bufferedReader = new BufferedReader(inputStreamReader);
            while (proxy.isActive()) {
                String line = bufferedReader.readLine();
                if (line == null) continue;
                proxy.log(line);
            }
        } catch (IOException e) {
            proxy.log(e.getMessage());
        }
        return null;
    }
}
