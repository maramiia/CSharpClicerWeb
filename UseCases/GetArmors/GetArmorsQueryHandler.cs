using AutoMapper;
using CSharpClicker.Web.Infrastructure.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.Web.UseCases.GetArmors
{
    public class GetArmorsQueryHandler : IRequestHandler<GetArmorsQuery, IReadOnlyCollection<ArmorDto>>
    {
        private IAppDbContext appDbContext;
        private  IMapper mapper;

        public GetArmorsQueryHandler(IAppDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }

        public async Task<IReadOnlyCollection<ArmorDto>> Handle(GetArmorsQuery request, CancellationToken cancellationToken)
        {
            return await mapper
                .ProjectTo<ArmorDto>(appDbContext.Armors)
                .ToArrayAsync();
        }
    }
}
