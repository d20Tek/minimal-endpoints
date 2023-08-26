//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
namespace D20Tek.Sample.Api.Services;

internal sealed class KeyValueRepository : IKeyValueRepository
{
    private static Dictionary<string, string> _keyValuePairs = new();

    public IEnumerable<KeyValuePair<string, string>> Get()
    {
        return _keyValuePairs.ToList();
    }

    public KeyValuePair<string, string>? Get(string key)
    {
        return _keyValuePairs.FirstOrDefault(kv => kv.Key == key);
    }

    public bool Create(string key, string value)
    {
        if (_keyValuePairs.ContainsKey(key))
        {
            return false;
        }

        _keyValuePairs.Add(key, value);
        return true;
    }

    public bool Update(string key, string value)
    {
        if (_keyValuePairs.ContainsKey(key) is false)
        {
            return false;
        }

        _keyValuePairs[key] = value;
        return true;
    }

    public bool Delete(string key)
    {
        if (_keyValuePairs.ContainsKey(key) is false)
        {
            return false;
        }

        _keyValuePairs.Remove(key);
        return true;
    }
}
