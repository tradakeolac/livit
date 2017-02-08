namespace Livit.Model.Entities
{
    using System.Collections.Generic;

    public class EmployeeEntity : EntityBase<int>
    {
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public ICollection<RequestedLeaveEntity> Leaves { get; set; }
    }
}