namespace vue_expenses_api.Dtos
{
    public class ProfileDetailsDto
    {
        protected ProfileDetailsDto()
        {
        }

        public ProfileDetailsDto(
            string firstName,
            string lastName,
            string fullName)
        {
            FirstName = firstName;
            LastName = lastName;
            FullName = fullName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
    }
}