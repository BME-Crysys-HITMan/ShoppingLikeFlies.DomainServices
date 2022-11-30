using ShoppingLikeFiles.DomainServices.Contract;
using ShoppingLikeFiles.DomainServices.Contract.Incoming;

namespace ShoppingLikeFiles.DomainServices.Service;

/// <summary>
/// This class represents the connection between DAL and Presentation layer.
/// </summary>
public interface IDataService
{
    Task<int> AsyncCreateCaff(CreateCaffContractDTO contract);
    Task<bool> AsyncDeleteCaff(int id);
    Task AsyncUpdateCaff(CaffDTO caffDTO);
    Task<List<CaffDTO>> AsyncSearchCaff(CaffSearchDTO caffSearchDTO);
    Task<CaffDTO> AsyncGetCaff(int id);
    Task<List<CaffDTO>> AsyncGetAll();
}
