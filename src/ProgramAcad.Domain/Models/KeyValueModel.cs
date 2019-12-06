namespace ProgramAcad.Domain.Models
{
    public class KeyValueModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class KeyValueModel<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
    }
}
