#include <iostream>

using namespace std;

int main()
{
  int a = 0;
  int b = 0;
  cout << "Введите значения a и b : " << endl;
  cin >> a >> b;

  int answer = 0;
  int sign = a * b;
  a = abs(a);
  b = abs(b);
  while (a >= b)
  {
    a -= b;
    ++answer;
  }

  cout << "Неполное частное от деления a на b : " << ((sign > 0) ? answer : -++answer) << endl;

  return 0;
}
