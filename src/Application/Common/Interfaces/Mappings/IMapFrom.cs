using AutoMapper;

namespace PLS.Application.Common.Interfaces.Mappings
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
