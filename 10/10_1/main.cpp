#include <iostream>
#include <vector>
#include <climits>
#include <fstream>
#include <clocale>

using namespace std;

vector<vector<pair<int, int>>> readGraph(int n, int m, ifstream& in)
{
    vector<vector<pair<int, int>>> graph(n + 1);

    for (int i = 0; i < m; ++i)
    {
        int start = 0;
        int finish = 0;
        int len = 0;

        in >> start >> finish >> len;

        graph[start].push_back({finish, len});
        graph[finish].push_back({start, len});
    }

    return graph;
};

void splitting(vector<vector<pair<int, int>>>& graph, vector<vector<int>>& world, vector<bool>& isUsed, int k, int n)
{
    int countOfUsedCities = k;
    while (countOfUsedCities < n)
    {
        for (int i = 0; i < k; ++i)
        {
            int newCity = 0;
            int minPath = INT_MAX;
            for (int j = 0; j < world[i].size(); ++j)
            {
                auto neighbors = graph[world[i][j]];
                for (int l = 0; l < neighbors.size(); ++l)
                {
                    int city = neighbors[l].first;
                    int path = neighbors[l].second;
                    if (!isUsed[city] && path < minPath)
                    {
                        newCity = city;
                        minPath = path;
                    }
                }
            }

            if (newCity != 0)
            {
                world[i].push_back(newCity);
                ++countOfUsedCities;
                isUsed[newCity] = true;
            }
        }
    }
}

void printAnswer(vector<vector<int>>& world, int k)
{
    for (int i = 0; i < k; ++i)
    {
        cout << "Государство №" << i + 1 << " : ";
        for (int j = 0; j < world[i].size(); ++j)
        {
            cout << world[i][j] << " ";
        }
        cout << endl;
    }
}

int main()
{
    setlocale(LC_ALL, "Russian");

    ifstream in("input.txt");

    if (!in.is_open())
    {
        cout << "Не удалось открыть файл." << endl;
        return 1;
    }

    int n = 0;
    int m = 0;
    in >> n >> m;

    auto graph = readGraph(n, m, in);

    int k = 0;
    in >> k;

    vector<vector<int>> world(k);
    vector<bool> isUsed(n + 1, 0);

    for (int i = 0; i < k; ++i)
    {
        int c = 0;
        in >> c;
        isUsed[c] = 1;
        world[i] = {c};
    }

    in.close();

    splitting(graph, world, isUsed, k, n);

    printAnswer(world, k);

    return 0;
}
