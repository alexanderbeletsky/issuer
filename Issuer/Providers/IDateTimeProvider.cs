using System;

namespace Issuer.Providers
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}