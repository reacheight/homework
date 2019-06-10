#include <iostream>
#include <vector>
#include <algorithm>

using namespace std;

void partition(vector<int>& v, int size)
{
  int pivot = v[0];
  int i = 0, j = size - 1;
  while (i <= j)
  {
    while (v[i] < pivot)
    {
      ++i;
    }
    while (v[j] > pivot)
    {
      --j;
    }
    if (i <= j)
    {
      swap(v[i], v[j]);
      ++i;
      --j;
    }
  }
}

int main()
{
  int size = 0;
  cout << "Введите размер массива : " << endl;
  cin >> size;
  vector<int> array(size);
  for (int& elem : array)
  {
    elem = rand();
  }
  cout << "Первый элемент массива - " << array[0] << endl;
  partition(array, size);
  for (int elem : array)
  {
    cout << elem << " ";
  }

  return 0;
}
