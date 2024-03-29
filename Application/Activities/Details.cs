using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
  public class Details
  {
    public class Query : IRequest<Result<ActivityDTO>>
    {
      public Guid Id { get; set; }

    }

    public class Handler : IRequestHandler<Query, Result<ActivityDTO>>
    {
      private readonly DataContext _context;
      private readonly IMapper _mapper;
      public Handler(DataContext context, IMapper mapper)
      {
        _mapper = mapper;
        _context = context;
      }

      public async Task<Result<ActivityDTO>> Handle(Query request, CancellationToken cancellationToken)
      {
        var activity = await _context.Activities
          .ProjectTo<ActivityDTO>(_mapper.ConfigurationProvider)
          .FirstOrDefaultAsync(x => x.Id == request.Id); // can't use FindAsync when using projections

        return Result<ActivityDTO>.Success(activity);
      }
    }
  }
}