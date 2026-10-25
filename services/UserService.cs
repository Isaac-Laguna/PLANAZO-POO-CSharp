using PLANAZO_POO_CSharp.Models;

namespace PLANAZO_POO_CSharp.Services;

public sealed class UserService
{
    private readonly List<User> _users = [];
    private int _nextId = 1;

    public UserService(IEnumerable<User> initialUsers)
    {
        foreach (var user in initialUsers)
        {
            _users.Add(user);
        }

        _nextId = _users.Count + 1;
    }

    public IReadOnlyList<User> GetAll() => _users.AsReadOnly();

    public User? FindById(int id) => _users.FirstOrDefault(user => user.Id == id);

    public User GetOrCreate(string name)
    {
        var existingUser = _users.FirstOrDefault(user =>
            user.Name.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase));

        if (existingUser is not null)
        {
            return existingUser;
        }

        var user = new User(_nextId, name);
        _users.Add(user);
        _nextId++;

        return user;
    }
}
