#include <iostream>
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
    int size = hi - lo + 1;
    if (size < 10)
    {
        insertionSort(array, lo, hi);
    }
    else
    {
        if (lo < hi)
        {
            int p = partition(array, lo, hi);
            qsort(array, lo, p - 1);
            qsort(array, p, hi);
        }
    }
}

int main()
{
    int t = 0;
    vector<int> array;
    while (cin >> t)
    {
        array.push_back(t);
    }
    int size = array.size();
    qsort(array, 0, size - 1);

    int maxCount = 0;
    int maxCountNumber = array[0];
    for (int i = 0; i < size; ++i)
    {
        int number = array[i];
        int count = 0;
        while (i < size && array[i] == number)
        {
            ++count;
            ++i;
        }
        if (count > maxCount)
        {
            maxCount = count;
            maxCountNumber = number;
        }
    }

    cout << maxCountNumber << endl;

    return 0;
}
