using InvoicesNet7CQRS.Data.Entities;
using InvoicesNet7CQRS.Data.Interfaces;
using InvoicesNet7CQRS.Domain.Commands.CustomerCommands;
using InvoicesNet7CQRS.Domain.Commands.UserCommands;
using InvoicesNet7CQRS.Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvoicesNet7CQRS.Services.CommandHandlers.CustomerCommandHandler
{
    public class UserCommandHandler
    {
        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, GenericResponse<User>>
        {
            private readonly IDbContext _context;

            public CreateUserCommandHandler(IDbContext context)
            {
                _context = context;
            }

            public async Task<GenericResponse<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<User>();

                if (request.User is not null)
                {
                    await _context.Users.AddAsync(request.User);

                    await _context.SaveAsync();

                    response.Result = request.User;

                    response.Message = $"Saved successfully!";

                    return response;
                }

                response!.Message = "The user not created";

                return response;
            }
        }

        public class GetAllUsersCommandHander : IRequestHandler<GetAllUsersQuery, GenericResponse<IEnumerable<User>>>
        {
            private readonly IDbContext _context;

            public GetAllUsersCommandHander(IDbContext context)
            {
                _context = context;
            }

            public async Task<GenericResponse<IEnumerable<User>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<IEnumerable<User>>();

                var users = await _context.Users.AsNoTracking().ToListAsync(cancellationToken);

                response.Result = users;

                return response;
            }
        }

        public class GetUserByIdCommandHandler : IRequestHandler<GetUserByIdQuery, GenericResponse<User>>
        {
            private readonly IDbContext _context;

            public GetUserByIdCommandHandler(IDbContext context)
            {
                _context = context;
            }

            public async Task<GenericResponse<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<User>();

                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (user is not null)
                {
                    response.Result = user;

                    return response;
                }

                response.Message = "User not found!";

                return response;
            }
        }

        public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
        {
            private readonly IDbContext _context;

            public DeleteUserCommandHandler(IDbContext context)
            {
                _context = context;
            }

            public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FindAsync(request.Id, cancellationToken);

                _context.Users.Remove(user!);

                await _context.SaveAsync();
            }
        }

        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, GenericResponse<User>>
        {
            private readonly IDbContext _context;

            public UpdateUserCommandHandler(IDbContext context)
            {
                _context = context;
            }

            public async Task<GenericResponse<User>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<User>();

                var user = await _context.Users.FindAsync(request._user.Id, cancellationToken);

                if (user is not null)
                {
                    _context.Users.Entry(user).State = EntityState.Detached;

                    _context.Users.Update(request._user);

                    await _context.SaveAsync();

                    response.Result = request._user;

                    response.Message = $"User updated successfully.";

                    return response;
                }

                response.Message = "User not found!";

                return response;
            }
        }

        public class GetUserByUsernameCommandHandler : IRequestHandler<GetUserByUsernameQuery, GenericResponse<IEnumerable<User>>>
        {
            private readonly IDbContext _context;

            public GetUserByUsernameCommandHandler(IDbContext context)
            {
                _context = context;
            }

            public async Task<GenericResponse<IEnumerable<User>>> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<IEnumerable<User>>();

                var users = await _context.Users.AsNoTracking().Where(x => x.Username == request.UserName).ToListAsync(cancellationToken);

                response.Result = users;

                return response;
            }
        }
    }
}
