using AutoMapper;

namespace JurisTempus.Profiles
{
    public class JurisProfiles : Profile
    {
      public JurisProfiles()
      {
        CreateMap<Data.Entities.Client, ViewModels.ClientViewModel>()
          .ForMember(vm => vm.ContactName, options => options.MapFrom(c => c.Contact))
          .ForMember(vm => vm.Address1, options => options.MapFrom(c => c.Address.Address1))
          .ForMember(vm => vm.Address2, options => options.MapFrom(c => c.Address.Address2))
          .ForMember(vm => vm.CityTown, options => options.MapFrom(c => c.Address.CityTown))
          .ForMember(vm => vm.StateProvince, options => options.MapFrom(c => c.Address.StateProvince))
          .ForMember(vm => vm.PostalCode, options => options.MapFrom(c => c.Address.PostalCode))
          .ForMember(vm => vm.Country, options => options.MapFrom(c => c.Address.Country)).ReverseMap();

        CreateMap<Data.Entities.Case, ViewModels.CaseViewModel>().ReverseMap();
        CreateMap<Data.Entities.TimeBill, ViewModels.TimeBillViewModel>()
          .ForMember(vm => vm.CaseId, options => options.MapFrom(entity => entity.Case.Id))
          .ForMember(vm => vm.EmployeeId, options => options.MapFrom(entity => entity.Employee.Id)).ReverseMap();
      }
    }
}
