using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Webapp.BusinessLayer;
using Webapp.Models;
using Webapp.Models.Repository;

namespace TODO_TEST
{
    [TestClass]
    public class TaskDetails_test
    {
        [TestMethod]
        public void GetTaskDetail_Test()
        {
            Mock<ITaskRepository> mock = new Mock<ITaskRepository>();
            var TaskRepository = new Mock<ITaskRepository>();
            TaskRepository.Setup(m => m.GetTaskDetail()).Returns(GetTaskDetail_Mock());
            TaskDetails taskDetails = new TaskDetails(TaskRepository.Object);
            var tasks = taskDetails.getTaskLists();
            var ff = tasks.First();
            Assert.AreEqual(tasks.Count, 2);
            Assert.AreEqual(tasks.First().Status, "ToDo");
        }
        [TestMethod]
        public void getTaskListById_test()
        {
            Mock<ITaskRepository> mock = new Mock<ITaskRepository>();
            var TaskRepository = new Mock<ITaskRepository>();
            TaskRepository.Setup(m => m.GetTaskDetailById(2)).Returns(GetTaskDetail_Mock().Last());
            TaskDetails taskDetails = new TaskDetails(TaskRepository.Object);
            var tasks = taskDetails.getTaskListById(2);   
            Assert.AreEqual(tasks.Status, "Done");
        }

        public List<TaskDetail> GetTaskDetail_Mock()
        {
            List<TaskDetail> taskDetails = new List<TaskDetail>();
            TaskDetail taskDetail = new TaskDetail();
            taskDetail.ID = 1;
            taskDetail.Status = "ToDo";
            taskDetail.TaskName = "Sample 1";
            taskDetail.Type = "Task";
            taskDetails.Add(taskDetail);
            taskDetail = new TaskDetail();
            taskDetail.ID = 2;
            taskDetail.Status = "Done";
            taskDetail.TaskName = "Sample 2";
            taskDetail.Type = "Bug";
            taskDetails.Add(taskDetail);
            return taskDetails;
        }


    }
}
