using Application.Core;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Activities
{
  public class Create
  {
    public class Command : IRequest<Result<Unit>>
    {
      public Activity Activity { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command> 
    {
      public CommandValidator()
      {
        RuleFor(x => x.Activity).SetValidator(new ActivityValidator());
      }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
      private readonly DataContext _context;
      public Handler(DataContext context)
      {
        _context = context;
      } 

      public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
      {
        // typically, you'd use AddAsync in the event you were working with a database. for now, we won't
        _context.Activities.Add(request.Activity);

        var result = await _context.SaveChangesAsync() > 0;

        if (!result) return Result<Unit>.Failure("Failed to create activity."); // returns 400 bad request with this message

        return Result<Unit>.Success(Unit.Value);

        // technically, this interface should return what's called a Unit.
        // Units are just an object that MediatR provides that's essentially like returning nothing
        // it's used to tell our API that the request is finished and to move on
        // return Unit.Value;
      }
    }
  }
}