public class Client
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public Client(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public void UpdateName(string newFirstName, string newLastName)
    {
        FirstName = newFirstName;
        LastName = newLastName;
    }
}