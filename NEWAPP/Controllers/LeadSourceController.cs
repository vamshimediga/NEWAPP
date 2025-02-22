﻿using AutoMapper;
using Data.Repositories.Implemention;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NEWAPP.Models;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net.Http;
using System.Collections.Generic;


namespace NEWAPP.Controllers
{
    public class LeadSourceController : Controller
    {
        public readonly ILeadSource _leadSource;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper; // Inject AutoMapper
        private  readonly HttpClient _httpClient; 
        public LeadSourceController(ILeadSource leadSource,IConfiguration configuration, IMapper mapper, HttpClient httpClient)
        {
            _leadSource = leadSource;
            _configuration = configuration;
            _mapper = mapper;
            _httpClient = httpClient;
        }
        // GET: api/<LeadSourceController>
        [HttpGet]
        public async Task<IActionResult> Index(string searchTerm)
        {
            List<LeadSource> leadSources;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                leadSources = await _leadSource.SearchLeadSourcesAsync(searchTerm);
                ViewData["SearchTerm"] = searchTerm;
            }
            else
            {
                leadSources = await _leadSource.GetleadSources();
            }

            List<LeadSourceViewModel> viewModel = _mapper.Map<List<LeadSourceViewModel>>(leadSources);
            return View(viewModel);
            //List<LeadSource> leadSources = await _leadSource.GetleadSources();
            //List<LeadSourceViewModel> viewModel = _mapper.Map<List<LeadSourceViewModel>>(leadSources);
            //return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Getpaging(int start = 0, int length = 10)
        {

            int startIndex = start + 1; // DataTables sends start as 0-indexed, so add 1 for SQL
            int pageSize = length;
            List<LeadSource> leads = await _leadSource.GetleadSourcespaging(startIndex, pageSize);
            List<LeadSourceViewModel> viewModel = _mapper.Map<List<LeadSourceViewModel>>(leads);
            leads = await _leadSource.GetleadSources();
            int totalRecords = leads.Count();
           return Json(new
            {
                draw = Request.Query["draw"].FirstOrDefault(), // DataTables draw count
                recordsTotal = totalRecords, // Total records before filtering
                recordsFiltered = totalRecords, // Total records after filtering (if any)
                data = viewModel // Data for the current page
            });
           
        }




        // GET: LeadSourceController/Create
        public ActionResult Create()
        {
            LeadSourceViewModel leadSourceViewModel = new LeadSourceViewModel();
            ViewBag.Message = "Please enter lead source details"; // Display message using ViewBag
         //   return View(leadSourceViewModel);
            return View(leadSourceViewModel);
        }

        // POST: LeadSourceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LeadSourceViewModel leadSourceViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string json = JsonConvert.SerializeObject(leadSourceViewModel);

                    // Deserialize JSON string back to LeadSourceViewModel (just for demo)
                    LeadSourceViewModel deserializedLeadSourceViewModel = JsonConvert.DeserializeObject<LeadSourceViewModel>(json);
                    // Map ViewModel back to Domain Model
                    LeadSource leadSource = _mapper.Map<LeadSource>(deserializedLeadSourceViewModel);
                    int id = await _leadSource.Insert(leadSource);
                    SendSecureCodeSMTP("mediga-vamshi@priyanet.com", "medigavamshi");
                    TempData["SuccessMessage"] = "Lead source created successfully"; // Set TempData for success message
                    return RedirectToAction(nameof(Index));
                }
                return View(leadSourceViewModel);
            }
            catch
            {
                TempData["ErrorMessage"] = "Error creating lead source"; // Set TempData for error message
                return View(leadSourceViewModel);
            }
        }

        // GET: LeadSourceController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            LeadSource leadSource = await _leadSource.GetById(id);
            LeadSourceViewModel leadSourceViewModel = _mapper.Map<LeadSourceViewModel>(leadSource);
            ViewData["OriginalCreatedDate"] = leadSource.CreatedDate.ToString("MM/dd/yyyy"); // Store original created date in ViewData

            return View(leadSourceViewModel); // Pass ViewModel to the view
        }

        // POST: LeadSourceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(LeadSourceViewModel leadSourceViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LeadSource originalLeadSource = await _leadSource.GetById(leadSourceViewModel.LeadSourceID);

                    // Check if the original entity was found
                    if (originalLeadSource != null)
                    {
                        // Update only the necessary properties
                        originalLeadSource.LeadSourceID = leadSourceViewModel.LeadSourceID;
                        originalLeadSource.SourceName = leadSourceViewModel.LeadSourceName;
                        originalLeadSource.ModifiedDate = leadSourceViewModel.ModifiedDate; 
                        originalLeadSource.Description = leadSourceViewModel.Description;
                        // Optionally update any other properties here
                        // e.g., originalLeadSource.SomeOtherProperty = leadSourceViewModel.SomeOtherProperty;

                        // Keep CreatedDate as is, since it's already stored in the originalLeadSource

                        // Update the entity in the database
                        await _leadSource.Update(originalLeadSource);

                        // Optionally send an email after updating

                        TempData["SuccessMessage"] = "Lead source updated successfully";
                        return RedirectToAction(nameof(Index));
                    }

                    // Handle the case where the original entity was not found (e.g., return a not found view)
                    return NotFound();
                }

                return View(leadSourceViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                TempData["ErrorMessage"] = "Error updating lead source"; // Set TempData for error message
                return View(leadSourceViewModel);
            }
        }



        [HttpGet]
        public async Task<IActionResult> SendSecureCodeSMTP(string Email,string FullName)
        {
            string OTP = GeneratePassword(); // Generate the OTP
            string email = Email;
            string fullName = FullName;

            if (!string.IsNullOrEmpty(email))
            {
                try
                {
                    // Fetch SMTP settings from appsettings.json
                    string host = _configuration["MailSettings:Host"];
                    int port = int.Parse(_configuration["MailSettings:Port"]);
                    string username = _configuration["MailSettings:Username"];
                    string password = _configuration["MailSettings:Password"];
                    string fromEmail = _configuration["fromAddress"]; // Correct path if it's under "MailSettings"


                    // Load the HTML template for the email
                    var mailBody = OTP;

                    using (var msg = new MailMessage())
                    {
                        msg.From = new MailAddress(fromEmail);
                        msg.To.Add(new MailAddress(email));
                        msg.Subject = "This is your One Time Password";
                        msg.Body = mailBody;
                        msg.IsBodyHtml = true;

                        // Set up the SmtpClient with the SMTP server details
                        using (var smtpClient = new SmtpClient(host, port))
                        {
                            smtpClient.EnableSsl = true; // Enable SSL if required by the server
                            smtpClient.Credentials = new System.Net.NetworkCredential(username, password);

                            // Send email asynchronously
                            await smtpClient.SendMailAsync(msg);
                        }
                    }

                    return Ok(OTP); // Return the OTP if successful
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message); // Return error message in case of failure
                }
            }

            return BadRequest("Email is required.");
        }

        private string GeneratePassword()
        {
            // Implement your OTP generation logic here
            return "123456"; // Example OTP
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMultiple(string leadSourceIds)
        {
            try
            {
                bool flag = await _leadSource.DeleteMultipleAsync(leadSourceIds);
                TempData["SuccessMessage"] = "Selected lead sources deleted successfully"; // Set TempData for success message
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "Error deleting lead sources"; // Set TempData for error message
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Search(string searchTerm)
        {
            // Fetch the lead sources based on the search term
            var leadSources = await _leadSource.SearchLeadSourcesAsync(searchTerm);

            // Map LeadSource entities to LeadSourceViewModel
            var viewModel = _mapper.Map<List<LeadSourceViewModel>>(leadSources);

            // Return the view with the filtered or unfiltered results
            return View(viewModel);
        }
        // GET: LeadSourceController/Edit/5
        [HttpGet]
        [Route("CountriesList")]
        public async Task<ActionResult> CountriesList()
        {
            string URL = _configuration["Apis:CountriesApiUrl"];
            HttpResponseMessage responseMessage = await _httpClient.GetAsync($"{URL}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonResponse = await responseMessage.Content.ReadAsStringAsync();
                // Deserialize the JSON response into an array of Country objects
                //var countries = JsonConvert.DeserializeObject<Country[]>(jsonResponse);
                //List<CountriesViewModel> mappedCountries = new List<CountriesViewModel>();  
                //foreach (var item in countries)
                //{
                //  mappedCountries.Add( new CountriesViewModel() { Name:item.name});

                //}
                //// Map the response to our DTO
                //List<CountriesViewModel> mappedCountries = countries.f(country => new()
                //{
                //    Name = country.Name?.Common,
                //    Capital = country.Capital != null && country.Capital.Count > 0 ? country.Capital[0] : "N/A",
                //    Flag = country.Flag
                //}).ToList();

                return View(jsonResponse);
            }
            else
            {
                // Handle error
                return View("Error");
            }
        }

    }
}
