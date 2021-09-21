using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Dice
{
    public static int RollD6()
    {
        return (int)(Random.value * 6) + 1;
    }

    public static int[] RollD6Multiple(int number)
    {
        int[] rolls = new int[number];
        for (int i = 0; i < rolls.Length; i++)
        {
            rolls[i] = (int)(Random.value * 6) + 1;
        }
        return rolls;
    }
}
