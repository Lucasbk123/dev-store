using System.Threading.Tasks;
using DevStore.Customers.API.Application.Commands;
using DevStore.Customers.API.Models;
using DevStore.WebAPI.Core.Controllers;
using DevStore.WebAPI.Core.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevStore.Customers.API.Controllers
{
    [Route("customers")]
    public class CustomerController : MainController
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMediator _mediator;
        private readonly IAspNetUser _user;

        public CustomerController(ICustomerRepository customerRepository, IMediator mediator, IAspNetUser user)
        {
            _customerRepository = customerRepository;
            _mediator = mediator;
            _user = user;
        }

        [HttpGet("address")]
        public async Task<IActionResult> ObterEndereco()
        {
            var endereco = await _customerRepository.GetAddressById(_user.GetUserId());

            return endereco == null ? NotFound() : CustomResponse(endereco);
        }

        [HttpPost("address")]
        public async Task<IActionResult> AdicionarEndereco(AddAddressCommand endereco)
        {
            endereco.CustomerId = _user.GetUserId();
            return CustomResponse(await _mediator.Send(endereco));
        }
    }
}