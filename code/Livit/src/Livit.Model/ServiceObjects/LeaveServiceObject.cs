namespace Livit.Model.ServiceObjects
{
    using System;

    public class LeaveServiceObject
    {
        public string EmployeeEmail { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Id { get; set; }
    }
}