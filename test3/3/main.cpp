#include <iostream>
#include <vector>
#include <fstream>
#include <queue>
#include <clocale>

using namespace std;

void printVertices(vector<vector<int>>& graph, int start)
{
    queue<int> queue;
    queue.push(start);

    vector<bool> isUsed(graph.size());
    isUsed[start] = true;

    while (!queue.empty())
    {
        int vertex = queue.front();
        cout << vertex << " ";
        queue.pop();

        for (int i = 0; i < graph[vertex].size(); ++i)
        {
            int to = graph[vertex][i];
            if (!isUsed[to])
            {
                isUsed[to] = true;
                queue.push(to);
            }
        }
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
    in >> n;

    vector<vector<int>> graph(n);

    for (int i = 0; i < n; ++i)
    {
        for (int j = 0; j < n; ++j)
        {
            int tmp = 0;
            in >> tmp;
            if (tmp == 1)
            {
                graph[i].push_back(j);
            }
        }
    }

    cout << "Вершины графа: " << endl;
    printVertices(graph, 0);

    return 0;
}