#include <iostream>
#include <string>

using namespace std;

int main()
{
  string s = "";
  string s1 = "";
  int answer = 0;

  cout << "Введите S и S1 : " << endl;
  cin >> s >> s1;

  for (int i = 0; i < s.size(); ++i)
  {
    if (s[i] == s1[0])
    {
      bool isSubString = true;
      int start = i;
      int j = 0;
      while (start < s.size() && j < s1.size())
      {
        if (s[start] != s1[j])
        {
          isSubString = false;
        }

        ++start;
        ++j;
      }

      if (j != s1.size())
      {
        isSubString = false;
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
