using Data.Repositories.Implemention;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Net.Mail;

namespace NEWAPP.Controllers
{
    public class LeadSourceController : Controller
    {
        public readonly ILeadSource _leadSource;
        private readonly IConfiguration _configuration;
        public LeadSourceController(ILeadSource leadSource,IConfiguration configuration)
        {
            _leadSource = leadSource;
            _configuration = configuration; 
        }
        // GET: api/<LeadSourceController>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<LeadSource> leadSources = await _leadSource.GetleadSources();
            return View(leadSources); // Pass the data to the view
        }


      

        // GET: LeadSourceController/Create
        public ActionResult Create()
        {
            LeadSource LeadSource = new LeadSource();
            return View(LeadSource);
        }

        // POST: LeadSourceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LeadSource leadSource)
        {
            try
            {
                int id= await _leadSource.Insert(leadSource);
                SendSecureCodeSMTP("mediga-vamshi@priyanet.com", "medigavamshi");
                return RedirectToAction(nameof(Index));
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
            return View(leadSource);
        }

        // POST: LeadSourceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(LeadSource leadSource)
        {
            try
            {
                int id =await _leadSource.Update(leadSource);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
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
