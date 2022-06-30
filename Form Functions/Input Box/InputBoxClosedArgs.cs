using System;

namespace Form_Functions;

public class InputBoxClosedArgs : EventArgs
{
    public string[] Inputs { get; }

    public InputBoxClosedArgs(string[] inputs) => Inputs = inputs;
}