namespace Week11;

using System;
using System.Collections;

public class MyDictionary<K, V> : IEnumerable where K:notnull
{
    private K[] keys;
    private V[] values;
    private int size;
    private int capacity;

    public MyDictionary(int initialCapacity = 8)
    {
        capacity = initialCapacity;
        size = 0;
        keys = new K[capacity]!;
        values = new V[capacity]!;
    }

    // Add a key-value pair to the dictionary
    public void Add(K key, V value)
    {
        // Check if the key already exists, update its value if it does
        for (int i = 0; i < size; i++)
        {
            if (keys[i].Equals(key))
            {
                values[i] = value;
                return;
            }
        }

        // If the dictionary is full, resize it
        if (size == capacity)
        {
            Resize();
        }

        // Add new key and value
        keys[size] = key;
        values[size] = value;
        size++;
    }

    // Get the value associated with a key
    public V Get(K key)
    {
        for (int i = 0; i < size; i++)
        {
            if (keys[i].Equals(key))
            {
                return values[i];
            }
        }
        throw new KeyNotFoundException("The key was not found.");
    }

    // Check if the dictionary contains a particular key
    public bool ContainsKey(K key)
    {
        for (int i = 0; i < size; i++)
        {
            if (keys[i].Equals(key))
            {
                return true;
            }
        }
        return false;
    }

    // Resize the arrays when the dictionary is full
    private void Resize()
    {
        capacity *= 2;
        K[] newKeys = new K[capacity];
        V[] newValues = new V[capacity];

        for (int i = 0; i < size; i++)
        {
            newKeys[i] = keys[i];
            newValues[i] = values[i];
        }

        keys = newKeys;
        values = newValues;
    }

    // Get the current number of items in the dictionary
    public int Count => size;

    public IEnumerator GetEnumerator()
    {
        for (int index = 0; index < size; index++)
        {
            yield return keys[index];
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        MyDictionary<string, int> myDict = new MyDictionary<string, int>();

        // Add some key-value pairs
        myDict.Add("Apple", 3);
        myDict.Add("Banana", 5);
        myDict.Add("Orange", 2);

        Console.WriteLine("Dictionary contents:");
        foreach (var key in myDict)
        {
            Console.WriteLine($"{key} => {myDict.Get((string)key)}");
        }

        // Test ContainsKey
        Console.WriteLine("\nContainsKey tests:");
        Console.WriteLine($"Contains 'Banana': {myDict.ContainsKey("Banana")}");
        Console.WriteLine($"Contains 'Mango': {myDict.ContainsKey("Mango")}");

        // Test updating a value
        myDict.Add("Apple", 10);
        Console.WriteLine($"\nUpdated 'Apple' value: {myDict.Get("Apple")}");

        // Test exception for missing key
        try
        {
            Console.WriteLine("\nTrying to get value for 'Pineapple'...");
            int value = myDict.Get("Pineapple");
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
        }

        // Check the count
        Console.WriteLine($"\nTotal items: {myDict.Count}");
    }
}