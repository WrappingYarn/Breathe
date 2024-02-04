using System;
using System.Collections.Generic;

public static class Logs
{
    private static readonly Dictionary<int, Log> _logs = new()
    {
        {
            -1,
            new Log
            {
                LogNumber = -1,
                Message = new()
                {
                    new("Fil", "This is page one of the log."),
                    new("El", "This is page 2 of the log."),
                    new("Floor Ascent", "This is not page 4,"),
                    new("Incan Descent", "But this is."),
                    new("Halogen", "This is the last one.")
                }
            }
        }
    };

    public static Log GetLogNumber(int logNumber)
    {
        if(_logs.TryGetValue(logNumber, out var log))
            return log;
        else throw new Exception("Cheater");
    }
}