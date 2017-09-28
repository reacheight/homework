#include <iostream>
#include <vector>

using namespace std;

int main()
{
  int n = 0;
  cout << "Введите значение числа n : " << endl;
  cin >> n;

  vector<bool> isPrime(n + 1, 1);

  cout << "Все простые числа, не превосходящие n: " << endl;
  for (int i = 2; i <= n; i++)
  {
    if (isPrime[i])
    {
      for (int j = i * i; j <= n; j += i)
      {
        isPrime[j] = 0;
      }
      cout << i << " ";
    }
  }

  return 0;
}
