using System.Threading.Tasks;
using Eoss.Backend.Configuration.Dto;

namespace Eoss.Backend.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
