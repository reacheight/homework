#include <string>
#include <iostream>

#include "tree.h"

struct Node
{
    std::
    string value;
    Node* leftChild;
    Node* rightChild;
};

struct Tree
{
    Node* root{};
};

bool isInteger(std::string str)
{
    for (char c : str)
    {
        if ('0' > c || '9' < c)
        {
            return false;
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
            while (query[i] != ')')
            {
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

int recursiveCalculate(Node* node)
{
    if (isInteger(node->value))
    {
        int result = 0;
        for (char c : node->value)
        {
            result += c - '0';
        }

        return result;
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
    recursiveCalculate(tree->root);
}
