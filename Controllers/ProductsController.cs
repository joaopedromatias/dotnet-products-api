using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductsController: ControllerBase { 
    
    [HttpGet]
    public ActionResult<List<Product>>? ListProducts([FromQuery] string UserType) { // na vida real seria usado o header de autorização com JWT para validar o acesso do usuário
        if (UserType == "admin") {
            var products = ProductService.List();
            return products;
        }
        return Forbid();
    }

    [HttpGet("{id}")]
    public ActionResult<Product?>? GetProduct(int id, [FromQuery] string UserType) { // na vida real seria usado o header de autorização com JWT para validar o acesso do usuário
        if (UserType == "admin") {
            Product? product = ProductService.Get(id);
            if (product is null) return NotFound();
            return product;
        }
        return Forbid();
    }

    [HttpPost]
    public ActionResult? CreateProduct(Product product, [FromQuery] string UserType) { // na vida real seria usado o header de autorização com JWT para validar o acesso do usuário
        if (UserType == "admin") {
            ProductService.Create(product);
            return CreatedAtAction(nameof(GetProduct), new {id = product.Id}, product);
        }
        return Forbid();
    }

    [HttpPut("{id}")]
    public ActionResult<Product?>? UpdateProduct(int id, Product product, [FromQuery] string UserType) { // na vida real seria usado o header de autorização com JWT para validar o acesso do usuário
        if (UserType == "admin") {
            var updated = ProductService.Update(id, product);
            if (!updated) return NotFound();
            return NoContent();
        }
        return Forbid();
    }

    [HttpDelete("{id}")]
    public ActionResult<Product?>? DeleteProduct(int id, [FromQuery] string UserType) { // na vida real seria usado o header de autorização com JWT para validar o acesso do usuário
        if (UserType == "admin") {
            var deleted = ProductService.Delete(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
        return Forbid();
    }
}