#include <iostream>
#include <clocale>

#include "stack.h"

using namespace std;

bool isDigit(char c)
{
    return '0' <= c && c <= '9';
}

bool isOperator(char c)
{
    return c == '+' || c == '-' || c == '*' || c == '/';
}

bool isOpenBracket(char c)
{
    return c == '(' || c == '[' || c == '{';
}

bool isCloseBracket(char c)
{
    return c == ')' || c ==']' || c == '}';
}

bool isGreaterPriority(char operator1, char operator2)
{
    // is priority of operator1 greater then priority of operator2 ?
    return (operator1 == '*' || operator1 == '/') && (operator2 == '+' || operator2 == '-');
}

int main()
{
    setlocale(LC_ALL, "Russian");

    Stack* stack = createStack();

    char c = '0';
    while (cin >> c)
    {
        if (isDigit(c))
        {
            cout << c << " ";
        }
        else if (isOperator(c))
        {
            while (!isEmpty(stack) && isOperator(top(stack)) && !isGreaterPriority(c, top(stack)))
            {
                cout << pop(stack) << " ";
            }

            push(stack, c);
        }
        else if (isOpenBracket((c)))
        {
            push(stack, c);
        }
        else if (isCloseBracket(c))
        {
            while (!isOpenBracket(top(stack)))
            {
                cout << pop(stack) << " ";
            }

            pop(stack);
        }
    }

    while (!isEmpty(stack))
    {
        cout << pop(stack) << " ";
    }

    deleteStack(stack);

    return 0;
}

