using System.Runtime.Serialization;

namespace Northwind.Foundation;

public abstract class ScalarValue<T> : IEquatable<ScalarValue<T>>, ISerializable
    where T : notnull
{
    protected ScalarValue(T value)
    {
        Value = value;
    }
    
    public T Value { get; }  //Immutable
    
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ScalarValue<T>)obj);
    }

    public bool Equals(ScalarValue<T> other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return EqualityComparer<T>.Default.Equals(Value, other.Value);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<T>.Default.GetHashCode(Value);
    }

    public override string ToString()
    {
        return Value.ToString() ?? this.GetType().Name;
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        
    }

    public static implicit operator T(ScalarValue<T> scalar)
    {
        return scalar.Value;
    }
}