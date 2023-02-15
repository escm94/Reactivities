using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
  public class Create
  {
    public class Command : IRequest
    {
      public Activity Activity { get; set; }
    }

    public class Handler : IRequestHandler<Command>
    {
      private readonly DataContext _context;
      public Handler(DataContext context)
      {
        _context = context;
      }

      // technically, this interface should return what's called a Unit.
      // Units are just an object that MediatR provides that's essentially like returning nothing
      // it's used to tell our API that the request is finished and to move on
      public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
      {
        // typically, you'd use AddAsync in the event you were working with a database. for now, we won't
        _context.Activities.Add(request.Activity);

        await _context.SaveChangesAsync();

        return Unit.Value;
      }
    }
  }
}