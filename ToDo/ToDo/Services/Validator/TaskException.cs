using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDo.Services.Validator
{
    public class TaskException : Exception
    {
        public TaskException(string msg) : base(msg) { }
    }
}