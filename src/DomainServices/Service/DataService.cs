using ShoppingLikeFiles.DataAccessLogic.Repository;
using ShoppingLikeFiles.DomainServices.Contract.Incoming;

namespace ShoppingLikeFiles.DomainServices.Service;

public class DataService : IDataService
{
    private IGenericRepository<Caff> _genericRepository;
    private readonly IMapper _mapper;
    //private readonly ILogger //_logger;

    public DataService(IGenericRepository<Caff> genericRepository, IMapper mapper)//, ILogger logger)
    {
        _genericRepository = genericRepository;
        _mapper = mapper;
        ////_logger = logger.ForContext<DataService>();
    }

    public async Task<int> CreateCaffAsync(CreateCaffContractDTO contract)
        => await _genericRepository.AddAsync(_mapper.Map<Caff>(contract));

    public async Task<bool> DeleteCaffAsync(int id)
        => await _genericRepository.RemoveAsync(id);
    public async Task UpdateCaffAsync(CaffDTO caffDTO)
        => await _genericRepository.UpdateAsync(_mapper.Map<Caff>(caffDTO));
    public async Task<List<CaffDTO>> GetAllAsync()
        => _mapper.Map<List<CaffDTO>>(await _genericRepository.GetAllAsync());

    public async Task<CaffDTO> GetCaffAsync(int id)
        => _mapper.Map<CaffDTO>(await _genericRepository.GetAsync(id));

    public async Task<List<CaffDTO>> SearchCaffAsync(CaffSearchDTO caffSearchDTO)
        => _mapper.Map<List<CaffDTO>>(await _genericRepository.GetAsync(x =>
        x.Captions.Any(y => caffSearchDTO.Caption.Contains(y.Text)) &&
        x.Creator.Contains(caffSearchDTO.Creator) &&
        x.Tags.Any(y => caffSearchDTO.Tags.Contains(y.CaffTag.Tag))));
}
