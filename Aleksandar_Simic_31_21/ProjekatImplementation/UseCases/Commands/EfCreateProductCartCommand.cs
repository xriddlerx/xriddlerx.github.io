using FluentValidation;
using ProjekatApplication;
using ProjekatApplication.DTO;
using ProjekatApplication.UseCases.Commands.Cart;
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
    public class EfCreateProductCartCommand : EfUseCase,ICreateProductCartCommand
    {
        private readonly IApplicationActor _actor;
        private readonly CreateCartValidator _validator;
        public EfCreateProductCartCommand(ProjekatContext context, IApplicationActor actor, CreateCartValidator validator) : base(context)
        {
            _actor = actor;
            _validator = validator;
        }

        public int Id => 16;

        public string Name => "Adding products to cart using EF";

        public void Execute(CartCreateForUserDTO data)
        {
            _validator.ValidateAndThrow(data);
            CheckCartForUser(_actor.Id);
            var cartList = ListOfUniqueElements(data.Products);
            var updatedList = CheckForDuplicatesInDatabase(cartList);
            int cartId = Context.Carts.Where(x => x.UserId == _actor.Id).Select(x => x.Id).FirstOrDefault();
            InsertProductInCart(updatedList, cartId);
        }

        public void CheckCartForUser(int id)
        {
            if (!Context.Carts.Any(x => x.UserId == id))
            {
                Cart c = new Cart
                {
                    UserId = id,
                };

                Context.Carts.Add(c);
                Context.SaveChanges();
            }
        }
        public IEnumerable<ProductDTO> ListOfUniqueElements(IEnumerable<ProductDTO> products)
        {
            List<ProductDTO> newList = new List<ProductDTO>();
            int br = 0;
            foreach (ProductDTO product in products)
            {
                if(br == 0)
                {
                    newList.Add(product);
                }
                else 
                {
                    if(newList.Any(x => x.ProductId == product.ProductId))
                    {
                        newList.Where(x => x.ProductId == product.ProductId).First().Quantity += product.Quantity;                        
                    }
                    else
                    {
                        newList.Add(product);
                    }
                }
                br++;
            }
            return newList;
        }
        public IEnumerable<ProductDTO> CheckForDuplicatesInDatabase(IEnumerable<ProductDTO> products)
        {
            List<ProductDTO> newList = new List<ProductDTO>();
            foreach (ProductDTO product in products)
            {
                if (Context.ProductCart.Any(x => x.ProductId == product.ProductId))
                {
                    var productFromCart = Context.ProductCart.FirstOrDefault(x => x.ProductId == product.ProductId);
                    productFromCart.Quantity += product.Quantity;
                    Context.SaveChanges();
                }
                else
                {
                    newList.Add(product);
                }
            }
            return newList;
        }
        public void InsertProductInCart(IEnumerable<ProductDTO> products, int cartId)
        {
            if(products.Count() > 0)
            {
                foreach (ProductDTO product in products)
                {
                    ProductCart productCart = new ProductCart
                    {
                        CartId = cartId,
                        ProductId = product.ProductId,
                        Quantity = product.Quantity
                    };
                    Context.ProductCart.Add(productCart);
                    Context.SaveChanges();
                }
            }
        }
    }
}
