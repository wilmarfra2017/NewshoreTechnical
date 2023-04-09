namespace Newshore.Technical.Domain.Interfaces
{
    public interface IGenericRepository<T>
    {
        public Task<T> Create(T objectToCreate);

        public Task<bool> Update(T objectToUpdate);

        public Task<bool> Delete(T objectToDelete);
    }
}
