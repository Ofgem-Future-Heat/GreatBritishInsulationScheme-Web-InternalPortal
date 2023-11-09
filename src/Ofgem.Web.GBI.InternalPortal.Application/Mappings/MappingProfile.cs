using AutoMapper;
using Ofgem.Web.GBI.InternalPortal.Domain.Models;

namespace Ofgem.Web.GBI.InternalPortal.Application.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<UserDetailModel, SaveExternalUserRequest>();
		}
	}
}
