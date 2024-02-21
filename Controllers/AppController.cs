using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AppController: ControllerBase { 

    public class PostData { 
        public int ProductId {get;set;}
        public int ActiveProductId {get;set;}
        public int Quantity {get;set;}
        public double Price {get;set;}
        public required string OperationType {get;set;}
    }
    
    [HttpGet]
    public ActionResult<List<ActiveProduct>> GetActiveProducts([FromQuery] string userId) { // na vida real o usuário seria determinado a partir da validação de um JWT
        var userProducts = ActiveProductService.GetUserActiveProducts(int.Parse(userId));
        if (userProducts is null) return NotFound("No products found");
        return Ok(userProducts);
    }

    [HttpPost]
    public ActionResult BuyProduct([FromBody] PostData body, [FromQuery] string userId) { // na vida real o usuário seria determinado a partir da validação de um JWT
        if (body.OperationType == "buy") { 
            var product = ProductService.Get(body.ProductId);
            if (product is null) return NotFound();
            if (product.Quantity >= body.Quantity && body.Price == product.Price) {
                product.Quantity -= body.Quantity;
                ProductService.Update(body.ProductId, product);
                ActiveProductService.AddProductToUser(int.Parse(userId), body.ProductId, body.Quantity, body.Price);
                return Ok("Product bought successfully");
            }
            return BadRequest("Not enough products in stock or price does not match");    

        } else if (body.OperationType == "sell") { 
            var productId = ActiveProductService.GetProductIdFromActiveProductId(body.ActiveProductId);
            var product = ProductService.Get(productId);
            var userProducts = ActiveProductService.GetActiveProductsById(body.ActiveProductId);
            if (userProducts is null || product is null) { 
                return NotFound("Product not found");
            } 
            if (body.Quantity <= userProducts.Sum(p => p.Quantity) && body.Price == product.Price)  { 
                product.Quantity += body.Quantity;
                ProductService.Update(productId, product);
                ActiveProductService.RemoveProductFromUser(body.ActiveProductId, body.Quantity);
                return Ok("Product sold successfully");
            }
            return BadRequest("Price does not match or user is trying to sell more products than he has");
            } else { 
                return BadRequest("Invalid operation type");
            }    
    }
}