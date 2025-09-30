using Microsoft.EntityFrameworkCore;
using TpCommandManagerData.Context;
using TpCommandManagerData.Entities;

namespace TpCommandManagerData.Repositories;

public sealed class UserRepository
{
    private readonly CommandStoreContext _context;

    public UserRepository(CommandStoreContext context)
    {
        _context = context;
    }

    public List<User> GetAllUsers()
    {
        return _context.Users.Include(u => u.Address).ToList();
    }

    public User GetUser(int id)
    {
        return this._context.Users.Include(u => u.Address).Where(u => u.Id == id).FirstOrDefault();
    }

    public void CreateUser(User user)
    {
        this._context.Users.Add(user);
        this._context.SaveChanges();
    }

    public void UpdateUser(User user)
    {
        this._context.Users.Update(user);
        this._context.SaveChanges();
    }

    public void DeleteUser(User user)
    {
        this._context.Users.Remove(user);
        this._context.SaveChanges();
    }
}