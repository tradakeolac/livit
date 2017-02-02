namespace Livit.Model.Entities
{
    public class RequestedLeaveEntity : EntityBase<int>
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual int EmployeeId { get; set; }
        public virtual EmployeeEntity Employee { get; set; }
    }
}
