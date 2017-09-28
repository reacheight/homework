#include <iostream>
#include <vector>

using namespace std;

void reverse(vector<int>& a, int start, int finish)
{
  // reverse a[start, finish)
  int lenght = finish - start + 1;
  for (int i = 0; i < lenght/2; ++i)
  {
    int tmp = a[start + i];
    a[start + i] = a[finish - 1 - i];
    a[finish - 1 - i] = tmp;
  }
}

int main() 
{
  int n = 0, m = 0;
  cout << "Введите значения m и n: " << endl;
  cin >> m >> n;
  vector<int> a(n + m);
  cout << "Введите значения чисел в массиве: " << endl;;
  for (auto& elem : a)
  {
    cin >> elem;
  }
  reverse(a, 0, m);
  reverse(a, m, m + n);
  reverse(a, 0, m + n);

  for (auto elem : a)
  {
    cout << elem << " ";
  }
  
  return 0;
}
