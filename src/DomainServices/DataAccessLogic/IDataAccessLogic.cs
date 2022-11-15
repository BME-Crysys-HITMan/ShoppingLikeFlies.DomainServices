using DataAccessLogic.Model;

namespace DataAccessLogic
{
    public interface IDataAccessLogic
    {
        Task<IList<ViewModel>> ListAsync();
        Task<ViewModel> GetAsync(Guid id);


        Task CreateAsync(CreateModel model);
        Task UpdateAsync(Guid id, ViewModel model);
        Task DeleteAsync(Guid id);
    }
}
