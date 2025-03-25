using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class Client
{
    public string FirstName { get; }
    public string LastName { get; }

    public Client(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}