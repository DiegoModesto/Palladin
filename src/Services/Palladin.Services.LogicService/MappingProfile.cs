using AutoMapper;
using Microsoft.EntityFrameworkCore.Internal;
using Palladin.Data.Entity;
using Palladin.Services.LogicService.AuthenticationLogic;
using Palladin.Services.ViewModel.Project;
using Palladin.Services.ViewModel.User;
using Palladin.Services.ViewModel.Vulnerability;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Palladin.Services.LogicService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LoginPasswordViewModel, UserEntity>();
            CreateMap<UserEntity, LoginPasswordViewModel>();

            
            CreateMap<CustomerViewModel, UserEntity>()
                .ForMember(x => x.Login, y => y.MapFrom(z => z.CustomerName));
            CreateMap<UserEntity, CustomerViewModel>()
                .ForMember(x => x.CustomerName, y => y.MapFrom(z => z.Login));

            CreateMap<UserViewModel, UserEntity>()
                .ForMember(x => x.Login, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.UserType, y => y.MapFrom(z => !string.IsNullOrWhiteSpace(z.Type)
                                                            ? ((Enums.UserType)int.Parse(z.Type))
                                                            : Enums.UserType.Client));
            CreateMap<UserEntity, UserViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Login))
                .ForMember(x => x.Type, y => y.MapFrom(z => Enum.GetName(typeof(Enums.UserType), z.UserType)));

            #region Mapp of Menus

            CreateMap<MenuViewModel, MenuEntity>();
            CreateMap<MenuEntity, MenuViewModel>();

            CreateMap<MenuItemViewModel, MenuItemEntity>();
            CreateMap<MenuItemEntity, MenuItemViewModel>();

            CreateMap<List<MenuItemViewModel>, List<MenuItemEntity>>();
            CreateMap<List<MenuItemEntity>, List<MenuItemViewModel>>();

            #endregion

            #region Mapp for Vulnerabilities

            CreateMap<VulnerabilityViewModel, VulnerabilityEntity>()
                .ForMember(x => x.References, y => y.MapFrom(z => z.References.Join(",")))
                .ForMember(x => x.Tags, y => y.MapFrom(z => z.Tags.Join(",")))
                .ForMember(x => x.RiskFactor, y => y.MapFrom(z => !string.IsNullOrWhiteSpace(z.RiskFactorType)
                                                                ? Enum.Parse(typeof(Enums.RiskFactor), z.RiskFactorType)
                                                                : Enums.RiskFactor.Critical));

            CreateMap<VulnerabilityEntity, VulnerabilityViewModel>()
                .ForMember(x => x.References, y => y.MapFrom(z => z.References.Split(",", StringSplitOptions.RemoveEmptyEntries)))
                .ForMember(x => x.Tags, y => y.MapFrom(z => z.Tags.Split(",", StringSplitOptions.RemoveEmptyEntries)))
                .ForMember(x => x.RiskFactorType, y => y.MapFrom(z => Enum.GetName(z.RiskFactor.GetType(), z.RiskFactor)));

            CreateMap<VulnerabilityLangViewModel, VulnerabilityLangEntity>();
            CreateMap<VulnerabilityLangEntity, VulnerabilityLangViewModel>();

            CreateMap<VulnerabilitySimpleListViewModel, VulnerabilityEntity>()
                .ForMember(x => x.ProjectType, y => y.MapFrom(z => !string.IsNullOrWhiteSpace(z.ProjectType)
                                                                ? Enum.Parse(typeof(Enums.ProjType), z.ProjectType)
                                                                : Enums.ProjType.Web))
                .ForMember(x => x.RiskFactor, y => y.MapFrom(z => !string.IsNullOrWhiteSpace(z.RiskFactorType)
                                                                ? Enum.Parse(typeof(Enums.RiskFactor), z.RiskFactorType)
                                                                : Enums.RiskFactor.Critical));
            CreateMap<VulnerabilityEntity, VulnerabilitySimpleListViewModel>()
                .ForMember(x => x.ProjectType, y => y.MapFrom(z => Enum.GetName(typeof(Enums.ProjType), z.ProjectType)))
                .ForMember(x => x.RiskFactorType, y => y.MapFrom(z => Enum.GetName(typeof(Enums.RiskFactor), z.RiskFactor)));

            CreateMap<TypeAheadVulnerabilityViewModel, VulnerabilityEntity>();
            CreateMap<VulnerabilityEntity, TypeAheadVulnerabilityViewModel>();

            #endregion

            #region Mapp for Projects

            CreateMap<ProjectViewModel, ProjectEntity>()
                .ForMember(x => x.ProjectType, y => y.MapFrom(z => !string.IsNullOrWhiteSpace(z.ProjectType)
                                                                ? Enum.Parse(typeof(Enums.ProjType), z.ProjectType)
                                                                : Enums.ProjType.Web));
            CreateMap<ProjectEntity, ProjectViewModel>()
                .ForMember(x => x.ProjectType, y => y.MapFrom(z => Enum.GetName(typeof(Enums.ProjType), z.ProjectType)));

            CreateMap<TypeAheadProjectViewModel, ProjectEntity>();
            CreateMap<ProjectEntity, TypeAheadProjectViewModel>();

            #endregion
        }
    }
}
