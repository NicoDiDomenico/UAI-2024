namespace MindFitIntelligence_Backend.Services
{
    public interface ICommonService<T, TI, TU>
    {
        public List<string> Errors { get; }
        public Task<IEnumerable<T>> GetAll();
        public Task<T?> GetById(int id);
        public Task<T> Add(TI entityInsert);
        public Task<T?> Update(int id, TU entityUpdate);
        public Task<T?> Delete(int id);

        // Otros métodos de servicio comunes (validaciones):
        public bool IsNull(T entity);
    }
}
