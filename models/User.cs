namespace PLANAZO_POO_CSharp.Models;

public sealed class User
{
    public User(int id, string name)
    {
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id), "The user identifier must be greater than zero.");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("The user name is required.", nameof(name));
        }

        Id = id;
        Name = name.Trim();
    }

    public int Id { get; }
    public string Name { get; }
}
