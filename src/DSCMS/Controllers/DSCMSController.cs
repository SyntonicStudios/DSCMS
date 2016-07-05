﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DSCMS.Data;
using DSCMS.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DSCMS.Controllers
{
    public class DSCMSController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DSCMSController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ContentType(string contentTypeName = "Blog")
        {
            ContentType contentType = _context.ContentTypes.Where(ct => ct.Name == contentTypeName).FirstOrDefault();
            if (contentType == null) return NotFound();

            ViewData["Title"] = contentType.Title ?? "Title";

            Template template = _context.Templates.Where(t => t.TemplateId == contentType.TemplateId).FirstOrDefault();
            Layout layout = _context.Layouts.Where(l => l.LayoutId == template.LayoutId).FirstOrDefault();
            ViewData["Layout"] = layout.FileLocation ?? "_Layout";

            string viewLocationToUse = template.FileLocation ?? "/Views/Home/Index.cshtml";

            return View(viewLocationToUse);
        }

        new public IActionResult Content(string contentTypeName, string contentUrl) // The id here is Content.UrlToDisplay
        {
            ContentType contentType = _context.ContentTypes.Where(ct => ct.Name == contentTypeName).FirstOrDefault();
            if (contentType == null) return NotFound();

            Content content = _context.Contents.Where(c => c.UrlToDisplay == contentUrl).FirstOrDefault();
            if (content == null) return NotFound();

            ViewData["Title"] = content.Title ?? "Title";

            Template template = _context.Templates.Where(t => t.TemplateId == content.TemplateId).FirstOrDefault();
            Layout layout = _context.Layouts.Where(l => l.LayoutId == template.LayoutId).FirstOrDefault();
            ViewData["Layout"] = layout.FileLocation ?? "_Layout";

            string viewLocationToUse = template.FileLocation ?? "/Views/Home/Index.cshtml";

            return View(viewLocationToUse, content);
        }
    }
}