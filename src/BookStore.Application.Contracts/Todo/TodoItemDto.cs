using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Todo;

public class TodoItemDto
{
    public Guid Id { get; set; }
    public string Text { get; set; }
}