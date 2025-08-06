namespace WarehouseManagement.Application.Common.Exceptions;

public class InUseException : ApplicationException
{
    public InUseException(string message) : base(message)
    {}
}