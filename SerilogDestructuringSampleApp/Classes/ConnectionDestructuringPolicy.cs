using System.Data;
using Serilog.Core;
using Serilog.Events;

namespace SerilogDestructuringSampleApp.Classes;

/// <summary>
/// Implements a custom destructuring policy for handling objects of type <see cref="System.Data.IDbConnection"/>.
/// </summary>
/// <remarks>
/// This class is used to customize the way <see cref="System.Data.IDbConnection"/> objects are logged by Serilog.
/// It extracts specific properties such as <c>ConnectionString</c> and <c>ConnectionTimeout</c> for logging purposes.
/// </remarks>
public class ConnectionDestructuringPolicy : IDestructuringPolicy
{
    public bool TryDestructure(object value, ILogEventPropertyValueFactory propertyValueFactory, out LogEventPropertyValue result)
    {
        if (value is IDbConnection c)
        {
            // properties values to be logged
            result = propertyValueFactory.CreatePropertyValue(new { c.ConnectionString, c.ConnectionTimeout });
            return true;
        }

        result = null;

        return false;

    }
}