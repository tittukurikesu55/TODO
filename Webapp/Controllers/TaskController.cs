using System;
using System.Collections.Generic;
using System.Linq;
using Webapp.ViewModel;
using System.Web.Mvc;
using Webapp.Models;
using AutoMapper;
using Webapp.BusinessLayer;

namespace Webapp.Controllers
{
    public class TaskController : Controller
    {


        private readonly ITaskDetails _ITaskDetails;

        public TaskController(ITaskDetails ITaskDetails)
        {
            _ITaskDetails = ITaskDetails;
        }
        // GET: Task
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return Redirect("../Home/Login");
            }

            return View(_ITaskDetails.getTaskLists());

        }

        public ActionResult AddTask()
        {
            if( Session["UserName"]==null)
            {
                return Redirect("../Home/Login");
            }
            var TaskViewModel = new TaskList();
            TaskViewModel.AssignedUser = Session["UserName"].ToString();
            return View("Create", TaskViewModel);
        }

        [HttpPost]
        public ActionResult Save(TaskList task)
        {
            if (!ModelState.IsValid)
            {
                task.AssignedUser = Session["UserName"].ToString();
                return View("Create", task);
            }

            if (task.ID == 0)
            {

                task.AssignedUser = Session["UserName"].ToString();
                _ITaskDetails.SaveTaskList(task);

            }

            else
            {
                task.AssignedUser = Session["UserName"].ToString();
                _ITaskDetails.UpdateTaskList(task);

            }


            return RedirectToAction("Index", "Task");
        }


        public ActionResult Delete(int id)
        {
            _ITaskDetails.DeleteTask(id);
            return RedirectToAction("Index", "Task");

        }
        public ActionResult Edit(int id)
        {
            return View("Create", _ITaskDetails.getTaskListById(id));

        }

        public ActionResult ViewComments(int id,string Name)
        {
            var ff = _ITaskDetails.getTaskComments(id);
            ff.First().TaskName = Name;
            return View("Commenthistory", ff);

        }

    }
}