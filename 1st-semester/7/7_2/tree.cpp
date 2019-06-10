#include <string>
#include <iostream>
#include <cmath>

#include "tree.h"

struct Node
{
    std::string value;
    Node* leftChild;
    Node* rightChild;
};

struct Tree
{
    Node* root{};
};

bool isInteger(std::string str)
{
    for (size_t i = 0; i < str.length(); ++i)
    {
        if ('0' > str[i] || '9' < str[i])
        {
            if (str[i] != '-' || i != 0 || str.length() <= 1)
            {
                return false;
            }
        }
    }

    return true;
}

void parseQuery(std::string& sign, std::string& leftQuery, std::string& rightQuery, const std::string& query)
{
    sign = query[1];

    bool isLeft = true;
    for (size_t i = 3; i < query.length(); ++i)
    {
        if (query[i] == '(')
        {
            int bracketCount = -1;
            while (query[i] != ')' || bracketCount != 0)
            {
                if (query[i] == '(')
                {
                    ++bracketCount;
                }
                if (query[i] == ')')
                {
                    --bracketCount;
                }
                if (isLeft)
                {
                    leftQuery = leftQuery + query[i];
                }
                else
                {
                    rightQuery = rightQuery + query[i];
                }
                ++i;
            }
            if (isLeft)
            {
                leftQuery = leftQuery + query[i];
            }
            else
            {
                rightQuery = rightQuery + query[i];
            }
            isLeft = false;
        }
        else
        {
            if (query[i] == ' ')
            {
                continue;
            }
            if (isLeft)
            {
                while (query[i] != ' ')
                {
                    leftQuery = leftQuery + query[i];
                    ++i;
                }
                isLeft = false;
            }
            else
            {
                while (query[i] != ')')
                {
                    rightQuery = rightQuery + query[i];
                    ++i;
                }
            }
        }
    }
}

void insert(Node*& node, std::string query)
{
    if (!node)
    {
        node = new Node{};
    }

    if (isInteger(query))
    {
        node->value = query;

        return;
    }

    std::string sign = "0";
    std::string leftQuery = "";
    std::string rightQuery = "";

    parseQuery(sign, leftQuery, rightQuery, query);

    node->value = sign;
    insert(node->leftChild, leftQuery);
    insert(node->rightChild, rightQuery);
}

Tree* createTree(std::string query)
{
    Tree* tree = new Tree{};
    insert(tree->root, query);

    return tree;
}

void recursivePrint(Node* node)
{
    if (!node)
    {
        return;
    }

    std::cout << node->value << " ";
    recursivePrint(node->leftChild);
    recursivePrint(node->rightChild);
}

void printTree(Tree* tree)
{
    recursivePrint(tree->root);
    std::cout << std::endl;
}

int stringToInteger(std::string str)
{
    int result = 0;
    if (str[0] == '-')
    {
        int exp = str.length() - 2;
        for (size_t i = 1; i < str.length(); ++i)
        {
            result -= (str[i] - '0') * pow(10, exp);
            --exp;
        }

        return result;
    }

    int exp = str.length() - 1;
    for (size_t i = 0; i < str.length(); ++i)
    {
        result += (str[i] - '0') * pow(10, exp);
        --exp;
    }

    return result;
}

int recursiveCalculate(Node* node)
{
    if (isInteger(node->value))
    {
        return stringToInteger(node->value);
    }

    int leftOperand= recursiveCalculate(node->leftChild);
    int rightOperand = recursiveCalculate(node->rightChild);

    if (node->value == "+")
    {
        return leftOperand + rightOperand;
    }
    else if (node->value == "-")
    {
        return leftOperand - rightOperand;
    }
    else if (node->value == "*")
    {
        return leftOperand * rightOperand;
    }
    else
    {
        return leftOperand / rightOperand;
    }
}

int calculate(Tree* tree)
{
    return recursiveCalculate(tree->root);
}

void deleteTreeElements(Node* node)
{
    if (!node)
    {
        return;
    }

    deleteTreeElements(node->leftChild);
    deleteTreeElements(node->rightChild);

    delete node;
}

void deleteTree(Tree*& tree)
{
    deleteTreeElements(tree->root);
    delete tree;
    tree = nullptr;
}
