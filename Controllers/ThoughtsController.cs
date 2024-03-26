using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MemotecaApi.Data;
using MemotecaApi.ViewModels;
using MemotecaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//CRUD
namespace MemotecaApi.Controllers
{
    [ApiController]
    [Route("v1")]
    public class ThoughtsController : ControllerBase
    {
        [HttpGet]
        [Route("Thoughts")]
        public async Task<IActionResult> GetAsync(
            [FromServices] AppDataContext context
        )
        {
            var thoughts = await context
            .Thoughts
            .AsNoTracking()
            .ToListAsync();

            return Ok(thoughts);
        }

        [HttpGet]
        [Route("Thoughts/{id}")]
        public async Task<IActionResult> GetByAsync(
            [FromServices] AppDataContext context, [FromRoute] int id
        )
        {
            var thought = await context
            .Thoughts
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

            return thought == null ? NotFound() : Ok(thought);
        }

        [HttpPost]
        [Route("Thoughts")]
        public async Task<IActionResult> PostAsync(
            [FromServices] AppDataContext context, 
            [FromBody] CreateThoughtsViewModel model
        )
        {
            if (!ModelState.IsValid) {
                return BadRequest();
            }

            var thought = new Thought
            {
                Content = model.Content,
                Authorship = model.Authorship,
                Model = model.Model
            };

            try {
                await context.Thoughts.AddAsync(thought);
                await context.SaveChangesAsync();
                return Created($"v1/Thoughts/{thought.Id}", thought);
            } catch (Exception e) {
                return BadRequest();
            }
        }
        
        [HttpPut]
        [Route("Thoughts/{id}")]
         public async Task<IActionResult> PutAsync(
            [FromServices] AppDataContext context,
            [FromBody] CreateThoughtsViewModel model,
            [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var thought = await context.Thoughts.FirstOrDefaultAsync(x => x.Id == id);

            if (thought == null)
                return NotFound();

            try
            {
                thought.Content = model.Content;
                thought.Authorship = model.Authorship;
                thought.Model = model.Model;
                
                context.Thoughts.Update(thought);
                await context.SaveChangesAsync();
                return Ok(thought);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("Thoughts/{id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] AppDataContext context,
            [FromRoute] int id)
        {
            var thought = await context.Thoughts.FirstOrDefaultAsync(x => x.Id == id);

            try
            {
                context.Thoughts.Remove(thought);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}