#include "qsort.h"

#include <vector>
#include <algorithm>

using namespace std;

int partition(vector<int>& array, int lo, int hi)
{
    int pivot = array[lo];
    int i = lo, j = hi;
    while (i <= j)
    {
        while (array[i] < pivot)
        {
            ++i;
        }
        while (array[j] > pivot)
        {
            --j;
        }
        if (i <= j)
        {
            swap(array[i], array[j]);
            ++i;
            --j;
        }
    }

    return i;
}

void insertionSort(vector<int>& array, int lo, int hi)
{
    for (int i = lo + 1; i <= hi; ++i)
    {
        int key = i;
        while (key > 0 && array[key] < array[key - 1])
        {
            swap(array[key], array[key - 1]);
            --key;
        }
    }
}

void qsort(vector<int>& array, int lo, int hi)
{
    int size = lo - hi;
    if (size < 10)
    {
        insertionSort(array, lo, hi);
        return;
    }

    if (lo < hi)
    {
        int p = partition(array, lo, hi);
        qsort(array, lo, p - 1);
        qsort(array, p, hi);
    }
}
