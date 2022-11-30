using ShoppingLikeFiles.DomainServices.Contract;
using ShoppingLikeFiles.DomainServices.Contract.Incoming;

namespace ShoppingLikeFiles.DomainServices.Service;

/// <summary>
/// This class represents the connection between DAL and Presentation layer.
/// </summary>
public interface IDataService
{
    Task<Guid> CreateCaff(CreateCaffContract contract);
    Task<bool> DeleteCaff(Guid id);
    Task<List<CaffResponse>> SearchCaff(string? creator = null, string? caption = null, string[]? tags = null);
    Task<CaffDTO> AsyncGetCaff(int id);
    Task<List<CaffResponse>> GetAll();
}
