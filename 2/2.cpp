#include <iostream>

using namespace std;

long long powerLog(int a, int n)
{
  if (n == 0)
  {
    return 1;
  }

  if (n == 1)
  {
    return a;
  }

  if (n % 2 == 0)
  {
    return powerLog(a, n / 2) * powerLog(a, n / 2);
  }

  return powerLog(a, n - 1) * a;
}

long long power(int a, int n)
{
  if (n == 0)
  {
    return 1;
  }

  if (n == 1)
  {
    return a;
  }

  return power(a, n - 1) * a;
}

int main()
{
  int a = 0;
  int n = 0;
  cout << "Введите число и показатель степени : " << endl;
  cin >> a >> n;

  cout << a << " в степени " << n << " = " << powerLog(a, n);

  return 0;
}
