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
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Client name cannot be empty.");

            FirstName = firstName;
            LastName = lastName;
        }
    }