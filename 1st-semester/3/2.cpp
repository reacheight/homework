#include <iostream>
#include <vector>
#include <algorithm>
#include <cmath>

using namespace std;

int randInt()
{
  const int MAX_RAND_INT = pow(10, 9) + 1;
  return rand() % MAX_RAND_INT;
}

bool isContained(const vector<int>& array, int size, int value)
{
  int left = 0;
  int right = size - 1;

  if (size == 0 || array[left] > value || array[right] < value)
  {
    return false;
  }

  while (left != right)
  {
    int mid = (left + right) / 2;

    if (value <= array[mid])
    {
      right = mid;
    }
    else
    {
      left = mid + 1;
    }
  }

  return value == array[right];
}

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
  int n = 0;
  int k = 0;
  cout << "Введите значения n и k: " << endl;
  cin >> n >> k;
  vector<int> array(n);
  for (int& elem : array)
  {
    elem = randInt();
  }
  qsort(array, 0, n - 1);

  while (k > 0)
  {
    int value = randInt();
    cout << value << (isContained(array, n, value) ? " содержится" : " не содержится") << " в массиве." << endl;
    --k;
  }
  return 0;
}
