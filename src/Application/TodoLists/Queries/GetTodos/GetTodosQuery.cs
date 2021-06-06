namespace CleanCli.Todo.Application.TodoLists.Queries.GetTodos
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using CleanCli.Todo.Application.Common.Interfaces;
    using CleanCli.Todo.Domain.Enums;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetTodosQuery : IRequest<TodosVm>
    {
    }

    public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, TodosVm>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetTodosQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<TodosVm> Handle(GetTodosQuery request, CancellationToken cancellationToken) => new TodosVm
        {
            PriorityLevels = Enum.GetValues(typeof(PriorityLevel))
                    .Cast<PriorityLevel>()
                    .Select(p => new PriorityLevelDto { Value = (int)p, Name = p.ToString() })
                    .ToList(),

            Lists = await this.context.TodoLists
                    .Include(t => t.Items)
                    .AsNoTracking()
                    .ProjectTo<TodoListDto>(this.mapper.ConfigurationProvider)
                    .OrderBy(t => t.Title)
                    .ToListAsync(cancellationToken)
        };
    }
}
