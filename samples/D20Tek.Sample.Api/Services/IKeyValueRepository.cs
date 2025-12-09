namespace D20Tek.Sample.Api.Services;

public interface IKeyValueRepository
{
    public IEnumerable<KeyValuePair<string, string>> Get();

    public KeyValuePair<string, string>? Get(string key);

    public bool Create(string key, string value);

    public bool Update(string key, string value);

    public bool Delete(string key);
}
