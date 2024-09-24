using AutoMapper;
using Data.Repositories.Implemention;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NEWAPP.Models;
using System.Net.Mail;

namespace NEWAPP.Controllers
{
    public class LeadSourceController : Controller
    {
        public readonly ILeadSource _leadSource;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper; // Inject AutoMapper
        public LeadSourceController(ILeadSource leadSource,IConfiguration configuration, IMapper mapper)
        {
            _leadSource = leadSource;
            _configuration = configuration;
            _mapper = mapper;
        }
        // GET: api/<LeadSourceController>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<LeadSource> leadSources = await _leadSource.GetleadSources();
            List<LeadSourceViewModel> viewModel = _mapper.Map<List<LeadSourceViewModel>>(leadSources);
            return View(viewModel);
        }


      

        // GET: LeadSourceController/Create
        public ActionResult Create()
        {
            LeadSourceViewModel leadSourceViewModel = new LeadSourceViewModel();
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
                    // Map ViewModel back to Domain Model
                    LeadSource leadSource = _mapper.Map<LeadSource>(leadSourceViewModel);
                    int id = await _leadSource.Insert(leadSource);
                    SendSecureCodeSMTP("mediga-vamshi@priyanet.com", "medigavamshi");
                    return RedirectToAction(nameof(Index));
                }
                return View(leadSourceViewModel);
            }
            catch
            {
                return View();
            }
        }

        // GET: LeadSourceController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            LeadSource leadSource = await _leadSource.GetById(id);
            LeadSourceViewModel leadSourceViewModel = _mapper.Map<LeadSourceViewModel>(leadSource);
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
                        await SendSecureCodeSMTP("mediga-vamshi@priyanet.com", "medigavamshi");

                        return RedirectToAction(nameof(Index));
                    }

                    // Handle the case where the original entity was not found (e.g., return a not found view)
                    return NotFound();
                }

                return View(leadSourceViewModel);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine(ex.Message);

                return View(leadSourceViewModel); // Return view with error if the exception is caught
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

            bool flag=  await _leadSource.DeleteMultipleAsync(leadSourceIds);
            return RedirectToAction(nameof(Index));
        }

    }
}
