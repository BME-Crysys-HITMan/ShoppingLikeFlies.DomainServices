using ShoppingLikeFiles.DataAccessLogic.Repository;
using ShoppingLikeFiles.DomainServices.Contract.Incoming;
using ShoppingLikeFiles.DomainServices.Exceptions;

namespace ShoppingLikeFiles.DomainServices.Service;

public class DataService : IDataService
{
    private ICaffRepository _genericRepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public DataService(ICaffRepository genericRepository, IMapper mapper, ILogger logger)
    {
        _genericRepository = genericRepository;
        _mapper = mapper;
        _logger = logger.ForContext<DataService>();
    }

    public Task<int> CreateCaffAsync(CreateCaffContractDTO contract)
    {
        var entry = _mapper.Map<Caff>(contract);
        return _genericRepository.AddAsync(entry);
    }

    public Task<bool> DeleteCaffAsync(int id)
    {
        if (id < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id));
        }

        return _genericRepository.RemoveAsync(id);
    }

    public Task UpdateCaffAsync(CaffDTO caffDTO)
    {
        var entry = _mapper.Map<Caff>(caffDTO);
        return _genericRepository.UpdateAsync(entry);
    }

    public async Task<List<CaffDTO>> GetAllAsync()
    {
        var models = await _genericRepository.GetAllAsync();
        var dto = _mapper.Map<List<CaffDTO>>(models.ToList());
        return dto;
    }

    public async Task<CaffDTO> GetCaffAsync(int id)
    {
        if (id < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id));
        }

        var entry = await _genericRepository.GetAsync(id);
        if (entry is null)
        {
            throw new CaffNotFountException(id);
        }

        return _mapper.Map<CaffDTO>(entry);
    }

    public async Task<List<CaffDTO>> SearchCaffAsync(CaffSearchDTO caffSearchDTO)
    {
        var models = await _genericRepository.GetAllAsync((e) =>
            caffSearchDTO.Caption.Contains(e.Caption)
            || ContainTags(caffSearchDTO.Tags, e.Tags)
            || caffSearchDTO.Creator.Contains(e.Creator));

        var dto = _mapper.Map<List<CaffDTO>>(models.ToList());

        return dto;
    }

    public async Task<CaffDTO> AddCommentAsync(int id, string comment, Guid userId)
    {
        _logger.Verbose("Method {method} called, with args: {id}, {comment}, {guid}", nameof(AddCommentAsync), id, comment, userId);
        if (string.IsNullOrEmpty(comment))
        {
            _logger.Debug("Comment value is null");
            throw new ArgumentNullException(nameof(comment));
        }

        if (userId == Guid.Empty)
        {
            _logger.Debug("Value of userId is: {userId}", userId);
            throw new ArgumentNullException(nameof(userId));
        }

        if (id < 0)
        {
            _logger.Debug("Value of id: {id}", id);
            throw new ArgumentOutOfRangeException(nameof(id));
        }

        await _genericRepository.AddCommentAsync(id, new Comment { Text = comment, UserId = userId });

        var caff = await _genericRepository.GetAsync(id);

        if (caff == null)
        {
            throw new CaffNotFountException(id);
        }
        _logger.Information("Comment added successfuly by {userId}", userId);

        var dto = _mapper.Map<CaffDTO>(caff);

        return dto;
    }

    private static bool ContainTags(List<string> tags, string cafftags)
    {
        bool isValid = false;
        var t = cafftags.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList();
        t.ForEach(y =>
        {
            isValid = tags.Contains(y);
            if (isValid)
                return;
        });

        return isValid;
    }
}
