#include <iostream>
#include <string>

using namespace std;

int main()
{
  string s = "", s1 = "";
  int answer = 0;

  cout << "Введите S и S1 : " << endl;
  cin >> s >> s1;

  for (int i = 0; i < s.size(); ++i)
  {
    if (s[i] == s1[0])
    {
      bool isSubString = true;
      int start = i;
      for (int j = 0; j < s1.size(); ++j)
      {
        if (s[start] != s1[j])
        {
          isSubString = false;
        }
        ++start;
      }
      if (isSubString)
      {
        ++answer;
      }
    }
  }
  cout << "Строка S1 входит в S " << answer << " раз." << endl;

  return 0;
}
