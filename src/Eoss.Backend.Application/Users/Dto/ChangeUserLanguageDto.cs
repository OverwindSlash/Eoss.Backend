using System.ComponentModel.DataAnnotations;

namespace Eoss.Backend.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}