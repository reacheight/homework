#include <iostream>
#include <vector>
#include <climits>
#include <fstream>
#include <clocale>

using namespace std;

int main() {
    setlocale(LC_ALL, "Russian");

    ifstream in("input.txt");
    if (!in.is_open())
    {
        cout << "Не удалось открыть файл." << endl;
        return 1;
    }

    int n = 0;
    in >> n;

    vector<vector<int>> graph(n);

    for (int i = 0; i < n; ++i)
    {
        for (int j = 0; j < n; ++j)
        {
            int len = 0;
            in >> len;

            if (len == -1)
            {
                len = INT_MAX;
            }

            graph[i].push_back(len);
        }
    }

    in.close();

    int mstLen = 0;
    vector<bool> isUsed(n);
    vector<int> minLength(n, INT_MAX);
    vector<int> destination(n, -1);
    minLength[0] = 0;

    cout << "Ребра минимального оставного дерева: " << endl;

    for (int i = 0; i < n; ++i) {
        int v = -1;
        for (int j = 0; j < n; ++j)
        {
            if (!isUsed[j] && (v == -1 || minLength[j] < minLength[v])) {
                v = j;
            }
        }

        isUsed[v] = true;

        if (destination[v] != -1)
        {
            mstLen += minLength[v];
            cout << "(" << v << " " << destination[v] << ")" << " ";
        }

        for (int to = 0; to < n; ++to)
        {
            if (graph[v][to] < minLength[to])
            {
                minLength[to] = graph[v][to];
                destination[to] = v;
            }
        }
    }

    cout << endl << "Длина минимального оставного дерева: " << mstLen << endl;

    return 0;
}