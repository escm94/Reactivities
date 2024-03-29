using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
  public class List
  {
    public class Query : IRequest<Result<List<ActivityDTO>>> { }

    public class Handler : IRequestHandler<Query, Result<List<ActivityDTO>>>
    {
      private readonly DataContext _context;
      private readonly IMapper _mapper;

      public Handler(DataContext context, IMapper mapper)
      {
        _mapper = mapper;
        _context = context;
      }

      // since this is returning a Task, we need to make this async
      public async Task<Result<List<ActivityDTO>>> Handle(Query request, CancellationToken cancellationToken)
      {
        var activities = await _context.Activities
          .ProjectTo<ActivityDTO>(_mapper.ConfigurationProvider)
          .ToListAsync(cancellationToken);

        return Result<List<ActivityDTO>>.Success(activities);
      }
    }
  }
}