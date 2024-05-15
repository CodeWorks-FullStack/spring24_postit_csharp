
namespace postit_csharp.Models;

public class Account : Profile
{
  // All of these members are now inherited from profile class
  // public string Id { get; set; }
  // public string Name { get; set; }
  // public string Picture { get; set; }
  public string Email { get; set; }

}
