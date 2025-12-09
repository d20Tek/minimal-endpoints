namespace D20Tek.Sample.Api.Services;

internal sealed class KeyValueRepository : IKeyValueRepository
{
    private static readonly Dictionary<string, string> _keyValuePairs = [];

    public IEnumerable<KeyValuePair<string, string>> Get() => [.. _keyValuePairs];

    public KeyValuePair<string, string>? Get(string key) => _keyValuePairs.FirstOrDefault(kv => kv.Key == key);

    public bool Create(string key, string value)
    {
        if (_keyValuePairs.ContainsKey(key)) return false;

        _keyValuePairs.Add(key, value);
        return true;
    }

    public bool Update(string key, string value)
    {
        if (_keyValuePairs.ContainsKey(key) is false) return false;

        _keyValuePairs[key] = value;
        return true;
    }

    public bool Delete(string key)
    {
        if (_keyValuePairs.ContainsKey(key) is false) return false;

        _keyValuePairs.Remove(key);
        return true;
    }
}
