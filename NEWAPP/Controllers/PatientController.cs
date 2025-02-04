using AutoMapper;
using Data.Repositories.Interfaces;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NEWAPP.Models;
using System.Collections.Generic;

namespace NEWAPP.Controllers
{
    public class PatientController : Controller
    {
        public readonly IPatientRepository _patient;
        public readonly IMapper _mapper;
        private readonly IDoctorRepository _doctorRepository;
        public PatientController(IPatientRepository patient, IMapper mapper, IDoctorRepository doctorRepository)
        {

            _patient = patient;
            _mapper = mapper;
            _doctorRepository = doctorRepository;
        }
        // GET: PatientController
        public async Task<ActionResult> Index()
        {
            List<Patient> patients = await _patient.GetAllAsync();
            List<PatientViewModel> list = _mapper.Map<List<PatientViewModel>>(patients);
        //    List<Doctor> doctors = await _doctorRepository.GetAllAsync();
            return View(list);
        }

        // GET: PatientController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Patient patient = await _patient.GetByIdAsync(id);
            PatientViewModel patientViewModel = _mapper.Map<PatientViewModel>(patient); 
            return View(patientViewModel);
        }

        // GET: PatientController/Create
        public async Task<ActionResult> Create()
        {
             List<Doctor> doctors = await _doctorRepository.GetAllAsync();
             List<DoctorViewModel> doctorviewmodel = _mapper.Map<List<DoctorViewModel>>(doctors);
             PatientViewModel patientViewModel = new PatientViewModel();
             patientViewModel.Doctors = doctorviewmodel;
             return View(patientViewModel);
        }

        // POST: PatientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PatientViewModel patientViewModel)
        {
            try
            {
                Patient patient  = _mapper.Map<Patient>(patientViewModel);
                await _patient.AddAsync(patient);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PatientController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Patient patient = await _patient.GetByIdAsync(id);
            PatientViewModel patientViewModel = _mapper.Map<PatientViewModel>(patient);

            List<Doctor> doctors = await _doctorRepository.GetAllAsync();
            List<DoctorViewModel> doctorviewmodel = _mapper.Map<List<DoctorViewModel>>(doctors);
            patientViewModel.Doctors = new List<DoctorViewModel>();
            patientViewModel.Doctors = doctorviewmodel;
            return View(patientViewModel);
        }

        // POST: PatientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, PatientViewModel patientViewModel)
        {
            try
            {
                Patient patient = _mapper.Map<Patient>(patientViewModel);
                 await _patient.UpdateAsync(patient);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PatientController/Delete/5
        public async  Task<ActionResult> Delete(int id)
        {
            Patient patient = await _patient.GetByIdAsync(id);
            PatientViewModel patientViewModel = _mapper.Map<PatientViewModel>(patient);
            return View(patientViewModel);
        }

        // POST: PatientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                _patient.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
