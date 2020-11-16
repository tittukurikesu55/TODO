using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Webapp.ViewModel
{
    public class TaskList
    {
        public int ID { get; set; }
        [Required]
        public string Type { get; set; }

        [Required]
        public string TaskName { get; set; }
        public string AssignedUser { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DisplayName(" Add  Comments for the task")]
        public string Comments { get; set; }

        [Required]
        public string Status { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }




        public List<string> Statuslist = new List<string>() { "Todo", "Inprogress", "Done" };

        public List<string> tasktype = new List<string>() { "Task", "Bug", "Incident", "Story", "Epic", "Sub-task" };
   

    }

    public class TaskComments
    {
        public int ID { get; set; }
        public Nullable<int> TaskID { get; set; }
        public string TaskName { get; set; }
        public string Comments { get; set; }
        public System.DateTime CreatedTime { get; set; }


    }
}