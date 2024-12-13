using Microsoft.AspNetCore.Identity;

namespace TestProgram.Data;

public class Teacher : IdentityUser<string>
{
    public string FirstName{ get; set; }
    public string LastName{ get; set; }
    public string FullName => $"{FirstName} {LastName}";
    
    // will have a list of created tests
}