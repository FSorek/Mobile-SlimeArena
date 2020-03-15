using System;

public interface ICanRestore
{
    event Action<int> OnRestore;
    void Restore(int amount);
}