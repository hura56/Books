using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Books.DbModels;

public class DbBook
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; } // Unikalny klucz w bazie
    public string GoogleId { get; set; } // Id z API Google Books
    public string Title { get; set; }
    public string Authors { get; set; }
    public string Description { get; set; }
    public string PublishedDate { get; set; }
    public string Thumbnail { get; set; }
}
