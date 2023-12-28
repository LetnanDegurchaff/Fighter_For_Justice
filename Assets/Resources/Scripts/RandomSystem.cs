using System;

public static class RandomSystem
{
    private static Random _random = new Random();

    public static int Range(int min, int max) => _random.Next(min, max + 1);
}