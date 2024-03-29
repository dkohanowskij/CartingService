﻿using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace CartingApi.Controllers.v1;

/// <summary>
/// Cart functionality
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/cart")]

public class CartController : ControllerBase
{
    private readonly ICartService _cartService;
    private readonly ILogger _logger;

    public CartController(ICartService cartService, ILogger<CartController> logger)
    {
         _cartService = cartService;
        _logger = logger;
    }

    /// <summary>
    /// Gets Cart with its items
    /// </summary>
    /// <param name="id">The cart id</param>
    /// <returns>Cart with its items</returns>
    [HttpGet]
    public IActionResult GetcartInfo(string id)
    {
        var cart = _cartService.GetCartInfo(id);

        if (cart is null)
        {
            _logger.LogError("Cart is not found with the following id: {0}", id);
            return NotFound();
        }
        
        return Ok(cart);
    }

    /// <summary>
    /// Adds item to the cart
    /// </summary>
    /// <param name="id">The cart id</param>
    /// <param name="item">The item</param>
    /// <returns>Success status code</returns>
    [HttpPost]
    public IActionResult AddItemToCart(string id, Item item)
    {
        _cartService.AddItemToCart(id, item);
        return Ok();
    }

    /// <summary>
    /// Deletes item from cart
    /// </summary>
    /// <param name="id">The cart id</param>
    /// <param name="item">The item</param>
    /// <returns>Success status code</returns>
    [HttpDelete]
    public IActionResult DeleteItemFromCart(string id, Item item)
    {
        _cartService.DeleteItemFromCart(id, item);
        _logger.LogWarning("Cart with the following id: {0}, has been deleted", id);
        return Ok();
    }
}
