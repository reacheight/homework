#include <iostream>
#include <clocale>

#include "stack.h"

using namespace std;

int main()
{
    setlocale(LC_ALL, "Russian");

    Stack* stack = createStack();

    cout << "Введите арифметическое выражение в постфиксной форме" << endl;

    char c = '0';
    while (cin >> c)
    {
        if ('0' <= c && c <= '9')
        {
            int digit = c - '0';
            push(stack, digit);
        }
        else
        {
            int first = pop(stack);
            int second = pop(stack);

            int result = 0;
            switch (c)
            {
                case '+' : result = first + second;
                           break;

                case '-' : result = second - first;
                           break;

                case '*' : result = first * second;
                           break;

                case '/' : if (first == 0)
                           {
                               cout << "Деление на ноль." << endl;
                               return 0;
                           }
                           result = second / first;
                           break;

                default : cout << "Данная операция не поддерживается" << endl;
            }

            push(stack, result);
        }
    }

    cout << "Результат вычисления равен " << pop(stack) << endl;

    deleteStack(stack);

    return 0;
}

