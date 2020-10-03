using System;
public class SingletonTagAttribute : Attribute
{
    public string TagRoot { get; private set; }
    public SingletonTagAttribute(string rootObject = "")
    {
        TagRoot = rootObject;
    }
}

