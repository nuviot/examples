using LagoVista.IoT.Logging.Loggers;
using LagoVista.IoT.Logging.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientAPI
{

    public class DebugWriter : ILogWriter
    {
        public Task WriteError(LogRecord record)
        {
            Debug.WriteLine($"ERROR => {record.Tag} {record.Message}");
            foreach (var param in record.Parameters)
                Debug.WriteLine($"\t{param.Key}={param.Value}");

            return Task.CompletedTask;
        }

        public Task WriteEvent(LogRecord record)
        {
            Debug.WriteLine($"EVENT => {record.Tag} {record.Message}");
            foreach (var param in record.Parameters)
                Debug.WriteLine($"\t{param.Key}={param.Value}");

            return Task.CompletedTask;
        }
    }
}
