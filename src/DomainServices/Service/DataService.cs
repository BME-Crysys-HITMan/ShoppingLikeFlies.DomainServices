using DataAccessLogic.Repository;
using ShoppingLikeFiles.DomainServices.Contract;
using ShoppingLikeFiles.DomainServices.Contract.Incoming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingLikeFiles.DomainServices.Service
{
    public class DataService : IDataService
    {
        private IGenericRepository<Caff> _genericRepository;
        private readonly IMapper _mapper;
        public DataService(IGenericRepository<Caff> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<int> AsyncCreateCaff(CreateCaffContractDTO contract)
            => await _genericRepository.AddAsync(_mapper.Map<Caff>(contract));

        public async Task<bool> AsyncDeleteCaff(int id) 
            => await _genericRepository.RemoveAsync(id);
        public async Task AsyncUpdateCaff(CaffDTO caffDTO)
            => await _genericRepository.UpdateAsync(_mapper.Map<Caff>(caffDTO));
        public async Task<List<CaffDTO>> AsyncGetAll() 
            => _mapper.Map<List<CaffDTO>>(await _genericRepository.GetAllAsync());

        public async Task<CaffDTO> AsyncGetCaff(int id)
            => _mapper.Map<CaffDTO>(await _genericRepository.GetAsync(id));

        public async Task<List<CaffDTO>> AsyncSearchCaff(CaffSearchDTO caffSearchDTO)
            => _mapper.Map<List<CaffDTO>>(await _genericRepository.GetAsync(x =>
            x.Captions.Any(y => caffSearchDTO.Caption.Contains(y.Text)) &&
            x.Creator.Contains(caffSearchDTO.Creator) &&
            x.Tags.Any(y => caffSearchDTO.Tags.Contains(y.CaffTag.Tag))));
    }
}
