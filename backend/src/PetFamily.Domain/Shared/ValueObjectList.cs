using System.Collections;

namespace PetFamily.Domain.Shared;

public class ValueObjectList<T> : IReadOnlyList<T>
{
    public IReadOnlyList<T> Values { get; }

    public T this[int index] => Values[index];

    public int Count => Values.Count;

    private ValueObjectList()
    {
    }

    public ValueObjectList(IEnumerable<T> values)
    {
        Values = values.ToList();
    }

    public IEnumerator<T> GetEnumerator()
        => Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => Values.GetEnumerator();

    public static implicit operator ValueObjectList<T>(List<T> values) => new(values);

    public static implicit operator List<T>(ValueObjectList<T> values) => values.Values.ToList();
}