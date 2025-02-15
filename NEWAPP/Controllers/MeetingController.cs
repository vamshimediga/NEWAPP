using AutoMapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NEWAPP.Models;
using System.Collections.Generic;

namespace NEWAPP.Controllers
{
    public class MeetingController : Controller
    {
        private readonly IMeeting _meeting;
        private readonly IMapper _mapper;
        private readonly ICampaign _campaign;
        public MeetingController(IMapper mapper,IMeeting meeting,ICampaign campaign) {
            _mapper = mapper;
            _meeting = meeting;
            _campaign = campaign;
        }
        // GET: MeetingController
        public async Task<ActionResult> Index()
        {
            List<MeetingModel> models = await _meeting.GetAll();
            List<MeetingViewModel> result = _mapper.Map<List<MeetingViewModel>>(models);
            ViewBag.IsListView = true;
            return View(result);
        }

    

        // GET: MeetingController/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.IsCreateView = true;
            MeetingViewModel meeting = new MeetingViewModel();
            List<CampaignModel> campaigns = await _campaign.GetAll();
            List<CampaignViewModel> campaignViewModels = _mapper.Map<List<CampaignViewModel>>(campaigns);
            meeting.Campaigns = campaignViewModels;
            return View(meeting);
        }

        // POST: MeetingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MeetingViewModel meeting)
        {
            try
            {
                MeetingModel meetingModel = _mapper.Map<MeetingModel>(meeting); 
                int id = await _meeting.insert(meetingModel);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MeetingController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.IsEditView = true;
            MeetingModel meetingModel = await _meeting.GetById(id);
            MeetingViewModel meetingViewModel = _mapper.Map<MeetingViewModel>(meetingModel);
            List<CampaignModel> campaigns = await _campaign.GetAll();
            List<CampaignViewModel> campaignViewModels = _mapper.Map<List<CampaignViewModel>>(campaigns);
            meetingViewModel.Campaigns = campaignViewModels;
            return View("Create",meetingViewModel);
        }

        // POST: MeetingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, MeetingViewModel meeting)
        {
            try
            {
                MeetingModel meetingModel = _mapper.Map<MeetingModel>(meeting);
                int ids= await _meeting.update(meetingModel);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MeetingController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            ViewBag.IsDeleteView = true;
            MeetingModel meetingModel = await _meeting.GetById(id);
            MeetingViewModel meetingViewModel = _mapper.Map<MeetingViewModel>(meetingModel);
            return View("Create", meetingViewModel);
            
        }

        // POST: MeetingController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                int ids =await _meeting.deleteById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
