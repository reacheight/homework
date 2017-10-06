#include <iostream>
#include <vector>
#include <algorithm>
#include <clocale>

using namespace std;

int partition(vector<int>& array, int lo, int hi)
{
    int pivot = array[lo];
    int i = lo;
    int j = hi;
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
    setlocale(LC_ALL, "Russian");

    int t = 0;
    vector<int> array;
    cout << "Введите значения элементов в массиве : " << endl;
    while (cin >> t)
    {
        array.push_back(t);
    }

    int size = array.size();
    qsort(array, 0, size - 1);

    int maxCount = 0;
    int maxCountNumber = array[0];
    int i = 0;
    while (i < size)
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

    cout << "Наиболее часто встречающийся элемент в массиве - " << maxCountNumber << endl;

    return 0;
}
