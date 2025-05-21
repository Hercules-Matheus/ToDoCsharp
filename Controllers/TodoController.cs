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
    ViewData["Title"] = "To-do List";
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
      return RedirectToAction(nameof(Index));
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
  public IActionResult Edit(Todo todo)
  {
    if (ModelState.IsValid)
    { 
      _context.Todos.Update(todo);
      _context.SaveChanges();
      return RedirectToAction(nameof(Index));
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
    return RedirectToAction(nameof(Index));
  }

  public IActionResult Complete(int id)
  {
    var todo = _context.Todos.Find(id);
    if (todo == null)
    {
      return NotFound();
    }
    todo.IsCompleted = !todo.IsCompleted;
    todo.CompletedAt = todo.IsCompleted ? DateOnly.FromDateTime(DateTime.Now) : null;
    _context.Todos.Update(todo);
    _context.SaveChanges();
    return RedirectToAction(nameof(Index));
  }
}