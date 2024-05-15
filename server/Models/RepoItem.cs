namespace postit_csharp.Models;
// T denotes that you will supply a type when inheriting this class, similar to a parameter
// Abstract means that this class cannot be instantiated on its own, only inherited from
public abstract class RepoItem<T>
{
  public T Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}