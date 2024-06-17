using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProjekatApplication.DTO;
using ProjekatApplication.UseCases.Commands.Products;
using ProjekatDataAccess;
using ProjekatDomain;
using ProjekatImplementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatImplementation.UseCases.Commands
{
    public class EfCreateProductCommand : EfUseCase,ICreateProductCommand
    {
        private CreateProductValidator _validator;
        private static IEnumerable<string> allowedExtentions = new List<string>
        {
            ".jpg", ".png", ".jpeg" 
        };
        public EfCreateProductCommand(ProjekatContext context, CreateProductValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 9;

        public string Name => "Create product using EF";

        public void Execute(ProductCreateDTO data)
        {
            _validator.ValidateAndThrow(data);

            var extension = Path.GetExtension(data.Image.FileName).ToLower();

            if (!allowedExtentions.Contains(extension))
            {
                throw new Exception("This extention is not supported");
            }

            var fileName = Guid.NewGuid().ToString() + extension;
            var tmpFile = Path.Combine("wwwroot", "tmp", fileName);
            var destinationFile = Path.Combine("wwwroot", "gallery", fileName);

            using var fs = new FileStream(tmpFile, FileMode.Create);
            data.Image.CopyTo(fs);

            using var fs2 = new FileStream(destinationFile, FileMode.Create);
            data.Image.CopyTo(fs2);

            Product product = new Product
            {
                Name = data.Name,
                Description = data.Description,
                BrandCategoryId = data.BrandCategoryId,
                Prices = new List<Price> 
                { 
                    new Price
                    {
                        ProductPrice = data.Price,
                        DateOfPrice = DateTime.UtcNow,
                    }
                },
                Gallery = new Gallery 
                {
                    PathName = fileName
                }
            };

            Context.Products.Add(product);
            Context.SaveChanges();
        }
    }
}
