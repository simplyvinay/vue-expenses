namespace vue_expenses_api.Dtos
{
    public class UserDetailsDto
    {
        protected UserDetailsDto()
        {
        }

        public UserDetailsDto(
            string systemName,
            bool useDarkMode)
        {
            SystemName = systemName;
            UseDarkMode = useDarkMode;
            Theme = useDarkMode ? "dark" : "light";
        }

        public string SystemName { get; set; }
        public bool UseDarkMode { get; set; }
        public string Theme { get; set; }
    }
}