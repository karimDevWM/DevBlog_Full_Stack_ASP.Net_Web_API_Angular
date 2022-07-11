﻿using DevBlog.API.Data;
using DevBlog.API.Models.DTO;
using DevBlog.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevBlog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        private readonly DevBlogDbContext dbContext;

        public PostsController(DevBlogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await dbContext.Posts.ToListAsync();

            return Ok(posts);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetPostById")]
        public async Task<IActionResult> GetPostById(Guid id)
        {
            var post = await dbContext.Posts.FirstOrDefaultAsync(x => x.Id == id);

            if(post != null)
            {
                return Ok(post);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(AddPostRequest addPostRequest)
        {
            // convert DTO to Entity
            var post = new Post()
            {
                Title = addPostRequest.Title,
                Content = addPostRequest.Content,
                Author = addPostRequest.Author,
                FeatureImageUrl = addPostRequest.FeatureImageUrl,
                PublishedDate = addPostRequest.PublishedDate,
                UpdatedDate = addPostRequest.UpdatedDate,
                Summary = addPostRequest.Summary,
                UrlHandle = addPostRequest.UrlHandle,
                Visible = addPostRequest.Visible,
            };
            // that assign automatically an id to the new created post
            post.Id = Guid.NewGuid();
            await dbContext.Posts.AddAsync(post);
            // save the post into database
            await dbContext.SaveChangesAsync();

            // return the location of the new created post
            return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdatePost([FromRoute]Guid id, UpdatePostRequest updatePostRequest)
        {
            // check if the post exist
            var existingPost = await dbContext.Posts.FindAsync(id);

            // check if the post is not null
            if(existingPost != null)
            {
                //update existing post
                existingPost.Title = updatePostRequest.Title;
                existingPost.Content = updatePostRequest.Content;
                existingPost.Author = updatePostRequest.Author;
                existingPost.FeatureImageUrl = updatePostRequest.FeatureImageUrl;
                existingPost.PublishedDate = updatePostRequest.PublishedDate;
                existingPost.UpdatedDate = updatePostRequest.UpdatedDate;
                existingPost.Summary = updatePostRequest.Summary;
                existingPost.UrlHandle = updatePostRequest.UrlHandle;
                existingPost.Visible = updatePostRequest.Visible;

                //update the table
                await dbContext.SaveChangesAsync();

                return Ok(existingPost);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            //find the post
            var existingPost = await dbContext.Posts.FindAsync(id);

            //check if the post is not null
            if (existingPost != null)
            {
                //DeleteBehavior the post in the database
                dbContext.Remove(existingPost);
                //Save the changes in the database
                await dbContext.SaveChangesAsync();

                return Ok(existingPost);
            }
            return NotFound();
        }
    }
}
