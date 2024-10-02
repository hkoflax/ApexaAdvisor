using AdvisorManager.Application.Models;

namespace AdvisorManagement.Api.Mappings
{
    public interface IApiModelMapper<TApiModel, TApplicationModel>
        where TApiModel : class, IApiModel
        where TApplicationModel : class, IApplicationModel
    {
        TApiModel MapTo(TApplicationModel source);
        IEnumerable<TApiModel> MapTo(IEnumerable<TApplicationModel> source);
        TApplicationModel MapFrom(TApiModel source);
        IEnumerable<TApplicationModel> MapFrom(IEnumerable<TApiModel> source);
    }
}
