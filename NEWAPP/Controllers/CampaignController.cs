using AutoMapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NEWAPP.Models;
using System.Collections.Generic;

namespace NEWAPP.Controllers
{
    public class CampaignController : Controller
    {
        // GET: CampaignController
        private readonly IMapper _mapper;
        private readonly ICampaign _campaign;
        private readonly IMeeting _meeting;

        public CampaignController(ICampaign campaign, IMeeting meeting,IMapper mapper)
        {
            _mapper = mapper;
            _campaign = campaign;
            _meeting = meeting;
        }
        public async Task<ActionResult> Index()
        {
            List<CampaignModel> list = await _campaign.GetAll();
            List<CampaignViewModel> result = _mapper.Map<List<CampaignViewModel>>(list);
            return View(result);
        }

     
        // GET: CampaignController/Create
        public async Task<ActionResult> Create()
        {
            CampaignViewModel campaignViewModel = new CampaignViewModel();
            List<MeetingModel> meetingModels = await _meeting.GetAll();
            List<MeetingViewModel> meetingViewModels = _mapper.Map<List<MeetingViewModel>>(meetingModels);
            campaignViewModel.Meetings = meetingViewModels;
            return View(campaignViewModel);
        }

        // POST: CampaignController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CampaignViewModel campaignViewModel)
        {
            try
            {
                CampaignModel campaignModel = _mapper.Map<CampaignModel>(campaignViewModel);    
                int id = await _campaign.insert(campaignModel);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CampaignController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            CampaignModel campaignModel = await _campaign.GetById(id);
            CampaignViewModel campaignViewModel = _mapper.Map<CampaignViewModel>(campaignModel);
            List<MeetingModel> meetingModels = await _meeting.GetAll();
            List<MeetingViewModel> meetingViewModels = _mapper.Map<List<MeetingViewModel>>(meetingModels);
            campaignViewModel.Meetings = meetingViewModels;
            return View("Create",campaignViewModel);
        }

        // POST: CampaignController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CampaignViewModel campaignViewModel)
        {
            try
            {
                CampaignModel campaignModel = _mapper.Map<CampaignModel>(campaignViewModel);
                int updatedid = await _campaign.update(campaignModel);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CampaignController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            CampaignModel campaignModel = await _campaign.GetById(id);
            CampaignViewModel campaignViewModel = _mapper.Map<CampaignViewModel>(campaignModel);
            return View(campaignViewModel);
        }

        // POST: CampaignController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                bool DeletedId = await _campaign.deleteById(id);   
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
