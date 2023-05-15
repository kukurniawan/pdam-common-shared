using System.Collections;

// ReSharper disable CheckNamespace
namespace Pdam.Common.Shared.Data;
// ReSharper restore CheckNamespace

/// <summary>
/// 
/// </summary>
public class SortParamList : IList<SortParam>
{
    private readonly List<SortParam> _list;

    public SortParamList()
    {
        _list = new List<SortParam>();
    }

    /// <summary>
    /// create new sort param ist
    /// </summary>
    public static SortParamList Instance => new SortParamList();

    /// <inheritdoc />
    public IEnumerator<SortParam> GetEnumerator()
    {
        return _list.GetEnumerator();
    }

    /*IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }*/

    /// <inheritdoc />
    public void Add(SortParam item)
    {
        _list.Add(item);
    }

    /// <summary>
    /// add instance sort param;
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public SortParamList AddWith(SortParam item)
    {
        _list.Add(item);
        return this;
    }

    /// <summary>
    /// add instance sort param with column name and column sort
    /// </summary>
    /// <param name="columnName"></param>
    /// <param name="columnSort"></param>
    /// <returns></returns>
    public SortParamList AddWith(string columnName, string columnSort)
    {
        _list.Add(new SortParam
        {
            ColumnName = columnName, SortColumn = columnSort
        });
        return this;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public SortParamList AddBaseType(Type type)
    {
        var properties = type.GetProperties();
        foreach (var property in properties)
        {
            _list.Add(new SortParam
            {
                ColumnName = property.Name, SortColumn = property.Name
            });    
        }
        return this;
    }


    /// <summary>
    /// get array of column name
    /// </summary>
    /// <returns></returns>
    public string[] GetColumnName()
    {
        return _list.Select(x => x.ColumnName).ToArray();
    }

    /// <inheritdoc />
    public void Clear()
    {
        _list.Clear();
    }

    /// <inheritdoc />
    public bool Contains(SortParam item)
    {
        return _list.Contains(item);
    }

    /// <inheritdoc />
    public void CopyTo(SortParam[] array, int arrayIndex)
    {
        _list.CopyTo(array, arrayIndex);
    }

    /// <inheritdoc />
    public bool Remove(SortParam item)
    {
        return _list.Remove(item);
    }

    /// <inheritdoc />
    public int Count => _list.Count;

    /// <inheritdoc />
    public bool IsReadOnly => false;

    /// <inheritdoc />
    public int IndexOf(SortParam item)
    {
        return _list.IndexOf(item);
    }

    /// <inheritdoc />
    public void Insert(int index, SortParam item)
    {
        _list.Insert(index, item);
    }

    /// <inheritdoc />
    public void RemoveAt(int index)
    {
        _list.RemoveAt(index);
    }

    /// <inheritdoc />
    public SortParam this[int index]
    {
        get => _list[index];
        set => _list[index] = value;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}