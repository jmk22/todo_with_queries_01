using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace ToDoList
{

    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => {
              List<string> AllTasks = Task.All();
              return View["index.cshtml", AllTasks];
            };
            Post["/test_add"] = _ => {
              Task.Save();
              return View["test_add.cshtml"];
            };
        }
    }
}
