﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APILibrary.Core.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBaseAPI<Customer>
    {
        private readonly EatDbContext _context;

        public CustomersController(EatDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllAsync()
        {
            var results = await _context.Customers.ToListAsync();
            return results;
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer([FromBody]Customer item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
