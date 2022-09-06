using System.Globalization;

namespace Northwind.Foundation;

/// <summary>
/// Represents a currency with its value and ISO4217 code.
/// </summary>
/// <remarks>
/// Only currencies with the same ISO4217 code can be compared.
/// </remarks>
public readonly struct Currency : IComparable, IComparable<Currency>
{
    const string CANNOT_COMPARE_ERROR =
        "The currencies being compared have different ISO currency codes. " +
        "It is not possible to determine an accurate comparison. " +
        "If you want to compare the values irrespective of the currency, then compare the Value properies.";
    
    /// <summary>
    /// Creates a new currency for the supplied value using the current culture.
    /// </summary>
    /// <param name="value">The value as a decimal.</param>
    public Currency(decimal value)
        : this(new RegionInfo(CultureInfo.CurrentCulture.Name).ISOCurrencySymbol, value)
    { }
        
    /// <summary>
    /// Creates a new currency using the supplied ISO currency symbol (ex. USD) and value.
    /// </summary>
    /// <param name="isoCurrencySymbol">The 3 letter ISO 4217 currency code. Ex: USD</param>
    /// <param name="value">The value as a decimal.</param>
    public Currency(string isoCurrencySymbol, decimal value)
    {
        if (string.IsNullOrWhiteSpace(isoCurrencySymbol))
            throw new ArgumentException("A 3 letter ISO 4217 currency code must be provided");
        
        ISOCurrencySymbol = isoCurrencySymbol.ToUpper();
        Value = value;
    }
        
    public static CurrencyCompareOption CompareOption { get; set; }
    
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Gets the 3 letter ISO 4217 currency code for this instance. Ex: USD
    /// </summary>
    public string ISOCurrencySymbol { get; }
        
    /// <summary>
    /// Gets the numeric value for this instance.
    /// </summary>
    public decimal Value { get; }
        
    ///<inheritdoc />
    public override string ToString()
    {
        return $"{ISOCurrencySymbol} {Value:N2}";
    }

    public string ToString(IFormatProvider formatProvider)
    {
        return Value.ToString(formatProvider);
    }
    
    public string ToString(string format)
    {
        return Value.ToString(format);
    }
    
    public string ToString(string format, IFormatProvider formatProvider)
    {
        return Value.ToString(format, formatProvider);
    }

    ///<inheritdoc />
    public int CompareTo(object? obj)
    {
        if (obj == null)
            return -1;
        return CompareTo((Currency)obj);
    }
   
    ///<inheritdoc />
    public int CompareTo(Currency other)
    {
        if (CompareOption == CurrencyCompareOption.CodeAndValue)
        {
            var isoCurrencySymbolComparison =
                string.Compare(ISOCurrencySymbol, other.ISOCurrencySymbol, StringComparison.Ordinal);
            if (isoCurrencySymbolComparison != 0) return isoCurrencySymbolComparison;
        }

        return Value.CompareTo(other.Value);
    }
    
    ///<exclude />
    public static bool operator >(Currency lhs, Currency rhs)
    {
        if (CompareOption == CurrencyCompareOption.CodeAndValue && !lhs.ISOCurrencySymbol.Equals(rhs.ISOCurrencySymbol))
            throw new InvalidOperationException(CANNOT_COMPARE_ERROR);
        return lhs.Value > rhs.Value;
    }
        
    ///<exclude />
    public static bool operator <(Currency lhs, Currency rhs)
    {
        if (CompareOption == CurrencyCompareOption.CodeAndValue && !lhs.ISOCurrencySymbol.Equals(rhs.ISOCurrencySymbol))
            throw new InvalidOperationException(CANNOT_COMPARE_ERROR);
        return lhs.Value < rhs.Value;
    }

    ///<exclude />
    public static bool operator >=(Currency lhs, Currency rhs)
    {
        if (CompareOption == CurrencyCompareOption.CodeAndValue && !lhs.ISOCurrencySymbol.Equals(rhs.ISOCurrencySymbol))
            throw new InvalidOperationException(CANNOT_COMPARE_ERROR);
        return lhs.Value >= rhs.Value;
    }
        
    ///<exclude />
    public static bool operator <=(Currency lhs, Currency rhs)
    {
        if (CompareOption == CurrencyCompareOption.CodeAndValue && !lhs.ISOCurrencySymbol.Equals(rhs.ISOCurrencySymbol))
            throw new InvalidOperationException(CANNOT_COMPARE_ERROR);
        return lhs.Value <= rhs.Value;
    }

    ///<exclude />
    public static Currency operator +(Currency lhs, Currency rhs)
    {
        return new Currency(lhs.ISOCurrencySymbol, lhs.Value + rhs.Value);
    }
    
    ///<exclude />
    public static Currency operator -(Currency lhs, Currency rhs)
    {
        return new Currency(lhs.ISOCurrencySymbol, lhs.Value - rhs.Value);
    }

    ///<exclude />
    public static Currency operator *(Currency lhs, Currency rhs)
    {
        return new Currency(lhs.ISOCurrencySymbol, lhs.Value * rhs.Value);
    }

    ///<exclude />
    public static Currency operator /(Currency lhs, Currency rhs)
    {
        return new Currency(lhs.ISOCurrencySymbol, lhs.Value / rhs.Value);
    }   
    
    ///<exclude />
    public static Currency operator %(Currency lhs, Currency rhs)
    {
        return new Currency(lhs.ISOCurrencySymbol, lhs.Value % rhs.Value);
    } 
    
    ///<exclude />
    public static implicit operator decimal(Currency currency)
    {
        return currency.Value;
    }
}