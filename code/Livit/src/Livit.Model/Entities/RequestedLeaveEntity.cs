namespace Livit.Model.Entities
{
    using System;

    public class RequestedLeaveEntity : EntityBase<int>
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual int EmployeeId { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime AppliedFrom { get; set; }
        public virtual DateTime AppliedTo { get; set; }
        public virtual DateTime ApprovedDate { get; set; }
        public virtual EmployeeEntity Employee { get; set; }
    }
}