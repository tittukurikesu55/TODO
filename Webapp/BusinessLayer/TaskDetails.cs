using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Webapp.Models;
using Webapp.Models.Repository;
using Webapp.ViewModel;

namespace Webapp.BusinessLayer
{
    public interface ITaskDetails
    {
        List<TaskList> getTaskLists();
        TaskList getTaskListById(int id);

        bool SaveTaskList(TaskList task);

        bool UpdateTaskList(TaskList task);

        bool DeleteTask(int id);

        List<TaskComments> getTaskComments(int id);
    }
    public class TaskDetails : ITaskDetails
    {
        dynamic config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<TaskDetail, TaskList>();
            cfg.CreateMap<TaskList, TaskDetail>();
            cfg.CreateMap<TaskComments, TaskComment>().ForMember(des => des.TaskDetail, des => des.Ignore());
            cfg.CreateMap<TaskComment, TaskComments>();
        });

        private readonly ITaskRepository _ITaskRepository;
        dynamic mapper;
        public TaskDetails(ITaskRepository ITaskRepository)
        {
            _ITaskRepository = ITaskRepository;
            mapper = new Mapper(config);
        }
        public List<TaskList> getTaskLists()
        {
            var mapper = new Mapper(config);
            return mapper.Map<List<TaskList>>(_ITaskRepository.GetTaskDetail());
        }

        public TaskList getTaskListById(int id)
        {

            return mapper.Map<TaskList>(_ITaskRepository.GetTaskDetailById(id));
        }
        public List<TaskComments> getTaskComments(int id)
        {

            return mapper.Map<List<TaskComments>>(_ITaskRepository.GetTaskComments(id));
        }

        public bool SaveTaskList(TaskList task)
        {

            var TaskModel = mapper.Map<TaskDetail>(task);
            TaskModel.CreatedTime = DateTime.Now;
            TaskModel.UpdatedTime = DateTime.Now;
            return _ITaskRepository.Save(TaskModel);

        }
        public bool UpdateTaskList(TaskList task)
        {
            var TaskModel = mapper.Map<TaskDetail>(task);
            TaskModel.UpdatedTime = DateTime.Now;
            return _ITaskRepository.Update(TaskModel);

        }
        public bool DeleteTask(int id)
        {
            return _ITaskRepository.Delete(id);
        }


    }
}