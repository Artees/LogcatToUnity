package artees.diagnostics.logcattounityplugin;

public interface LogcatProxy {
    void log(String message);
    boolean isActive();
}
