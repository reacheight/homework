#include <iostream>
#include <vector>
#include <algorithm>
#include <climits>
#include <set>

using namespace std;

int main() {
    int n = 0;
    int m = 0;

    cout << "Введите кол-во строк и кол-во столбцов в двумерном массиве:" << endl;
    cin >> n >> m;

    vector<vector<int>> v(n, vector<int>(m));

    cout << "Введите элементы двумерного массива: " << endl;
    for (int i = 0; i < n; ++i)
    {
        for (int j = 0; j < m; ++j)
        {
            cin >> v[i][j];
        }
    }

    set<pair<int, int>> indexes;
    int value = 0;

    for (int i = 0; i < n; ++i)
    {
        int stringMin = *min_element(v[i].begin(), v[i].end());
        vector<int> stringMinIndexes;
        for (int j = 0; j < m; ++j)
        {
            if (v[i][j] == stringMin)
            {
                stringMinIndexes.push_back(j);
            }
        }

        for (int stringInd : stringMinIndexes)
        {
            int columnMax = INT_MIN;
            for (int j = 0; j < n; ++j)
            {
                columnMax = max(columnMax, v[j][stringInd]);
            }
            vector<int> columnMaxIndexes;
            for (int j = 0; j < n; ++j)
            {
                if (v[j][stringInd] == columnMax)
                {
                    columnMaxIndexes.push_back(j);
                }
            }

            if (stringMin == columnMax)
            {
                value = columnMax;
                for (int k : columnMaxIndexes)
                {
                    indexes.insert({stringInd, k});
                }
            }
        }
    }

    if (!indexes.empty())
    {
        cout << "Значение седловых точек: " << value << endl;
        cout << "Координаты точек: " << endl;
        for (auto p : indexes)
        {
            cout << "(" << p.first << " " << p.second << ")" << " ";
        }
    }
    else
    {
        cout << "В двумерном массиве нет сделовой точки." << endl;
    }

    return 0;
}