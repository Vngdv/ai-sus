namespace ImageSus.Data;
using Supabase;
using static Postgrest.Constants;

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

    public async Task<Image[]> GetImageAsync(int count = 1, bool isSus = false)
    {
        // Oke also das Problem ist, dass PostgREST keine functions calls erlaubt
        // macht natürlich sinn, da es ein Angriffsvektor sein könnte.
        // Deswegen müssen wir das hier anders lösen und fetchen im Schritt 1 alle IDs
        // und dann im zweiten Sch^ritt die Bilder.
        // Das ist natürlich nicht so performant, aber wir haben ja nur ca 7000 Bilder.
        // Falls wir mal scalieren sollten muss hier auf ein RPC zu Supabase gewechselt werden.
        await _client.InitializeAsync();
        var allIds = await _client.From<Image>()
          .Select("id")
          .Filter("IsSus", Operator.Equals, isSus.ToString())
          .Get();

        var random = new Random();
        var idsToFetch = allIds.Models
          .Select(x => x.Id)
          .OrderBy(x => random.Next())
          .Take(count)
          // Cast to object because the SDK is not generic :c
          .Cast<object>()
          .ToList();

        var result = await _client.From<Image>()
          .Filter("id", Operator.In, idsToFetch)
          .Get();

        return result.Models.ToArray();
    }
}