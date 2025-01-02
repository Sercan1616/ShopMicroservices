namespace Catalog.API.Exceptions;

public class CategoryNotFoundException : Exception
{
    public CategoryNotFoundException() : base("Categories not Found")
    {
    }
}
