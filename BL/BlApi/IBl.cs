namespace BlApi;

/// <summary>
/// BL's interface.
/// </summary>
public interface IBl    
{
    public IProduct Product { get; }
    public ICart Cart { get; }
    public IOrder Order { get; }
}
