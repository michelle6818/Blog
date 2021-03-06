﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IEmailSender _emailSender;
        private readonly MailSettings _mailSettings;

        public HomeController(
            ILogger<HomeController> logger,
            ApplicationDbContext dbContext,
            IEmailSender emailSender,
            IOptions<MailSettings> mailSettings)
        {
            _logger = logger;
            _dbContext = dbContext;
            _emailSender = emailSender;
            _mailSettings = mailSettings.Value;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _dbContext.BlogCategory.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        //Add contact sheet to Home Controller and Views with the same commands as Index and Privacy
        //Http GET
        public IActionResult Contact()
        {
            return View();
        }
        //Http POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(Contact contact)
        {
            await _emailSender.SendEmailAsync(_mailSettings.Mail, $"From {contact.Email}", contact.Message);
            return RedirectToAction("Index");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
