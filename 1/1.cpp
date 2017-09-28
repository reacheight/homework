#include <iostream>

using namespace std;

int main()
{
  int x = 0;
  cout << "Введите значение х: " << endl;
  cin >> x;
  int square = x * x;
  int answer = square * (square + x + 1) + x + 1;
  cout << "Зчение выражения : " << answer << endl;

  return 0;
}
