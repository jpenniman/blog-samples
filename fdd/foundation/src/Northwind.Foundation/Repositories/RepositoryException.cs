using System;
using JetBrains.Annotations;

namespace Northwind.Foundation.Repositories;

public class RepositoryException : ApplicationException
{
    public RepositoryException(string? message, Exception? innerException) : base(message, innerException)
    { }
}