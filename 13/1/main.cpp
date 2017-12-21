#include <iostream>
#include <string>

using namespace std;

enum class Status
{
    start,
    readDigit,
    readE,
    readDot,
    readFinalDigit,
    readOperator,
    readMidDigit,
    fail
};



int main()
{
    string str;
    cin >> str;

    Status state = Status::start;

    int i = 0;
    while (str[i] != '\0')
    {
        switch (state)
        {
            case Status::start:
                if (isdigit(str[i]))
                {
                    state = Status::readDigit;
                }
                else
                {
                    state = Status::fail;
                }
                ++i;
                break;

            case Status::readDigit:
                if (isdigit(str[i]))
                {
                    state = Status::readDigit;
                }
                else if (str[i] == 'E')
                {
                    state = Status::readE;
                }
                    else if (str[i] == '.')
                {
                    state = Status::readDot;
                }
                else
                {
                    state = Status::fail;
                }
                ++i;
                break;

            case Status::readE:
                if (isdigit(str[i]))
                {
                    state = Status::readFinalDigit;
                }
                else if (str[i] == '+' || str[i] == '-')
                {
                    state = Status::readOperator;
                }
                else
                {
                    state = Status::fail;
                }
                ++i;
                break;

            case Status::readDot:
                if (isdigit(str[i]))
                {
                    state = Status::readMidDigit;
                }
                else
                {
                    state = Status::fail;
                }
                ++i;
                break;

            case Status::readOperator:
                if (isdigit(str[i]))
                {
                    state = Status::readFinalDigit;
                }
                else
                {
                    state = Status::fail;
                }
                ++i;
                break;

            case Status::readFinalDigit:
                if (!isdigit(str[i]))
                {
                    state = Status::fail;
                }
                ++i;
                break;

            case Status::readMidDigit:
                if (isdigit(str[i]))
                {
                    state = Status::readMidDigit;
                }
                else if (str[i] == 'E')
                {
                    state = Status::readE;
                }
                else
                {
                    state = Status::fail;
                }
                ++i;
                break;
        }

        if (state == Status::fail)
        {
            break;
        }
    }

    if (state != Status::readDigit && state != Status::readMidDigit && state != Status::readFinalDigit)
    {
        cout << "Введённая последовательность символов не является вещественным числом." << endl;
        return 1;
    }

    cout << "Введённая последовательность символов является вещественным числом." << endl;

    return 0;
}