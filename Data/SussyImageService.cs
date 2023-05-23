namespace ImageSus.Data;

public class SussyImageService
{


    public Image[] LoadedImages = new Image[]{};
    public async Task<Image[]> LoadNewImageSet()
    {
        Random rnd = new Random();
        int offset = rnd.Next();

        var images = Task.FromResult(Enumerable.Range(1, 4).Select(index => new Image
        {
            Id = (offset + index),
            IsSus = true,
            Link = $"https://placekeanu.com/320/420/{index}"
        }).ToArray());

        LoadedImages = await images;
        
        return await images; 
    }
         
}
