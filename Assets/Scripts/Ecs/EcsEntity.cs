using System;
using System.Collections.Generic;
using System.Linq;

public class EcsEntity
{
    private readonly Dictionary<Type, object> _components;

    public EcsEntity()
    {
        _components = new Dictionary<Type, object>();
    }

    public T GetComponent<T>() where T : struct
    {
        var hasValue = _components.TryGetValue(typeof(T), out var res);
        if (hasValue)
            return (T)res;

        return (T)_components.Values.FirstOrDefault(val => val is T);
    }

    public bool HasComponent<T>() where T : struct
    {
        var type = typeof(T);
        return _components.ContainsKey(type) && _components[type] != null || _components.Values.Any(val => val is T);
    }


    public void AddComponent(object component) => _components[component.GetType()] = component;

    public void RemoveComponent<T>() where T : struct
    {
        if (HasComponent<T>())
            _components.Remove(typeof(T));
    }

    public T TryGetComponent<T>(Func<T> defaultFunc) where T : struct
    {
        if (HasComponent<T>())
            return GetComponent<T>();

        var component = defaultFunc();
        AddComponent(component);
        return component;
    }

    public int GetComponentsCount() => _components.Values.Count;
}
