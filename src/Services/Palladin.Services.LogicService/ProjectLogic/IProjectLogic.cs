using Palladin.Services.LogicService.Interfaces;
using Palladin.Services.ViewModel.Project;
using System;
using System.Collections.Generic;

namespace Palladin.Services.LogicService.ProjectLogic
{
    public interface IProjectLogic : IBaseLogic
    {
        IEnumerable<ProjectViewModel> GetProjectsGeneralList();
        ProjectViewModel GetDetailProjectById(Guid id);
        void CreateProject(ProjectViewModel model);
        void UpdateProject(ProjectViewModel model);
        void RemoveById(Guid id);
    }
}
