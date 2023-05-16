namespace ImageSus.Data;
using Supabase;

public class ImageService
{
    private Client _client;
    public ImageService(SupabaseConfiguration config)
    {
        _client = new Supabase.Client(config.Url, config.Key, new SupabaseOptions
      {
        AutoRefreshToken = true,
        AutoConnectRealtime = true,
      });
    }

    public async Task<Image[]> GetImageAsync()
    {
        await _client.InitializeAsync();
        var result = await _client.From<Image>().Get();
        return result.Models.ToArray();
    }
}
