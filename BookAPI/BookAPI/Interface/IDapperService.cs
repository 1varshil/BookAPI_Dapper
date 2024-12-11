using BookAPI.Models;

namespace BookAPI.Interface
{
    public interface IDapperService
    {
        Task<List<Book>> GetAll();
        Task<Book> GetBookById(int id);
        Task<Book> CreateBook(Book book);

        // UpdateBook now returns the number of rows affected (int)
        Task<int> UpdateBook(Book book);

        // DeleteBook now returns a boolean indicating success or failure
        Task<int> DeleteBook(int id);
    }
}
