using System.ComponentModel.DataAnnotations;

namespace ValidationServiceDotNetCoreSample.Models
{
    /// <summary>
    /// The authentication model
    /// </summary>
    public class AuthenticateModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}