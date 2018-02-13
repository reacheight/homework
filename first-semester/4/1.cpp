#include <iostream>
#include <clocale>
#include <vector>
#include <cmath>

using namespace std;

vector<bool> decimalToBinary(int number)
{
    vector<bool> binNumber;

    unsigned bit = 0b10000000000000000000000000000000;
    for (int i = 0; i < 32; ++i)
    {
        binNumber.push_back(number & bit);
        bit = bit >> 1;
    }

    return binNumber;
}

vector<bool> binAddition(const vector<bool>& firstNumber, const vector<bool>& secondNumber)
{
    vector<bool> result(32, false);

    bool carry = false;
    for (int i = 31; i >= 0; --i)
    {
        bool bit = firstNumber[i] ^ secondNumber[i];

        if (carry)
        {
            bit = bit ^ carry;
            carry = firstNumber[i] || secondNumber[i];
        }
        else
        {
            carry = firstNumber[i] & secondNumber[i];
        }

        result[i] = bit;
    }

    return result;
}

void reverseBinNumber(vector<bool>& number)
{
    for (auto bit : number)
    {
        bit = !bit;
    }
}

void printBinNumber(const vector<bool>& number)
{
    for (bool bit : number)
    {
        cout << bit;
    }

    cout << endl;
}

int binaryToDecimal(const vector<bool>& binNumber)
{
    int number = 0;

    bool sign = binNumber[0];
    vector<bool> tmp = binNumber;

    if (sign)
    {
        tmp = binAddition(tmp, decimalToBinary(-1));
        reverseBinNumber(tmp);
    }

    for (int i = 31; i > 0; --i)
    {
        number += tmp[i] * pow(2, 31 - i);
    }

    return (sign) ? -number : number;
}

int main()
{
    setlocale(LC_ALL, "Russian");

    int a = 0;
    int b = 0;
    cout << "Введите два числа a и b :" << endl;
    cin >> a >> b;

    vector<bool> aBin = decimalToBinary(a);
    vector<bool> bBin = decimalToBinary(b);

    cout << a << " в двоичном представлении: ";
    printBinNumber(aBin);
    cout << b << " в двоичном представлении: ";
    printBinNumber(bBin);

    vector<bool> sum = binAddition(aBin, bBin);
    cout << "Их сумма в двоичном представлении: ";
    printBinNumber(sum);

    cout << "Их сумма в десятичном представлении: ";
    cout << binaryToDecimal(sum) << endl;

    return 0;
}
