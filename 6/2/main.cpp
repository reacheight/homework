#include <iostream>
#include <clocale>
#include <map>

#include "stack.h"

using namespace std;

int main()
{
    setlocale(LC_ALL, "Russian");

    Stack* stack = createStack();

    map<char, char> brackets;
    brackets[')'] = '(';
    brackets[']'] = '[';
    brackets['}'] = '{';

    cout << "Введите скобочную последовательность" << endl;

    char c = '0';
    while (cin >> c)
    {
        if (c == '(' || c == '[' || c == '{')
        {
            push(stack, c);
        }
        else
        {
            if (brackets[c] == top(stack))
            {
                pop(stack);
            }
            else
            {
                cout << "Некорректная скобочная последовательность." << endl;
                return 0;
            }
        }
    }

    cout << (isEmpty(stack) ? "Корректная" : "Некорректная") << " скобочная последовательность" << endl;

    return 0;
}

