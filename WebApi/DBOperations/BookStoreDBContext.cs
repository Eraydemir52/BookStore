using Microsoft.EntityFrameworkCore;

namespace WebaApi.DBOperations
{
    public class BookStoreDbContext :DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options):base(options)
        {


        }
        public DbSet<Book> Books { get; set; }

    }

}