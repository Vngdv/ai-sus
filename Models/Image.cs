using Postgrest.Models;

namespace ImageSus.Models;

public class Image: BaseModel
{
    public int Id {get; set;}
    public String? Link {get; set;}
    public bool IsSus {get; set;}

}
