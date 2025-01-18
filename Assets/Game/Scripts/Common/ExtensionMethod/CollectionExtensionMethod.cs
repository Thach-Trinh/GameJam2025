using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public static class CollectionExtensionMethod
{
    public static void Iterate<T>(this T[] arr, Action<T> action)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            action?.Invoke(arr[i]);
        }
    }

    public static void Iterate<T>(this List<T> lst, Action<T> action)
    {
        for (int i = 0; i < lst.Count; i++)
        {
            action?.Invoke(lst[i]);
        }
    }

    public static void Iterate<T>(this List<T> lst, Action<int, T> action)
    {
        for (int i = 0; i < lst.Count; i++)
        {
            action?.Invoke(i, lst[i]);
        }
    }



    public static void Shuffle<T>(this List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static T TakeRandom<T>(this List<T> lst)
    {
        int index = Random.Range(0, lst.Count);
        T element = lst[index];
        lst.RemoveAt(index);
        return element;
    }

    public static T ChooseRandom<T>(this T[] arr)
    {
        int index = Random.Range(0, arr.Length);
        return arr[index];
    }

    public static T ChooseRandom<T>(this List<T> list)
    {
        int index = Random.Range(0, list.Count);
        return list[index];
    }

    public static T TakeFirst<T>(this List<T> lst)
    {
        T element = lst[0];
        lst.RemoveAt(0);
        return element;
    }

    public static T TakeLast<T>(this List<T> lst)
    {
        T element = lst[lst.Count - 1];
        lst.RemoveAt(lst.Count - 1);
        return element;
    }

    public static T GetMinElement<T>(this T[] arr, Func<T, int> selector)
    {
        int minValue = int.MaxValue;
        int minIndex = -1;
        for (int i = 0; i < arr.Length; i++)
        {
            int value = selector(arr[i]);
            if (value < minValue)
            {
                minValue = value;
                minIndex = i;
            }
        }
        return arr[minIndex];
    }

    public static void TryAdd<T1, T2>(this Dictionary<T1, T2> dict, T1 key, T2 value)
    {
        if (dict.ContainsKey(key))
            dict[key] = value;
        else
            dict.Add(key, value);
    }

    public static void TryAdd<T>(this List<T> lst, T element)
    {
        if (!lst.Contains(element))
            lst.Add(element);
    }


    public static void TryAdd<T>(this List<T> lst, List<T> elements)
    {
        for (int i = 0; i < elements.Count; i++)
            lst.TryAdd(elements[i]);
    }



    public static void ClearAndCopy<T>(this List<T> lst, List<T> other)
    {
        lst.Clear();
        for (int i = 0; i < other.Count; i++)
            lst.Add(other[i]);
    }

    public static List<T> TryGetRange<T>(this List<T> lst, int index, int count)
    {
        if (lst.Count == 0)
            return new List<T>();
        int newIndex = Mathf.Clamp(index, 0, lst.Count - 1);
        int newCount = Mathf.Clamp(count, 0, lst.Count - newIndex);
        return lst.GetRange(newIndex, newCount);
    }





    public static void DestroyAndClearInEditMode<T>(this List<T> lst) where T : Component
    {
# if UNITY_EDITOR
        for (int i = 0; i < lst.Count; i++)
        {
            if (lst[i] != null)
                Undo.DestroyObjectImmediate(lst[i].gameObject);
        }
        lst.Clear();
#endif
    }
}
