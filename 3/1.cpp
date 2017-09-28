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
  int size = 0;
  cout << "Введите размер массива : " << endl;
  cin >> size;
  cout << "Введите значения элементов массива : " << endl;
  vector<int> array(size);
  for (int& elem : array)
  {
    cin >> elem;
  }
  int lo = 0, hi = size - 1;
  cout << "Введите значения lo и hi, 0 <= lo < hi < n, где n - размер массива : " << endl;
  cin >> lo >> hi;
  qsort(array, lo, hi);
  for (int elem : array)
  {
    cout << elem << " ";
  }

  return 0;
}
