using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_RESTful.Models;
using api_restful.Models;

namespace api_restful.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ToDoListsController : ControllerBase
    {
        private readonly AppDBContext _ctx;

        public ToDoListsController(AppDBContext ctx)
        {
            _ctx = ctx;
        }

        // GET: ToDoLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoList>>> GetToDoLists()
        {
            return await _ctx.ToDoLists.OrderBy(d => d.deadLine).ToListAsync();
        }

        // GET: ToDo
        [HttpGet("toDo")]
        public ActionResult<IEnumerable<ToDoList>> GetToDo()
        {
            return _ctx.ToDoLists.Where(c => c.complete == false).OrderBy(d => d.deadLine).ToList();
        }
        // GET: Done
        [HttpGet("done")]
        public ActionResult<IEnumerable<ToDoList>> GetDone()
        {
            return _ctx.ToDoLists.Where(c => c.complete == true).OrderBy(d => d.deadLine).ToList();
        }

        // GET: Pra hoje
        [HttpGet("today")]
        public ActionResult<IEnumerable<ToDoList>> GetToDoToday()
        {
            return _ctx.ToDoLists.Where(d => d.deadLine.Date == DateTime.Now.Date).OrderBy(d => d.deadLine).ToList();
        }

        // GET: ToDoLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoList>> GetToDoList(int id)
        {
            var toDoList = await _ctx.ToDoLists.FindAsync(id);

            if (toDoList == null)
            {
                return NotFound("Registro não encontrado");
            }

            return toDoList;
        }

        // PUT: ToDoLists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDoList(int id, ToDoList toDoList)
        {
            var exist = _ctx.ToDoLists.Any(e => e.id == id);
            //var existe = await _ctx.ToDoLists.FindAsync(id); -> erro 2 requisições concorrentes
            if (exist)
            {
                toDoList.id = id;
                _ctx.Entry(toDoList).State = EntityState.Modified;

                try
                {
                    await _ctx.SaveChangesAsync();
                }
                catch (Exception)
                {
                    throw;
                }

                return Ok("Registro alterado com sucesso!");
            }
            return NotFound("Registro não encontrado"); //não existe id

        }

        // POST: ToDoLists
        [HttpPost]
        public async Task<ActionResult<ToDoList>> PostToDoList(ToDoList toDoList)
        {
            try
            {
                _ctx.ToDoLists.Add(toDoList);
                await _ctx.SaveChangesAsync();
            }
            catch(Exception)
            {
                throw;
            }
            return Ok("Registro cadastrado com sucesso!");
        }

        // DELETE: ToDoLists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ToDoList>> DeleteToDoList(int id)
        {
            var toDoList = await _ctx.ToDoLists.FindAsync(id);
            if (toDoList != null)
            {
                try
                {
                    _ctx.ToDoLists.Remove(toDoList);
                    await _ctx.SaveChangesAsync();

                    return Ok("Registro deletado com sucesso!");
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return NotFound("Registro não encontrado");
        }
    }
}
