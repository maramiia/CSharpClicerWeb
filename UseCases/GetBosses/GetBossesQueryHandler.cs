using AutoMapper;
using CSharpClicker.Web.Infrastructure.Abstractions;
using CSharpClicker.Web.UseCases.GetBooses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.Web.UseCases.GetBosses;

public class GetBossesQueryHandler : IRequestHandler<GetBossesQuery, IReadOnlyCollection<BossDto>>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    public GetBossesQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
    }

    public async Task<IReadOnlyCollection<BossDto>> Handle(GetBossesQuery request, CancellationToken cancellationToken)
    {
        return await mapper
            .ProjectTo<BossDto>(appDbContext.Bosses)
            .ToArrayAsync();
    }
}
