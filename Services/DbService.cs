using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books.DbModels;

namespace Books.Services;

public class DbService
{
    private readonly SQLiteAsyncConnection _database;

    public DbService()
    {
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "Books.db");
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<DbBook>().Wait();
    }

    public Task<List<DbBook>> GetBookAsync() => _database.Table<DbBook>().ToListAsync();

    public async Task<int> SaveBookAsync(DbBook book)
    {
        var existingBook = await _database.Table<DbBook>().FirstOrDefaultAsync(b => b.GoogleId == book.GoogleId);
        if (existingBook != null)
        {
            throw new InvalidOperationException("Wybrana ksiązka jest już w liście przeczytanych");
        }
        return await _database.InsertAsync(book);
    }
    public Task<int> DeleteBookAsync(DbBook book) => _database.DeleteAsync(book);
}
