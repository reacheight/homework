#include <iostream>
#include <vector>
#include <map>

using namespace std;

int main()
{
  int size = 0;
  cout << "Введите размер массива : " << endl;
  cin >> size;
  cout << "Введите значения элементов массива : " << endl;
  vector<int> array(size);
  map<int, int> counts;
  for (int& elem : array)
  {
    cin >> elem;
    ++counts[elem];
  }

  int maxCount= array[0];
  for (auto pair : counts)
  {
    if (pair.second > counts[maxCount])
    {
      maxCount= pair.first;
    }
  }
  cout << "Наиболее часто встречающийся элемент в массиве : " << maxCount << endl;;

  return 0;
}
