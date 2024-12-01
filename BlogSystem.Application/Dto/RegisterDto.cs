namespace BlogSystem.Application.Dto
{
    /// <summary>
    /// Represents a data transfer object for user registration.
    /// </summary>
    public class RegisterDto
    {
        /// <summary>
        /// Gets or sets the username for registration.
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// Gets or sets the email address for registration.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the password for registration.
        /// </summary>
        public string? Password { get; set; }
    }
}