using AutoMapper;
using Palladin.Data.Entity;
using Palladin.Data.Repository;
using Palladin.Data.Repository.Interfaces;
using Palladin.Services.LogicService.Interfaces;
using Palladin.Services.ViewModel.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Palladin.Services.LogicService.ProjectLogic
{
    public class ProjectLogic : BaseLogic, IBaseLogic, IProjectLogic
    {
        private IMapper _mapp { get; set; }

        public ProjectLogic(IMapper map)
        {
            this._mapp = map;
        }

        public ProjectViewModel CreateProject(ProjectViewModel model)
        {
            using (var uow = new UnitOfWork(ConnectionString))
            {
                model.Id = Guid.NewGuid();
                model.UserName = uow._userR.GetNameById(model.UserId);
                model.CustomerName = uow._userR.GetNameById(model.CustomerId);

                var project = this._mapp.Map<ProjectViewModel, ProjectEntity>(model);
                uow._projectR.Add(project);
                uow.Complete();

                return model;
            }
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
        public IEnumerable<TypeAheadProjectViewModel> GetTypeAheadList()
        {
            using(var uow = new UnitOfWork(ConnectionString))
            {
                return _mapp.Map<IEnumerable<TypeAheadProjectViewModel>>(uow._projectR.GetAll());
            }
        }

        public void RemoveById(Guid id)
        {
            using (var uow = new UnitOfWork(ConnectionString))
            {
                uow._projectR.Remove(id);
                uow.Complete();
            }
        }

        public void UpdateProject(ProjectViewModel model)
        {
            using (var uow = new UnitOfWork(ConnectionString))
            {
                //Originais do banco
                var dbProjectEntity = uow._projectR.GetById(model.Id);
                if (dbProjectEntity == null)
                    throw new Exception("Projeto não encontrada.");

                //Preenchendo a entidade
                dbProjectEntity.Subsidiary = model.Subsidiary;
                dbProjectEntity.InitialDate = model.InitialDate;
                dbProjectEntity.EndDate = model.EndDate;

                //Atualizando Vulnerability
                uow._projectR.Update(dbProjectEntity);
                uow.Complete();
            }
        }

        public IEnumerable<string> JoinProjectWithVulnerability(JoinProjectViewModel model)
        {
            using(var uow = new UnitOfWork(ConnectionString))
            {
                var lstDuplicates = new List<string>();
                //Lista dos ambientes inseridos, será 
                //utilizado para criação das MIDIAS enviadas
                var listOfProjectVulnerabilities = new List<Guid>();
                //Lista dos arquivos criados durante a 
                //vinculação dos projetos x vulnerabilidades
                var listOfFiles = new List<Guid>();

                //Insere cada ambiente inserido, gerando seu ID
                foreach (var environment in model.ListOfUris)
                {
                    //Verifica se o ambiente enviado (IP ou URL), 
                    //o Projeto 
                    //e a Vulnerabilidade já estão vinculados
                    if(uow._projectVultR.Any(x => 
                                                x.ProjectId.Equals(model.ProjectId) && 
                                                x.VulnerabilityId.Equals(model.VulnerabilityId) &&
                                                x.Environment.Equals(environment.Uri)))
                    {
                        lstDuplicates.Add(environment.Uri);
                        continue;
                    }

                    var projectVulnerabilityId = Guid.NewGuid();
                    listOfProjectVulnerabilities.Add(projectVulnerabilityId);

                    var methodProtocolId = uow._methodProtocolR
                                                .SingleOrDefault(x => x.Name.Equals(environment.Method)).Id;

                    uow._projectVultR.Add(new ProjectVulnerabilityEntity
                    {
                        Id = projectVulnerabilityId,
                        Status = Enums.ProjStatus.New,
                        Environment = environment.Uri,
                        Port = environment.Port,
                        FiledOrCookieName = environment.FormCookie,
                        Observation = string.Empty,

                        ProjectId = model.ProjectId,
                        VulnerabilityId = model.VulnerabilityId,
                        MethodProtocolId = methodProtocolId,
                        UserId = model.UserId
                    });
                }

                foreach (var media in model.ListOfFiles)
                {
                    var mediaId = Guid.NewGuid();
                    listOfFiles.Add(mediaId);

                    uow._mediaR.Add(new MediaEntity
                    {
                        Id = mediaId,
                        Name = media.Name,
                        Archive = media.Base64
                    });
                }
                //Adiciona para cada item de projeto-vulnerabilidade,
                //todas as mídias enviadas
                this.JoinMediaWithProjectVulnerability(listOfProjectVulnerabilities, listOfFiles, uow);
                //Salva na base de dados todas insersões
                uow.Complete();

                return lstDuplicates;
            }
        }

        #region Private Methods
        private void JoinMediaWithProjectVulnerability(IEnumerable<Guid> listOfPVs, IEnumerable<Guid> listOfMedias, IUnitOfWork uow)
        {
            foreach (var projVult in listOfPVs)
            {
                foreach (var media in listOfMedias)
                {
                    uow._mediaPvR.Add(new MediaPVEntity
                    {
                        MediaId = media,
                        ProjectVulnerabilityId = projVult,
                        IsDeleted = false,
                        CreatedDate = DateTime.Now
                    });
                }
            }
        }
        #endregion
    }
}
