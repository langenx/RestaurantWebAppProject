namespace RestaurantWebAppProject.Models
{
    public interface IRepository<T> where T: class //interfejs definiujący podstawowe operacje CRUD dla dowolnego typu T
    {
        Task<IEnumerable<T>> GetAllAsync(); //ppobiera wszystkie rekordy danego typu T z basy danych w sposób asynchroniczny
        Task<T> GetByIdAsync(int id, QueryOptions<T> options); //pobiera pojedynczy rekord na podstawie identyfikatora id z dodatkowy opcjami QueryOptions
        Task AddAsync(T entity); //dodaje nowy rerkord do bazy danych w sposób asynchroniczny
        Task UpdateAsync(T entity); //aktualizuje istniejący rekord w bazie danych w sposób asynchroniczny
        Task DeleteAsync(int id); //usuwa rekord z bazy danych w sposób asynchroniczny
    }
}
