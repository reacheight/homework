#include <stdio.h>
#include <vector>
#include <clocale>

#include "qsort.h"

using namespace std;

int main()
{
    setlocale(LC_ALL, "Russian");

    FILE* in = fopen("data.in", "r");
    if (in == nullptr)
    {
        printf("Не удалось открыть файл.\n");
        return 1;
    }

    vector<int> array;
    while (!feof(in))
    {
        int tmp = 0;
        fscanf(in, "%i", &tmp);
        if (feof(in))
        {
            break;
        }
        array.push_back(tmp);
    }

    fclose(in);

    int size = array.size();
    qsort(array, 0, size - 1);

    int maxCount = 0;
    int maxCountNumber = array[0];
    int i = 0;
    while (i < size)
    {
        int number = array[i];
        int count = 0;
        while (i < size && array[i] == number)
        {
            ++count;
            ++i;
        }
        if (count > maxCount)
        {
            maxCount = count;
            maxCountNumber = number;
        }
    }

    printf("Наиболее часто встречающийся элемент в массиве - %i.\n", maxCountNumber);

    return 0;
}
