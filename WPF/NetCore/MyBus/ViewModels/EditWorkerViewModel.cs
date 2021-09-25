using System;
using System.Collections.Generic;
using MyBus.Infrastructure;
using MyBus.Models.User;

namespace MyBus.ViewModels
{
    public class EditWorkerViewModel : Base.ViewModel
    {
        public event EventHandler<EventArgs<bool>> Complete;
        private readonly Dictionary<string, object> _Values = new();
        private readonly Worker Worker;
    }
}
