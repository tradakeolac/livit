namespace Livit.Web.ViewModel
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class LeaveRequestViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string EmployeeEmail { get; set; }

        [Required]
        public string Summary { get; set; }

        public string Description { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime From { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime To { get; set; }
    }

    public class LeaveResponseViewModel : LeaveRequestViewModel
    {
        public string Id { get; set; }
    }
}