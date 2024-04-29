namespace am.kon.projects.marktguru_test.Models;

/// <summary>
/// Model used to transfer data for user logging in process.
/// </summary>
public class LoginModel
{
    /// <summary>
    /// Username used to log in.
    /// </summary>
    public string Username { get; set; }
    
    /// <summary>
    /// Password used for log in.
    /// </summary>
    public string Password { get; set; }
}