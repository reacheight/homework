#include <iostream>

using namespace std;

long long fibRecursive(int n)
{
  if (n == 0)
  {
    return 0;
  } 
  if (n == 1)
  {
    return 1;
  }
  return fibRecursive(n - 1) + fibRecursive(n - 2);
}

long long fibIter(int n)
{
  int prev = 0;
  int curr = 1;
  for (int i = 1; i <= n; ++i)
  {
    int temp = prev + curr;
    prev = curr;
    curr = temp;
  }
  return prev;
}

int main()
{
  int n = 0;
  cout << "Введите значение числа n : " << endl;
  cin >> n;

  cout << n << "-ое число фибоначчи : " << fibIter(n) << endl;

  return 0;
}
