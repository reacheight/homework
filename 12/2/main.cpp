#include <iostream>
#include <vector>
#include <climits>
#include <fstream>
#include <clocale>

using namespace std;

vector<vector<int>> readGraph(int n, ifstream& in)
{
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

    return graph;
}

pair<int, vector<vector<int>>> getMinSpanningTree(vector<vector<int>>& graph, int n)
{
    int treeLength = 0;
    vector<vector<int>> treeEdges;
    vector<bool> isUsed(n);
    vector<int> minLength(n, INT_MAX);
    vector<int> destination(n, -1);
    minLength[0] = 0;

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
            treeLength += minLength[v];
            treeEdges.push_back({v, destination[v]});
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

    return {treeLength, treeEdges};
};

void printAnswer(pair<int, vector<vector<int>>>& lengthAndEdges)
{
    cout << "Рёбра минимального остовного дерева: " << endl;
    for (auto edge : lengthAndEdges.second)
    {
        cout << "(" << edge[0] << " " << edge[1] << ")" << " ";
    }

    cout << endl << "Длина минимального остовного дерева: " << lengthAndEdges.first << endl;
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

    auto graph = readGraph(n, in);

    in.close();

    auto lengthAndEdges = getMinSpanningTree(graph, n);
    printAnswer(lengthAndEdges);

    return 0;
}
