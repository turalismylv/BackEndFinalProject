﻿namespace Web.Areas.Admin.ViewModels.HomeChooseComponent
{
    public class HomeChooseComponentUpdateVM
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
      
        public IFormFile? MainPhoto { get; set; }
        public string? MainPhotoPath { get; set; }
    }
}
