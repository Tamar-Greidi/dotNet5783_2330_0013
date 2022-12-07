using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;

/// <summary>
/// Crud function interface.
/// </summary>
/// <typeparam name="T"></typeparam>

public interface ICrud<T>
{
    public int Add(T value);
    public T Get(int value);
    public IEnumerable<T?> GetAll();
    public int Update(T value);
    public void Delete(int value);
}