#include <iostream>

using namespace std;

int main()
{
  int a = 0, b = 0;
  cout << "Введите значения a и b : " << endl;
  cin >> a >> b;
  int answer = 0;
  while (a >= b)
  {
    a -= b;
    ++answer;
  }

  cout << "Неполное частно от деления a на b : " << answer << endl;

  return 0;
}
