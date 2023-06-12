﻿using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Models;

namespace MyLeasing.Web.Controllers
{
    public class OwnersController : Controller
    {
        private readonly IOwnerRepository _ownerRepository;

        public OwnersController(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        // GET: Owners
        public IActionResult Index()
        {
            return View(_ownerRepository.GetAll());
        }

        // GET: Owners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _ownerRepository.GetByIdAsync(id.Value);
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // GET: Owners/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Owners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OwnerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if(model.ImageFile != null && model.ImageFile.Length > 0) 
                {
                    var guid = Guid.NewGuid().ToString();
                    var file = $"{guid}.jpg";


                    path = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot\\images\\owners",
                        file);

                    using(var stream = new FileStream(path, FileMode.Create)) 
                    { 
                        await model.ImageFile.CopyToAsync(stream);
                    }

                    path = $"~/images/owners/{file}";

                }

                var owner = this.ToOwner(model, path);

                //Todo: modificar para p o user que tiver logado.

                //owner.User = await _userHelper.GetUserByEmailAsync("tatiane.c.r.avellar@gmail.com");
                await _ownerRepository.CreateAsync(owner);
               
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        private Owner ToOwner(OwnerViewModel model, string path)
        {
            return new Owner
            {
                Id = model.Id,
                ImageUrl = path,
                Document = model.Document,
                FirstName = model.FirstName,
                LastName = model.LastName,
                FixedPhone = model.FixedPhone,
                CellPhone = model.CellPhone,
                Address = model.Address,
                User = model.User

            };
        }

        // GET: Owners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _ownerRepository.GetByIdAsync(id.Value);
            if (owner == null)
            {
                return NotFound();
            }

            var model = this.ToOwnerViewModel(owner); 
            return View(model);
        }

        private OwnerViewModel ToOwnerViewModel(Owner owner)
        {
            return new OwnerViewModel
            {
                Id = owner.Id,
                ImageUrl = owner.ImageUrl,
                Document = owner.Document,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                FixedPhone = owner.FixedPhone,
                CellPhone = owner.CellPhone,
                Address = owner.Address,
                User = owner.User
            };
        }



        // POST: Owners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OwnerViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    var path = model.ImageUrl;

                    if(model.ImageFile != null && model.ImageFile.Length > 0) 
                    {
                        var guid = Guid.NewGuid().ToString();
                        var file = $"{guid}.jpg";

                        path = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot\\images\\owners",
                            file);

                        using(var stream = new FileStream(path,FileMode.Create))
                        {
                            await model.ImageFile.CopyToAsync(stream);
                        }

                        path = $"~/images/owners/{file}";
                    }

                    var owner = this.ToOwner(model, path);

                    //Todo: modificar para p o user que tiver logado.
                    //owner.User = await _userHelper.GetUserByEmailAsync("tatiane.c.r.avellar@gmail.com");
                    await _ownerRepository.CreateAsync(owner);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _ownerRepository.ExistAsync(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Owners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _ownerRepository.GetByIdAsync(id.Value);
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // POST: Owners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var owner = await _ownerRepository.GetByIdAsync(id);
            await _ownerRepository.DeleteAsync(owner);
            return RedirectToAction(nameof(Index));
        }


    }
}
