using ShoppingLikeFiles.DomainServices.Contract.Incoming;

namespace ShoppingLikeFiles.DomainServices.Service;

/// <summary>
/// This class represents the connection between DAL and Presentation layer.
/// </summary>
public interface IDataService
{
    /// <summary>
    /// Creates a mew <see cref="Caff"/> entry.
    /// </summary>
    /// <param name="contract">Value to be created.</param>
    /// <returns>Returns the id of the created entry.</returns>
    Task<int> CreateCaffAsync(CreateCaffContractDTO contract);

    /// <summary>
    /// Deletes an entry based on the given id.
    /// </summary>
    /// <param name="id">Id of a <see cref="Caff"/> entry.</param>
    /// <returns>Returns true if the deletion was successful.</returns>
    Task<bool> DeleteCaffAsync(int id);

    /// <summary>
    /// Updates a <see cref="Caff"/> entry.
    /// </summary>
    /// <param name="caffDTO">Updated values.</param>
    /// <returns></returns>
    Task UpdateCaffAsync(CaffDTO caffDTO);

    /// <summary>
    /// Makes a search based on a <see cref="CaffSearchDTO"/>.
    /// </summary>
    /// <param name="caffSearchDTO">Query.</param>
    /// <returns>Result of the query.</returns>
    Task<List<CaffDTO>> SearchCaffAsync(CaffSearchDTO caffSearchDTO);

    /// <summary>
    /// Method <c>GetCaffAsync</c> finds and return a <seealso cref="CaffDTO"/> record.
    /// </summary>
    /// <param name="id">Id of a <see cref="Caff"/> entity.</param>
    /// <returns>The requested <see cref="CaffDTO"/>.</returns>
    /// <exception cref="Exceptions.CaffNotFountException"></exception>
    Task<CaffDTO> GetCaffAsync(int id);

    /// <summary>
    /// Method <c>GetAllAsync</c> returns all the caff data stored in the persistent layer.
    /// </summary>
    /// <returns>All the Caff entries.</returns>
    Task<List<CaffDTO>> GetAllAsync();

    /// <summary>
    /// Adds a comment to a <see cref="Caff" /> entity.
    /// </summary>
    /// <param name="id">Id of a <see cref="Caff"/>.</param>
    /// <param name="comment">Main content.</param>
    /// <param name="userId">Id of the user who made the comment.</param>
    /// <returns></returns>
    Task<CaffDTO> AddCommentAsync(int id, string comment, Guid userId);
}
