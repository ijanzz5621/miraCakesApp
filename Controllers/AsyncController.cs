using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using miraCakesApp.Modules;
using miraCakesApp.Models;

namespace miraCakesApp.Controllers
{
    [Route("api/[controller]")]
    public class AsyncController : Controller
    {
        // GET api/async
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new ProductQuery(db);
                var result = await query.LatestProductsAsync();
                return new OkObjectResult(result);
            }
        }

        // GET api/async/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            //"server=localhost;user id=root;password=ch@rm1n9;port=3306;database=miracakesdb"
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new ProductQuery(db);
                var result = await query.FindOneAsync(id);
                if (result == null)
                return new NotFoundResult();
                return new OkObjectResult(result);
            }
        }

        // POST api/async
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Product body)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                body.Db = db;
                await body.InsertAsync();
                return new OkObjectResult(body);
            }
        }

        // PUT api/async/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody]Product body)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new ProductQuery(db);
                var result = await query.FindOneAsync(id);
                if (result == null)
                    return new NotFoundResult();
                result.ProductName = body.ProductName;
                await result.UpdateAsync();
                return new OkObjectResult(result);
            }
        }

        // DELETE api/async/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new ProductQuery(db);
                var result = await query.FindOneAsync(id);
                if (result == null)
                    return new NotFoundResult();
                await result.DeleteAsync();
                return new OkResult();
            }
        }

        // DELETE api/async
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new ProductQuery(db);
                await query.DeleteAllAsync();
                return new OkResult();
            }
        }
    }
}