using ShoppingLikeFiles.DomainServices.Contract;
using ShoppingLikeFiles.DomainServices.Contract.Incoming;

namespace ShoppingLikeFiles.DomainServices.Service;

/// <summary>
/// This class represents the connection between DAL and Presentation layer.
/// </summary>
public interface IDataService
{
    Task<int> CreateCaffAsync(CreateCaffContractDTO contract);
    Task<bool> DeleteCaffAsync(int id);
    Task UpdateCaffAsync(CaffDTO caffDTO);
    Task<List<CaffDTO>> SearchCaffAsync(CaffSearchDTO caffSearchDTO);
    Task<CaffDTO> GetCaffAsync(int id);
    Task<List<CaffDTO>> GetAllAsync();
}
