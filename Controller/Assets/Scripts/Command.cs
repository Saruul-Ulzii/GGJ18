using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command {

	public string Name { get; private set; }
    public string Content { get; private set; }

    public Command( string name, string content)
    {
        this.Name = name;
        this.Content = content;
    }

    public string serialize()
    {
        string serialized = Name + ";" + Content;
        return serialized;
    }

    public static Command deserialize( string message)
    {
        string[] splitted = message.Split(';');
        if ( splitted.Length != 2)
        {
            throw new ArgumentException("Can't parse message: " + message );
        }
        return new Command(splitted[0], splitted[1]);
    }
}
