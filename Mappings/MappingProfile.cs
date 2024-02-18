using AutoMapper;
using TaskSched.Data.Models;
using TaskSched.ViewModels;
using Task = TaskSched.Data.Models.Task;

namespace TaskSched.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Project, ProjectViewModel>()
           .ReverseMap()
           .ForMember(dest => dest.ProjectId, opt => opt.Ignore())
           .ForMember(dest => dest.DateCreated, opt => opt.Ignore());

            CreateMap<Task, TaskViewModel>()
            .ReverseMap()
            .ForMember(dest => dest.TaskId, opt => opt.Ignore())
            .ForMember(dest => dest.ProjectId, opt => opt.Ignore())
            .ForMember(dest => dest.DateCreate, opt => opt.Ignore());

            CreateMap<CompanyDetails, CompanyDetailsViewModel>();

			CreateMap<ProjectDisplayState, TaskSortViewModel>()
			.ReverseMap()
			.ForMember(dest => dest.ProjectId, opt => opt.Ignore());
		}
    }
}
