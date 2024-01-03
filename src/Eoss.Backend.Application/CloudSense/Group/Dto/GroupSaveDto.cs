using Abp.AutoMapper;
using Abp.Domain.Entities;
using System.Data.Common;

namespace Eoss.Backend.CloudSense.Group.Dto
{
    [AutoMapTo(typeof(GroupByBehavior))]
    public class GroupSaveDto : Entity<int>
    {
    }
}
