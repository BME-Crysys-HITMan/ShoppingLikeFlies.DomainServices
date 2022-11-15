using DataAccessLogic.Model;

namespace DataAccessLogic
{
    public class DefaultDataAccessLogic : IDataAccessLogic
    {
        public DefaultDataAccessLogic()
        {

        }


        public Task CreateAsync(CreateModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ViewModel> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<ViewModel>> ListAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid id, ViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
