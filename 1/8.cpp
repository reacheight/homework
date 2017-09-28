#include <iostream>

using namespace std;

int main()
{
  int tmp = 0, count = 0;
  cout << "Введите значения чисел в массиве : " << endl;
  while (cin >> tmp)
  {
    if (tmp == 0)
    {
      ++count;
    }
  }

  cout << "Количество нулевых элементов в массиве : " << count << endl;

  return 0;
}
