using AutoMapper;
using CSharpClicker.Web.Infrastructure.Abstractions;
using CSharpClicker.Web.UseCases.GetWeapons;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.Web.UseCases.GetWeapons
{
    public class GetWeaponsQueryHandler : IRequestHandler<GetWeaponsQuery, IReadOnlyCollection<WeaponDto>>
    {
        private readonly IAppDbContext appDbContext;
        private readonly IMapper mapper;

        public GetWeaponsQueryHandler(IAppDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }

        public async Task<IReadOnlyCollection<WeaponDto>> Handle(GetWeaponsQuery request, CancellationToken cancellationToken)
        {
            return await mapper
                .ProjectTo<WeaponDto>(appDbContext.Weapons)
                .ToArrayAsync();
        }
    }
}



