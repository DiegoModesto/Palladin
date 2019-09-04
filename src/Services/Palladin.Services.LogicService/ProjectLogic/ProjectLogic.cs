using AutoMapper;
using Palladin.Data.Entity;
using Palladin.Data.Repository;
using Palladin.Services.LogicService.Interfaces;
using Palladin.Services.ViewModel.Project;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Palladin.Services.LogicService.ProjectLogic
{
    public class ProjectLogic : BaseLogic, IBaseLogic, IProjectLogic
    {
        private IMapper _mapp { get; set; }

        public ProjectLogic(IMapper map)
        {
            this._mapp = map;
        }

        public void CreateProject(ProjectViewModel model)
        {
            throw new NotImplementedException();
        }

        public ProjectViewModel GetDetailProjectById(Guid id)
        {
            using (var uow = new UnitOfWork(ConnectionString))
            {
                var project = uow._projectR.GetById(id);
                var projectViewModel = _mapp.Map<ProjectEntity, ProjectViewModel>(project);
                projectViewModel.UserName = project.User?.Login;
                projectViewModel.CustomerName = project.Customer?.Login;

                return projectViewModel;
            }
        }

        public IEnumerable<ProjectViewModel> GetProjectsGeneralList()
        {
            using (var uow = new UnitOfWork(ConnectionString))
            {
                var projects = uow._projectR.GetAllWithUsersName();
                if(projects.Count() > 0)
                {
                    var ret = new List<ProjectViewModel>();
                    foreach (var item in projects)
                    {
                        var x = _mapp.Map<ProjectEntity, ProjectViewModel>(item);
                        x.UserName = item.User?.Login;
                        x.CustomerName = item.Customer?.Login;
                        ret.Add(x);
                    }

                    return ret;
                }
                
                throw new Exception("Nenhum projeto está cadastrado.");
            }
        }

        public void RemoveById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void UpdateProject(ProjectViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
