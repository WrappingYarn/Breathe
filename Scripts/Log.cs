using System;
using System.Collections.Generic;

public class Log
{
    public int LogNumber { get; set; }
    // if we want to make each message to have an image of the speaker we can turn this into a new class
    public List<Tuple<string, string>> Message { get; set; }
}