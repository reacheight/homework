#include <iostream>
#include <fstream>
#include <map>
#include <string>
#include <clocale>

using namespace std;

void addNotation(map<string, string>& phonebook)
{
    cout << "Введите имя :" << endl;
    string name = "name";
    cin >> name;
    cout << "Введите номер :" << endl;
    string number = "number";
    cin >> number;

    phonebook[name] = number;
}

void printAll(const map<string, string>& phonebook)
{
    for (auto note : phonebook)
    {
        cout << note.first << " " << note.second << endl;
    }
}

void findNumber(const map<string, string>& phonebook)
{
    cout << "Введите имя :" << endl;
    string name = "name";
    cin >> name;

    string number = "";

    for (auto note : phonebook)
    {
        if (note.first == name)
        {
            number = note.second;
        }
    }

    cout << ((number == "") ? "Не обнаружено записи с таким именем." : number) << endl;
}

void findName(const map<string, string>& phonebook)
{
    cout << "Введите номер :" << endl;
    string number = "number";
    cin >> number;

    string name = "";

    for (auto note : phonebook)
    {
        if (note.second == number)
        {
            name = note.first;
        }
    }

    cout << ((name == "") ? "Не обнаружено записи с таким номером." : name) << endl;
}

void saveFile(const map<string, string>& phonebook)
{
    ofstream out("database", ios_base::out);

    for (auto note : phonebook)
    {
        out << note.first << " " << note.second << endl;
    }

    out.close();
}

void startOperation(int operationNumber, map<string, string>& phonebook)
{
    switch(operationNumber)
    {
        case 0 : exit(0);
                 break;
        case 1 : addNotation(phonebook);
                 break;
        case 2 : printAll(phonebook);
                 break;
        case 3 : findNumber(phonebook);
                 break;
        case 4 : findName(phonebook);
                 break;
        case 5 : saveFile(phonebook);
                 break;

        default : cout << "Нет операции под номером " << operationNumber << endl;
                 break;
    }
}

void startMenu(map<string, string>& phonebook)
{
    cout << "0 - выйти"                            << endl <<
            "1 - добавить запись"                  << endl <<
            "2 - распечатать все записи"           << endl <<
            "3 - найти телефон по имени"           << endl <<
            "4 - найти имя по телефону"            << endl <<
            "5 - сохранить текущие данные в файл"  << endl;

    int operationNumber = -1;
    cin >> operationNumber;

    startOperation(operationNumber, phonebook);
    startMenu(phonebook);
}

int main()
{
    setlocale(LC_ALL, "Russian");

    map<string, string> phonebook;

    ifstream in("database");
    if (in.is_open())
    {
        string name = "name";
        string number = "number";
        while (in >> name && in >> number)
        {

            phonebook[name] = number;
        }

        in.close();
    }

    startMenu(phonebook);

    return 0;
}
