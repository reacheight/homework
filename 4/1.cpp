#include <iostream>
#include <string>
#include <clocale>

using namespace std;

string decimalToBinary(int number)
{
    string binNumber = "";

    int bit = 0b10000000;
    for (int i = 0; i < 8; ++i)
    {
        binNumber += ((number & bit) ? "1" : "0");
        bit = bit >> 1;
    }

    return binNumber;
}

int main()
{
    setlocale(LC_ALL, "Russian");

    int a = 0;
    int b = 0;
    cout << "Введите два числа a и b :" << endl;
    cin >> a >> b;

    cout << "a = " << a << " в двоичном представлении - " << decimalToBinary(a) << endl;
    cout << "b = " << b << " в двоичном представлении - " << (decimalToBinary(b)) << endl;
    cout << "a + b = " << a + b << " в двоичном представлении - " << decimalToBinary(a + b) << endl;
    cout << decimalToBinary(a + b) << " в десятичном представлении - " << (a + b) % 256 << endl;

    return 0;
}
