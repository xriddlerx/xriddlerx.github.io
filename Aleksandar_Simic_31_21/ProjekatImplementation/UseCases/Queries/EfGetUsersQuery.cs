using ProjekatApplication.DTO;
using ProjekatApplication.DTO.Searches;
using ProjekatApplication.UseCases.Queries;
using ProjekatDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatImplementation.UseCases.Queries
{
    public class EfGetUsersQuery : EfUseCase,IGetUsersQuery
    {
        public EfGetUsersQuery(ProjekatContext context) : base(context)
        {
        }

        public int Id => 24;

        public string Name => "Search users using EF";

        public PagedResponse<GetUserDTO> Exectue(UserSearch search)
        {
            var query = Context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(search.FirstName) || !string.IsNullOrWhiteSpace(search.FirstName))
            {
                query = query.Where(x => x.FirstName.ToLower().Contains(search.FirstName.ToLower()));
            }

            if (!string.IsNullOrEmpty(search.LastName) || !string.IsNullOrWhiteSpace(search.LastName))
            {
                query = query.Where(x => x.LastName.ToLower().Contains(search.LastName.ToLower()));
            }

            if (!string.IsNullOrEmpty(search.Email) || !string.IsNullOrWhiteSpace(search.Email))
            {
                query = query.Where(x => x.Email.ToLower().Contains(search.Email.ToLower()));
            }

            var skipCount = search.PerPage * (search.Page - 1);

            var response = new PagedResponse<GetUserDTO>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage).Select(x => new GetUserDTO
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email
                }).ToList()
            };

            return response;
        }
    }
}
