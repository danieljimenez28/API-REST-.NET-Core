using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiProductos.Context;
using WebApiProductos.Entidades;

namespace WebApiProductos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly AppDBContext _context;

        public ProductoController(AppDBContext context)
        {
            this._context = context;
        }
        // GET: api/<ClienteController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProducto()
        {   
            return await _context.Producto.ToListAsync();
        }

        // GET api/<ClienteController>/5
        [HttpGet("{id}")]
        public Producto Get(int id)
        {
            var prod = (from a in _context.Producto
                        where (a.ProductoID == id)
                        select a).FirstOrDefault();
            return prod;
        }

        // GET: api/<ClienteController>
        //[HttpGet("{name}, {status}")]
        //public async Task<ActionResult<IEnumerable<Producto>>> GetProductoByName(string name, bool Status)
        //{
        //    return await (from a in _context.Producto
        //                  where (a.Nombre.Contains(name))
        //                  select a).ToListAsync();
        //}
        // POST api/<ClienteController>
        [HttpPost]
        public ActionResult Post([FromBody] Producto product)
        {
            try
            {
                _context.Producto.Add(product);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // PUT api/<ClienteController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Producto product)
        {
            if (product.ProductoID == id)
            {
                _context.Entry(product).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/<ClienteController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var forDelete = (from a in _context.Producto
                             where (a.ProductoID == id)
                             select a).FirstOrDefault();
            if (forDelete != null)
            {
                _context.Producto.Remove(forDelete);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
