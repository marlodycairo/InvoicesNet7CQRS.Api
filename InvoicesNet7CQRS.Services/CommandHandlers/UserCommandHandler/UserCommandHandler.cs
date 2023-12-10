using InvoicesNet7CQRS.Data.Context;
using InvoicesNet7CQRS.Data.Entities;
using InvoicesNet7CQRS.Domain.Commands.CustomerCommands;
using InvoicesNet7CQRS.Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvoicesNet7CQRS.Services.CommandHandlers.CustomerCommandHandler
{
    public class UserCommandHandler
    {
        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, GenericResponse<User>>
        {
            private readonly ApplicationDbContext _context;

            public CreateUserCommandHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<GenericResponse<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<User>();

                if (response is null)
                {
                    response!.Message = "El usuario no pudo ser creado";

                    return response;
                }

                await _context.Users.AddAsync(request.User);

                await _context.SaveChangesAsync(cancellationToken);

                response.Result = request.User;

                response.Message = $"Guardado exitosamente!";

                return response;
            }
        }

        public class GetAllUsersCommandHander : IRequestHandler<GetAllUsersQuery, GenericResponse<IEnumerable<User>>>
        {
            private readonly ApplicationDbContext _context;

            public GetAllUsersCommandHander(ApplicationDbContext context)
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
            private readonly ApplicationDbContext _context;

            public GetUserByIdCommandHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<GenericResponse<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<User>();

                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (user is null)
                {
                    response.Message = "Usuario no encontrado";
                }

                response.Result = user;

                return response;
            }
        }

        public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
        {
            private readonly ApplicationDbContext _context;

            public DeleteUserCommandHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FindAsync(request.Id, cancellationToken);

                _context.Users.Remove(user!);

                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, GenericResponse<User>>
        {
            private readonly ApplicationDbContext _context;

            public UpdateUserCommandHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<GenericResponse<User>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericResponse<User>();

                //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

                var user = await _context.Users.FindAsync(request._user.Id, cancellationToken);

                if (user is null)
                {
                    response.Message = "Usuario no encontrado.";

                    return response;
                }

                _context.Users.Update(request._user);

                await _context.SaveChangesAsync(cancellationToken);

                response.Result = request._user;

                response.Message = $"Usuario actualizado exitosamente";

                return response;
            }
        }
    }
}
