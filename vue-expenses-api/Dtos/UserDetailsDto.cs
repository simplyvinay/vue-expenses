namespace vue_expenses_api.Dtos
{
    public class UserDetailsDto
    {
        protected UserDetailsDto()
        {
        }

        public UserDetailsDto(
            bool useDarkMode)
        {
            UseDarkMode = useDarkMode;
            Theme = useDarkMode ? "dark" : "light";
        }

        public bool UseDarkMode { get; set; }
        public string Theme { get; set; }
    }
}