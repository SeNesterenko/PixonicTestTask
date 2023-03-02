using System;
using System.Collections.Generic;

public static class Utils
{
    public static int BinarySearch(List<Planet> array, int target)
    {
        var left = 0;
        var right = array.Count - 1;
        var closest = -1;
    
        while (left <= right)
        {
            var mid = left + (right - left) / 2;
        
            if (array[mid].Rank == target)
                return mid;
        
            if (closest == -1 || Math.Abs(array[mid].Rank - target) < Math.Abs(array[closest].Rank - target))
                closest = mid;
        
            if (array[mid].Rank > target)
                right = mid - 1;
            else
                left = mid + 1;
        }
    
        return closest;
    }
}