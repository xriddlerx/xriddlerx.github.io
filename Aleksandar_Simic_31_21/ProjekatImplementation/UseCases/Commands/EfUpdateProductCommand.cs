using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProjekatApplication.DTO;
using ProjekatApplication.UseCases.Commands.Products;
using ProjekatDataAccess;
using ProjekatDomain;
using ProjekatImplementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatImplementation.UseCases.Commands
{
    public class EfUpdateProductCommand : EfUseCase,IUpdateProductCommand
    {
        private readonly UpdateProductValidator _validator;
        private static IEnumerable<string> allowedExtentions = new List<string>
        {
            ".jpg", ".png", ".jpeg"
        };
        public EfUpdateProductCommand(ProjekatContext context, UpdateProductValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 13;

        public string Name => "Update Product using EF";

        public void Execute(UpdateProductDTO data)
        {
            var product = Context.Products.Include(x => x.BrandCategory)
                                                  .Include(x => x.Gallery)
                                                  .Include(x => x.Prices)
                                                  .Include(x => x.BrandCategory.Brand)
                                                  .Include(x => x.BrandCategory.Category)
                                                  .FirstOrDefault(p => p.Id == data.ProductId);

            if(product == null)
            {
                throw new KeyNotFoundException();
            }

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

            product.Id = data.ProductId;
            product.Name = data.Name;
            product.Description = data.Description;
            product.BrandCategoryId = data.BrandCategoryId;
            Price price = new Price
            {
                ProductId = product.Id,
                ProductPrice = data.Price,
                DateOfPrice = DateTime.UtcNow
            };
            Gallery gallery = new Gallery
            {
                Product = product,
                PathName = fileName
            };
            Context.Prices.Add(price);
            Context.Galleries.Add(gallery);
            Context.SaveChanges();
        }
    }
}
