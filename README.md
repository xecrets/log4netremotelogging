# log4netremotelogging
This is the log4net Remote Logging Server Service project. It's not much actual code, but I could not find a ready-made log4net remoting receiver anywhere, so I made this for myself and also for you!
There is functionality built-in with log4net via the log4net.Appender.RemotingAppender class to send messages over the network, but it requires an application program to host the receiver and redirect the logs to their final destination.

That's what log4net Remote Logging Server Service is; a Windows Service implementing a thin host that listens to log messages sent with the RemotingAppender, and then redirects them according to it's own log4net configuration, by default to a RollingFileAppender.

Consolidate all your log4net logs from your applications and servers to a single log via the log4net .NET Remoting Appender.
Solve all issues with simultaneous writes to the log file, lost logs during roll-over etc.
Browse all logs from all systems in a single logfile.
Check out the documentation tab for details on how to use. It's simple! And real life- and time-saver once you've done it...
Please enjoy, and do provide feedback.

If you're a developer or system administrator, you may want to check out my other software .

Svante
