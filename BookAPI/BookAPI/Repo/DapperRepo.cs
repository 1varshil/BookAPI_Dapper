using BookAPI.DapperContext;
using BookAPI.Interface;
using BookAPI.Models;
using Dapper;

namespace BookAPI.Repo
{
    public class DapperRepo : IDapperService
    {
        private readonly DapperDbContext _dapperContext;

        public DapperRepo(DapperDbContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<List<Book>> GetAll()
        {
            var sql = "SELECT * FROM book";
            using (var connection = _dapperContext.CreateConnection())
            {
                return (await connection.QueryAsync<Book>(sql)).ToList();
            }
        }

        public async Task<Book> GetBookById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ID.");

            var sql = "SELECT * FROM book WHERE id = @id";
            using (var connection = _dapperContext.CreateConnection())
            {
                var book = await connection.QueryFirstOrDefaultAsync<Book>(sql, new { id });
                if (book == null)
                    throw new KeyNotFoundException($"Book with ID {id} not found.");

                return book;
            }
        }

        public async Task<Book> CreateBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            var sql = "INSERT INTO book (title, description, author, createdAt) VALUES (@title, @description, @author, @CreatedAt)";
            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(sql, GetBookParameters(book));
            }
            return book;
        }

        public async Task<int> UpdateBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            var sql = @"UPDATE book 
                SET Title = @Title, 
                    Description = @Description, 
                    Author = @Author,    
                    CreatedAt= @CreatedAt
                WHERE Id = @Id";

            using (var connection = _dapperContext.CreateConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(sql, GetBookParameters(book));

                if (rowsAffected == 0)
                    throw new Exception($"No record found with Id = {book.Id} to update.");

                return rowsAffected;
            }
        }


        public async Task<int> DeleteBook(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ID.");

            var sql = "DELETE FROM book WHERE id = @id";
            using (var connection = _dapperContext.CreateConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(sql, new { id });
                return rowsAffected;
            }
        }

        private DynamicParameters GetBookParameters(Book book)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", book.Id, System.Data.DbType.String);   
            parameters.Add("Title", book.Title, System.Data.DbType.String);
            parameters.Add("Description", book.Description, System.Data.DbType.String);
            parameters.Add("Author", book.Author, System.Data.DbType.String);
            parameters.Add("CreatedAt", book.CreatedAt, System.Data.DbType.String);
            return parameters;
        }

    }
}
