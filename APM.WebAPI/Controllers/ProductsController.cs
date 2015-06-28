﻿using System.Threading;
using System.Web.OData;
using APM.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace APM.WebAPI.Controllers
{
    [EnableCors("http://localhost:59919", "*", "*")]
    public class ProductsController : ApiController
    {
        private readonly IProductRepository _productRepository;

        public ProductsController()
        {
            _productRepository = new ProductRepository();
        }
        public ProductsController(IProductRepository productRepo)
        {
            _productRepository = productRepo;
        }
        // GET: api/Products
        [EnableQuery()]
        public IHttpActionResult Get()
        {
            Thread.Sleep(3000);
            return Ok(_productRepository.Retrieve().AsQueryable());
        }

        public IEnumerable<Product> Get(string search)
        {

            return _productRepository.Retrieve().Where(p => p.ProductCode.Contains(search));
        }


        // GET: api/Products/5
        public IHttpActionResult Get(int id)
        {
            Product product;
            if (id > 0)
            {
                product = _productRepository.Retrieve().FirstOrDefault(p => p.ProductId == id);
                if (product == null)
                {
                    return NotFound();
                }
            }
            else
            {
                product = _productRepository.Create();
            }
            return Ok(product);
        }

        // POST: api/Products
        public IHttpActionResult Post([FromBody]Product product)
        {
            if (product == null)
            {
                return BadRequest("Product cannot be null");
            }

            var newProduct = _productRepository.Save(product);

            if (newProduct == null)
            {
                return Conflict();
            }

            return Created<Product>(Request.RequestUri + newProduct.ProductId.ToString(), newProduct);
        }

        // PUT: api/Products/5
        public IHttpActionResult Put(int id, [FromBody]Product product)
        {
            if (product == null)
            {
                return BadRequest("Product cannot be null");
            }

            var updateProduct = _productRepository.Save(id, product);
            if (updateProduct == null)
            {
                return NotFound();
            }

            return Ok();
        }

        // DELETE: api/Products/5
        public void Delete(int id)
        {
        }
    }
}