#include <iostream>
#include <vector>
#include <algorithm>

using namespace std;

void printVector(vector<int>& v)
{
  for (int elem : v)
  {
    cout << elem << " ";
  }
  cout << endl;
}

void bubbleSort(vector<int>& v)
{
  int size = v.size();
  for (int i = 0; i < size; ++i)
  {
    for (int j = 0; j < size - i - 1; ++j)
    {
      if (v[j] > v[j + 1])
      {
        swap(v[j + 1], v[j]);
      }
    }
  }
}

void countingSort(vector<int>& v)
{
  int maxElement = *max_element(v.begin(), v.end());
  vector<int> counts(maxElement + 1, 0);
  for (int elem : v)
  {
    ++counts[elem];
  }
  int j = 0;
  for (int i = 0; i <= maxElement; ++i)
  {
    for (int k = 0; k < counts[i]; ++k)
    {
      v[j] = i;
      ++j;
    }
  }
}

int main()
{
  int t = 0;
  vector<int> array;
  cout << "Введите значения чисел в массиве : " << endl;
  while (cin >> t)
  {
    array.push_back(t);
  }
  cout << "Сортировка пузырьком : " << endl;
  bubbleSort(array);
  printVector(array);
  cout << "Сортировка подсчётом : " << endl;
  countingSort(array);
  printVector(array);

  return 0;
}
