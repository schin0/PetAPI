﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetDelivey.WebAPI.Dal.Repositories;

namespace PetDelivey.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProduto _produto;
        public ProdutoController(IProduto produto)
        {
            this._produto = produto;
        }

        [HttpGet]
        public Task<List<Model.Produto>> Get()
        {
            return _produto.GetProdutos();
        }
        [HttpGet("{id}")]
        public Task<Model.Produto> Get(string id)
        {
            return _produto.GetProdutoId(id);
        }
        [HttpPost]
        public string Post([FromBody] Model.Produto produto) => _produto.AddProduto(produto);
        [HttpPut]
        public void Put([FromBody] Model.Produto produto)
        {
            _produto.UpdateProduto(produto);
        }
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _produto.DeleteProduto(id);
        }

    }
}