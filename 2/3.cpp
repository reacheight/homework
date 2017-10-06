#include <iostream>
#include <vector>
#include <map>
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
  map<int, int> counts;
  for (int elem : v)
  {
    ++counts[elem];
  }
  int j = 0;
  for (auto pair : counts)
  {
    for (int k = 0; k < pair.second; ++k)
    {
      v[j] = pair.first;
      ++j;
    }
  }
}

int main()
{
  int t = 0;
  vector<int> arrayForBubble;
  vector<int> arrayForCounting;
  cout << "Введите значения чисел в массиве : " << endl;
  while (cin >> t)
  {
    arrayForBubble.push_back(t);
    arrayForCounting.push_back(t);
  }
  cout << "Сортировка пузырьком : " << endl;
  bubbleSort(arrayForBubble);
  printVector(arrayForBubble);
  cout << "Сортировка подсчётом : " << endl;
  countingSort(arrayForCounting);
  printVector(arrayForCounting);

  return 0;
}
