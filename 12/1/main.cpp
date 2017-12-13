#include <iostream>
#include <vector>
#include <string>
#include <fstream>
#include <clocale>

using namespace std;

const string SEP = "#";

vector<int> prefix_function(string s) {
    int n = s.length();
    vector<int> pi(n, 0);

    for (int i = 1; i < n; ++i)
    {
        int j = pi[i - 1];
        while (j > 0 && s[i] != s[j])
        {
            j = pi[j - 1];
        }

        if (s[i] == s[j])
        {
            ++j;
        }

        pi[i] = j;
    }

    return pi;
}

int main()
{
    setlocale(LC_ALL, "Russian");

    fstream fin("input.txt");
    if (!fin.is_open())
    {
        cout << "Не удалось открыть файл";
        return 1;
    }

    string text = "";
    fin >> text;
    fin.close();

    cout << "Введите строку: " << endl;
    string str = "";
    cin >> str;

    string tmp = str + SEP + text;
    auto pi = prefix_function(tmp);

    for (int i = str.length() + 1; i < tmp.length(); ++i)
    {
        if (pi[i] == str.length())
        {
            cout << "Позиция первого вхождения введённой строки в тексте: " << i - 2 * str.length() << endl;
            return 0;
        }
    }

    cout << "Введённой строки нет в тексте." << endl;

    return 0;
}