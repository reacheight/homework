#include <iostream>
#include <cassert>

#include "stack.h"

struct StackElement
{
    int value;
    StackElement* next;
};

struct Stack
{
    StackElement* head;
};

Stack* createStack()
{
    return new Stack{};
}

void push(Stack *stack, int value)
{
    StackElement* newHead = new StackElement;
    newHead->value = value;
    newHead->next = stack->head;
    stack->head = newHead;
}

bool isEmpty(Stack* stack)
{
    return stack->head == nullptr;
}

int pop(Stack* stack)
{
    assert(!isEmpty(stack));

    int value = stack->head->value;
    StackElement* next = stack->head->next;
    delete stack->head;
    stack->head = next;

    return value;
}

int top(Stack *stack)
{
    assert(!isEmpty(stack));

    return stack->head->value;
}

int size(Stack *stack)
{
    int result = 0;
    StackElement* element = stack->head;
    while (element != nullptr)
    {
        ++result;
        element = element->next;
    }

    return result;
}

void deleteStack(Stack* stack)
{
    while (stack->head != nullptr)
    {
        StackElement* newHead = stack->head->next;
        delete stack->head;
        stack->head = newHead;
    }

    stack = nullptr;
}
