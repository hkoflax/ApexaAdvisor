using AdvisorManager.Application.Models;
using AutoMapper;

namespace AdvisorManagement.Api.Mappings
{
    public class DefaultApiModelMapper<TApiModel, TApplicationModel> : IApiModelMapper<TApiModel, TApplicationModel>
            where TApiModel : class, IApiModel
            where TApplicationModel : class, IApplicationModel
    {
        private readonly IMapper _mapper;

        public DefaultApiModelMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TApplicationModel MapFrom(TApiModel source)
            => _mapper.Map<TApplicationModel>(source);

        public IEnumerable<TApplicationModel> MapFrom(IEnumerable<TApiModel> source)
            => _mapper.Map<TApplicationModel[]>(source);

        public TApiModel MapTo(TApplicationModel source)
            => _mapper.Map<TApiModel>(source);

        public IEnumerable<TApiModel> MapTo(IEnumerable<TApplicationModel> source)
            => _mapper.Map<TApiModel[]>(source);
    }
}