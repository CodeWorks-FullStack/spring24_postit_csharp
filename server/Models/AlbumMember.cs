namespace postit_csharp.Models;

public class AlbumMember : RepoItem<int>
{
  // Id, CreatedAt, Updated at inherited from RepoItem
  public string AccountId { get; set; }
  public int AlbumId { get; set; }
}