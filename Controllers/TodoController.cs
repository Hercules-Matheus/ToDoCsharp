using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using toDo.Contexts;
using toDo.Models;
using toDo.ViewModels;

namespace toDo.Controllers;

[Authorize]
public class TodoController : Controller
{
  private readonly ToDoContext _context;
  private readonly SignInManager<ApplicationUser> _signInManager;
  private readonly UserManager<ApplicationUser> _userManager;

  private string? CurrentUserId => _userManager.GetUserId(User);

  public TodoController(ToDoContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
  {
    _context = context;
    _signInManager = signInManager;
    _userManager = userManager;
  }

  public IActionResult Index()
  {
    var userId = CurrentUserId;
    var todos = _context.Todos.Where(t => t.UserId == userId).ToList();
    return View(todos);
  }

  [AllowAnonymous]
  public IActionResult Register()
  {
    ViewData["Title"] = "Register";
    return View("Register");
  }

  [HttpPost]
  [AllowAnonymous]
  public async Task<IActionResult> Register(RegisterViewModel model)
  {
    if (ModelState.IsValid)
    {
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

        var emailExists = await _userManager.FindByEmailAsync(model.Email);
        if (emailExists != null)
        {
            ModelState.AddModelError(string.Empty, "Email already registered.");
            return View(model);
        }

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            // Normalmente redireciona para a página principal ou dashboard depois do registro
            return RedirectToAction("Index", "Todo"); 
        }

        // Adiciona os erros do Identity no ModelState para exibição na View
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
    }

    // Se ModelState inválido ou falha no CreateAsync, retorna a View com erros
    return View(model);
  }


  [AllowAnonymous]
  public IActionResult Login()
  {
    ViewData["Title"] = "Login";
    return View("Login");
  }

  [HttpPost]
  [AllowAnonymous]
  public async Task<IActionResult> Login(LoginViewModel model)
  {
    if (!ModelState.IsValid)
    {
        ViewData["Title"] = "Login";
        return View(model);
    }

    var user = await _userManager.FindByEmailAsync(model.Email);
    if (user == null)
    {
        ModelState.AddModelError(string.Empty, "Invalid user ou password.");
        ViewData["Title"] = "Login";
        return View(model);
    }

    var result = await _signInManager.PasswordSignInAsync(
        user.UserName!,
        model.Password,
        model.RememberMe,
        lockoutOnFailure: false
    );

    if (result.Succeeded)
    {
        return RedirectToAction("Index", "Todo");
    }
    else if (result.IsLockedOut)
    {
        ModelState.AddModelError(string.Empty, "Blocked account.");
    }
    else if (result.IsNotAllowed)
    {
        ModelState.AddModelError(string.Empty, "Login not authorized.");
    }
    else
    {
        ModelState.AddModelError(string.Empty, "Invalid user ou password.");
    }

    ViewData["Title"] = "Login";
    return View(model);
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
      var userId = CurrentUserId;
      if (userId == null)
      {
        ModelState.AddModelError(string.Empty, "User not authenticated.");
        return View("Form", todo);
      }
      todo.UserId = userId;
      todo.CreatedAt = DateTime.Now;
      todo.IsCompleted = false;

      _context.Todos.Add(todo);
      _context.SaveChanges();
      return RedirectToAction("Index");
    }
    return View("Form", todo);
  }

  public IActionResult Edit(int id)
  {
    var userId = CurrentUserId;

    var todo = _context.Todos.Find(id);
    if (todo == null)
    {
      return NotFound();
    }

    if (todo.UserId != userId)
    {
      return Forbid();
    }

    ViewData["Title"] = "Edit Task";
    return View("Form", todo);
  }

  [HttpPost]
  public IActionResult Edit(Todo todo)
  {
    var userId = CurrentUserId;

    if (ModelState.IsValid)
    {
      var existingTodo = _context.Todos.Find(todo.Id);
      if (existingTodo == null)
      {
        return NotFound();
      }

      if (existingTodo.UserId != userId)
      {
        return Forbid();
      }

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
    var userId = CurrentUserId;

    var todo = _context.Todos.Find(id);
    if (todo == null)
    {
      return NotFound();
    }

    if (todo.UserId != userId)
    {
      return Forbid();
    }

    ViewData["Title"] = "Delete Task";
    return View(todo);
  }

  [HttpPost]
  public IActionResult Delete(Todo todo)
  {
    var userId = CurrentUserId;

    var existingTodo = _context.Todos.Find(todo.Id);
    if (existingTodo == null)
    {
      return NotFound();
    }

    if (existingTodo.UserId != userId)
    {
      return Forbid();
    }

    _context.Todos.Remove(existingTodo);
    _context.SaveChanges();
    return RedirectToAction("Index");
  }

  public IActionResult Complete(int id)
  {
    var userId = CurrentUserId;

    var todo = _context.Todos.Find(id);
    if (todo == null)
    {
      return NotFound();
    }

    if (todo.UserId != userId)
    {
      return Forbid();
    }

    todo.Complete();
    _context.Todos.Update(todo);
    _context.SaveChanges();
    return RedirectToAction("Index");
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Logout()
  {
    await _signInManager.SignOutAsync();
    return RedirectToAction("Login", "Todo");
  }
}
