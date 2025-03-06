using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Twixter.Models.Dtos.Posts;
using Twixter.Service.Services.Abstracts;
using Twixter.WebMvc.Hubs;
using Twixter.WebMvc.Models;
using Twixter.WebMvc.Models.ViewModels;

namespace Twixter.WebMvc.Controllers;

public class HomeController(
    IPostService postService,
    IMapper mapper,
    IUserService userService,
    IHubContext<PostHub> hubContext) : Controller
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
        var user = await userService.GetByEmailAsync("osmnistbayrak@gmail.com");

        PostAddRequestDto addRequestDto = new()
        {
            UserName = user.UserName,
            Content = model.NewPost.Content,
            CreatedDate = DateTime.Now,
            UserId = user.Id,
            IsDeleted = false
        };
        await postService.AddAsync(addRequestDto);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> IndexAjaxTest()
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
    public async Task<IActionResult> IndexAjaxTest(PostViewModel model)
    {
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

        var response = new
        {
            userName = user.UserName,
            userProfilePictureUrl = user.ProfilePictureUrl ?? "/images/nonprofilepictures/blank_profile.webp",
            content = addRequestDto.Content,
            createdDate = addRequestDto.CreatedDate.ToString("MMM dd, yyyy HH:mm")
        };

        return Json(response);
    }

    
    [HttpPost]
    public async Task<IActionResult> CreatePost(PostViewModel model)
    {
        var user = await userService.GetByEmailAsync("osmnistbayrak@gmail.com");

        PostAddRequestDto addRequestDto = new()
        {
            UserName = user.UserName,
            Content = model.NewPost.Content,
            CreatedDate = DateTime.UtcNow,
            UserId = user.Id,
            IsDeleted = false
        };
            // Yeni gönderi veritabanına kaydedilir (Repository ve Service katmanları aracılığıyla)
            await postService.AddAsync(addRequestDto);

            // Gönderiyi SignalR ile tüm istemcilere bildir
            await hubContext.Clients.All.SendAsync("ReceiveNewPost", addRequestDto.UserName, addRequestDto.Content);

            return RedirectToAction("Index");
        
        
        return View(model);
    }
    //
    // [HttpPost]
    // public async Task<IActionResult> CreatePost(string content)
    // {
    //     if (string.IsNullOrEmpty(content))
    //     {
    //         return BadRequest("Post content cannot be empty.");
    //     }
    //
    //     // Yeni gönderiyi veritabanına kaydet
    //     var post = await _postService.CreatePostAsync(content);
    //
    //     // SignalR ile tüm istemcilere yeni gönderiyi gönder
    //     var postDto = new
    //     {
    //         post.Id,
    //         post.UserName,
    //         post.Content,
    //         UserProfilePictureUrl = post.User.ProfilePictureUrl ?? "/images/nonprofilepictures/blank_profile.webp",
    //         CreatedDate = post.CreatedDate.ToString("MMM dd, yyyy HH:mm")
    //     };
    //
    //     await _hubContext.Clients.All.SendAsync("ReceiveNewPost", postDto);
    //
    //     return Ok(postDto);
    // }
    
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}