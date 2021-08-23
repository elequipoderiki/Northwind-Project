using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Northwind.BussinessLogic.Interfaces;
using Northwind.Models;
using Northwind.WebApi.Models;

namespace Northwind.WebApi.Controllers
{
  [Produces("application/json")]
  [Route("api/Supplier")]

  [Authorize]
  public class SupplierController : Controller
  {
    
    private readonly ISupplierLogic _logic;
    
    public SupplierController(ISupplierLogic logic)
    {
      _logic = logic;
    }

    [HttpGet]
    [Route("{id:int}")]
    public IActionResult GetById(int id)
    {
      //notar que usamos el singleton NorthwindUnitOfWork (creado en startup) accediendo a su propiedad Supplier la cual implementa ISupplierRepository de donde obtenemos getById, pues la hereda de su clase padre repository
      //no es necesario instanciar a SupplierRepository pues se determinaria el uso obligatorio de un repositorio respectivo en cada controlador sino que todos los controladores solo conocen a unitOfWork
      return Ok(_logic.GetById(id));
    }

    [HttpPost]
    [Route("GetPaginatedSupplier")]
    public IActionResult GetPaginatedSupplier([FromBody]GetPaginatedSupplier request)
    {
      //notice that all the stuff one of bussiness logic is abstracted
      //in _logic field, delegating the requested services to it
      return Ok(_logic.SupplierPagedList(request.Page, request.Rows, request.SearchTerm));
    }

    [HttpPost]
    public IActionResult Post([FromBody] Supplier supplier)
    {
      if(!ModelState.IsValid) return BadRequest();
      return Ok(_logic.Insert(supplier));
    }

    [HttpPut]
    public IActionResult Put([FromBody] Supplier supplier)
    {
      if(ModelState.IsValid && _logic.Update(supplier))
      {
        return Ok(new {Message = "The supplier is Updated"});
      }
      return BadRequest();
    }

    [HttpDelete]
    public IActionResult Delete([FromBody]Supplier supplier)
    {
      if(supplier.Id > 0)
        return Ok(_logic.Delete(supplier));
      return BadRequest();
    }
  }
}


