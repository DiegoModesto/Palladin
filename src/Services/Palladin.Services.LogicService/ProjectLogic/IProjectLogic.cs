using Palladin.Services.LogicService.Interfaces;
using Palladin.Services.ViewModel.Project;
using System;
using System.Collections.Generic;

namespace Palladin.Services.LogicService.ProjectLogic
{
    public interface IProjectLogic : IBaseLogic
    {
        IEnumerable<ProjectViewModel> GetProjectsGeneralList();
        IEnumerable<TypeAheadProjectViewModel> GetTypeAheadList();
        ProjectViewModel GetDetailProjectById(Guid id);
        ProjectViewModel CreateProject(ProjectViewModel model);
        void UpdateProject(ProjectViewModel model);
        void RemoveById(Guid id);
        IEnumerable<string> JoinProjectWithVulnerability(JoinProjectViewModel model);
    }
}
