using Microsoft.JSInterop;

public interface ICookie
{
    public Task SetValue(string key, string value, int? days = null);
    public Task<string> GetValue(string key, string def = "");
    public Task DeleteAll();
}

public class Cookie: ICookie
{
    readonly IJSRuntime JSRuntime;
    string expires = "";

    public int ExpireDays
    {
        set => expires = DateToUTC(value);
    }

    private static string DateToUTC(int days) => DateTime.Now.AddDays(days).ToString();

    public Cookie(IJSRuntime jSRuntime)
    {
        this.JSRuntime = jSRuntime;
        ExpireDays = 300;
    }

    public async Task SetValue(string key, string value, int? days = null)
    {
        var curExp = (days != null) ? (days > 0 ? DateToUTC(days.Value) : ""): expires;
        await SetCookie($"{key}={value}; expires={curExp}; path=/");
    }

    public async Task<string> GetValue(string key, string def = "")
    {
        var cValue = await GetCookie();
        if (string.IsNullOrEmpty(cValue)) return def;

        var vals = cValue.Split(';');
        foreach ( var v in vals )
        {
            if (!string.IsNullOrEmpty(v) && v.IndexOf('=') > 0)
            {
                if (v.Substring(0, v.IndexOf("=")).Trim().Equals(key, StringComparison.OrdinalIgnoreCase))
                    return v.Substring(v.IndexOf("=") + 1);
            }
        }
        return def;
    }

    private async Task SetCookie(string value)
    {
        await JSRuntime.InvokeVoidAsync("eval", $"document.cookie = \"{value}\"");
    }

    private async Task<string> GetCookie()
    {
        return await JSRuntime.InvokeAsync<string>("eval", $"document.cookie");
    }

    public async Task DeleteAll()
    {
        var cValue = await GetCookie();
        if (!string.IsNullOrEmpty(cValue))
        {
            var vals = cValue.Split(";"); 
            foreach(var v in vals)
            {
                if (!string.IsNullOrEmpty(v) && v.IndexOf('=') > 0)
                {
                    string key = v.Substring(0, v.IndexOf("=")).Trim();
                    await SetCookie($"{key}=''; expires= Thu, 01 Jan 1970 00:00:00 GMT; path=/");
                }
            }
        }
    }
}
