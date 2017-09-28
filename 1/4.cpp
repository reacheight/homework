#include <iostream>

using namespace std;

bool isHappy(int n)
{
  int first_numbers = 0, last_numbers = 0;

  for(int i = 0; i < 3; ++i)
  {
    last_numbers += n % 10;
    n /= 10;
  }

  while (n > 0)
  {
    first_numbers += n % 10;
    n /= 10;
  }

  return first_numbers == last_numbers;
}

int main()
{
  int cnt = 0;
  for (int number = 1000; number <= 999999; ++number)
  {
    if (isHappy(number))
    {
      ++cnt;
    }
  }

  cout << "Количество счастливых билетов : " << cnt << endl;

  return 0;
}
