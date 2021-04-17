using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace GameFramework.Utilities
{
    public class Tracer : ITracer
    {
        private static readonly Tracer InstanceTracer = new Tracer();
        public static Tracer Instance => InstanceTracer;


        private TraceSource ts = new TraceSource("Game Demo");
        private Tracer()
        {
            ts.Switch = new SourceSwitch("SuperGame", "All");

            TraceListener consoleLog = new ConsoleTraceListener();
            ts.Listeners.Add(consoleLog);

            TraceListener fileLog = new TextWriterTraceListener(new StreamWriter("TraceDemo.txt"));
            ts.Listeners.Add(fileLog);
            //fileLog.Filter = new EventTypeFilter(SourceLevels.Error);

            //TraceListener eventLog = new EventLogTraceListener("Application");
            //ts.Listeners.Add(eventLog);
            //eventLog.Filter = new EventTypeFilter(SourceLevels.Error);
        }

        public void TraceEvent(string text)
        {
            ts.TraceEvent(TraceEventType.Information, 333, text);
        }

        public void Close()
        {
            ts.Close();
        }
    }
}
