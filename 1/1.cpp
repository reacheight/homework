#include <bits/stdc++.h>

using namespace std;

int main() {
  int x;
  cin >> x;
  int square_x = x * x;
  int ans = square_x * (square_x + x + 1) + x + 1;
  cout << ans;
  
  return 0;
}
