using Microsoft.AspNetCore.Mvc;
using toDo.Contexts;
using toDo.Models;

namespace toDo.Controllers;

public class TodoController : Controller
{
  private readonly ToDoContext _context;

  public TodoController(ToDoContext context)
  {
    _context = context;
  }

  public IActionResult Index()
  {
    ViewData["Title"] = "TaskFlow";
    var todos = _context.Todos.ToList();
    return View(todos);
  }

  public IActionResult Create()
  {
    ViewData["Title"] = "Add New Task";
    return View("Form");
  }
  [HttpPost]
  public IActionResult Create(Todo todo)
  {
    if (ModelState.IsValid)
    {
      todo.CreatedAt = DateTime.Now;
      todo.IsCompleted = false;
      _context.Todos.Add(todo);
      _context.SaveChanges();
      Console.WriteLine("on if ModelState.IsValid from Create Route/Action");
      return RedirectToAction("Index");
    }
    ViewData["Title"] = "Add New Task";
    return View("Form", todo);
  }

  public IActionResult Edit(int id)
  {
    var todo = _context.Todos.Find(id);
    if (todo == null)
    {
      return NotFound();
    }
    ViewData["Title"] = "Edit Task";
    return View("Form", todo);
  }

  [HttpPost]
[HttpPost]
public IActionResult Edit(Todo todo)
{
    if (ModelState.IsValid)
    {
        var existingTodo = _context.Todos.Find(todo.Id);
        if (existingTodo == null)
        {
            return NotFound();
        }

        // Atualiza apenas os campos que podem ser editados
        existingTodo.Title = todo.Title;
        existingTodo.Deadline = todo.Deadline;

        _context.Todos.Update(existingTodo);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    ViewData["Title"] = "Edit Task";
    return View("Form", todo);
}


  public IActionResult Delete(int id)
  {
    var todo = _context.Todos.Find(id);
    if (todo == null)
    {
      return NotFound();
    }
    ViewData["Title"] = "Delete Task";
    return View(todo);
  }

  [HttpPost]
  public IActionResult Delete(Todo todo)
  {
    _context.Todos.Remove(todo);
    _context.SaveChanges();
    return RedirectToAction("Index");
  }

  public IActionResult Complete(int id)
  {
    var todo = _context.Todos.Find(id);
    if (todo == null)
    {
      return NotFound();
    }
    todo.Complete();
    _context.Todos.Update(todo);
    _context.SaveChanges();
    return RedirectToAction("Index");
  }
}