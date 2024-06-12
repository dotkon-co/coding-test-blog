namespace BlogSystem.Application.Dto
{
    /// <summary>
    /// Represents a data transfer object for user login.
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// Gets or sets the username for login.
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// Gets or sets the password for login.
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to remember the user's login.
        /// </summary>
        public bool RememberMe { get; set; }
    }
}