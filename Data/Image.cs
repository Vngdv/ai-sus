using Postgrest.Models;

namespace ImageSus.Data;

public class Image: BaseModel
{
    public int Id {get; set;}
    public String? Link {get; set;}
    public bool IsSus {get; set;}

}
