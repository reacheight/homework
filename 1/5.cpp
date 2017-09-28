#include <iostream>
#include <map>
#include <vector>

using namespace std;

int main()
{
  map <char, char> scobes;
  brackets[')'] = '(';
  brackets[']'] = '[';
  brackets['}'] = '{';

  bool isRight = 1;

  vector<int> stack;
  char c = '0';
  cout << "Введите скобочную последовательность : " << endl;
  while (cin >> c) {
    if (c == '(' || c == '[' || c == '{')
    {
      stack.push_back(c);
    }
    else
    {
      if (stack.back() == brackets[c])
      {
        stack.pop_back();
      }
      else
      {
        isRight = 0;
        break;
      }
    }
  }
  cout << ((isRight & stack.empty()) ? "Правильная" : "Не правильная") << " скобочная последовательность." << endl;

  return 0;
}
