using Abp.Application.Services.Dto;

namespace Eoss.Backend.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

