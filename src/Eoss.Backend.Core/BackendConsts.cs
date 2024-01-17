using Eoss.Backend.Debugging;

namespace Eoss.Backend
{
    public class BackendConsts
    {
        public const string LocalizationSourceName = "Backend";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;

        public const int MinStringIdLength = 1;
        public const int MaxStringIdLength = 100;

        public const int MinNameLength = 1;
        public const int MaxNameLength = 100;

        public const int MaxDescriptionLength = 255;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "9d0dee1adad24fc4861683a141a3856d";
    }
}
