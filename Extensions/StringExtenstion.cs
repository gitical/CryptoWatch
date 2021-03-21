using System.Text.Json;

public static class StringExtension
{
    public static T Deserialize<T>(this string json)
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(json, options);
        }
        catch (JsonException e)
        {

        }

        return default(T);
    }
}