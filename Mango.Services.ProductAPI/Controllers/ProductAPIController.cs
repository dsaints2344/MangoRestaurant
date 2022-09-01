using Mango.Services.ProductAPI.DbContexts.Models.Dto;
using Mango.Services.ProductAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Controllers;

[Route("api/products")]
public class ProductAPIController : ControllerBase
{
    private readonly ResponseDto _response;
    private readonly IProductRepository _productRepository;

    public ProductAPIController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
        this._response = new ResponseDto();
    }
    // GET
    [HttpGet]
    public async Task<object> Get()
    {
        try
        {
            IEnumerable<ProductDto> productDtos = await _productRepository.GetProducts();
            _response.Result = productDtos;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages
                = new List<string>() { e.ToString() };
        }

        return _response;
    }
    
    // GET by id
    [HttpGet]
    [Route("{id}")]
    public async Task<object> GetById(int id)
    {
        try
        { 
            ProductDto productDto = await _productRepository.GetProductById(id);
            _response.Result = productDto;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages
                = new List<string>() { e.ToString() };
        }

        return _response;
    }
}