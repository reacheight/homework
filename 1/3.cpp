#include <bits/stdc++.h>

using namespace std;

void reverse(vector<int>& a, int start, int finish) {
  // reverse [start, finish)
  int lenght = finish - start + 1;
  for (int i = 0; i < lenght/2; ++i) {
    int t = a[start + i];
    a[start + i] = a[finish - 1 - i];
    a[finish - 1 - i] = t;
  }
}

int main() {
  int n, m;
  cin >> m >> n;
  vector<int> a(n + m);
  for (auto& e : a) {
    cin >> e;
  }
  reverse(a, 0, m);
  reverse(a, m, m + n);
  reverse(a, 0, m + n);
  for (auto e : a) {
    cout << e << " ";
  }
  
  return 0;
}
