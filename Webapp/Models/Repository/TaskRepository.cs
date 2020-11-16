using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webapp.Models.Repository
{
    public interface ITaskRepository
    {
        List<TaskDetail> GetTaskDetail();
        TaskDetail GetTaskDetailById(int id);

        List<TaskComment> GetTaskComments(int id);

        bool Save(TaskDetail taskDetail);

        bool Update(TaskDetail taskDetail);

        bool Delete(int id);
    }
    public class TaskRepository : ITaskRepository
    {
        public List<TaskDetail> GetTaskDetail()
        {
            using (codedemoEntities dc = new codedemoEntities())
            {
                List<TaskDetail> taskdata = dc.TaskDetails.ToList();
                return taskdata;
            }
        }
        public List<TaskComment> GetTaskComments(int id)
        {
            using (codedemoEntities dc = new codedemoEntities())
            {
                var taskcomments = dc.TaskDetails.FirstOrDefault(x => x.ID == id).TaskComments.ToList();
                return taskcomments;
            }
        }

        public TaskDetail GetTaskDetailById(int id)
        {
            using (codedemoEntities dc = new codedemoEntities())
            {
                var taskdata = dc.TaskDetails.FirstOrDefault(x => x.ID == id);
                return taskdata;
            }
        }
        public bool Save(TaskDetail taskDetail)
        {
            int id = 0;
            using (codedemoEntities dc = new codedemoEntities())
            {
                dc.TaskDetails.Add(taskDetail);
                
                dc.SaveChanges();
                id = taskDetail.ID;               
               
            }
            using (codedemoEntities dc = new codedemoEntities())
            {
            
                dc.TaskComments.Add(new TaskComment { Comments = taskDetail.Comments, CreatedTime = DateTime.Now, TaskID = id });
                dc.SaveChanges();
            }
            return true;
        }

        public bool Update(TaskDetail taskDetail)
        {
            using (codedemoEntities dc = new codedemoEntities())
            {
                var tasks = dc.TaskDetails.FirstOrDefault(x => x.ID == taskDetail.ID);
                tasks.TaskName = taskDetail.TaskName;
                tasks.Status = taskDetail.Status;
                tasks.Description = taskDetail.Description;
              
                if (tasks.Comments != taskDetail.Comments)
                {
                    dc.TaskComments.Add(new TaskComment() { Comments = taskDetail.Comments, CreatedTime = DateTime.Now, TaskID = taskDetail.ID });
                }
                tasks.Comments = taskDetail.Comments;
                tasks.Type = taskDetail.Type;
                tasks.UpdatedTime = DateTime.Now;     
                dc.SaveChanges();
                return true;
            }



        }
        public bool Delete(int id)
        {
            using (codedemoEntities dc = new codedemoEntities())
            {
                var taskdata = dc.TaskDetails.FirstOrDefault(x => x.ID == id);
                 dc.TaskComments.RemoveRange(dc.TaskComments.Where(x => x.TaskID == id));               
                dc.TaskDetails.Remove(taskdata);
                dc.SaveChanges();
                return true;
            }
        }
    }
}