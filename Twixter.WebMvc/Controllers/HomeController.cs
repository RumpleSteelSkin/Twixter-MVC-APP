using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Twixter.Models.Dtos.Posts;
using Twixter.Models.Dtos.Users;
using Twixter.Service.Services.Abstracts;
using Twixter.WebMvc.Models;
using Twixter.WebMvc.Models.ViewModels;

namespace Twixter.WebMvc.Controllers;

public class HomeController(IPostService postService, IMapper mapper, IUserService userService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        List<PostResponseDto> posts = await postService.GetAllAsync();
        posts.Reverse();
        var model = new PostViewModel()
        {
            Posts = posts,
            NewPost = new PostAddRequestDto()
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Index(PostViewModel model)
    {
        // RegisterRequestDto registerRequestDto = new()
        // {
        //     UserName = "RumpleSteelSkin",
        //     Email = "osmnistbayrak@gmail.com",
        //     Password = "Password1.",
        //     PhoneNumber = "555 555 55 55"
        // };
        // await userService.CreateUserAsync(registerRequestDto);
        
        var user = await userService.GetByEmailAsync("osmnistbayrak@gmail.com");
        
        PostAddRequestDto addRequestDto = new()
        {
            UserName = user.UserName,
            Content = model.NewPost.Content,
            CreatedDate = DateTime.UtcNow,
            UserId = user.Id,
            IsDeleted = false
        };
        await postService.AddAsync(addRequestDto);
        return RedirectToAction("Index");
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}