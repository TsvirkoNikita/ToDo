namespace ToDo.Application.Common.Exceptions
{
    public class NotFoundException(int id) : Exception($"Queried object entity was not found, Id: {id}")
    {
    }
}
