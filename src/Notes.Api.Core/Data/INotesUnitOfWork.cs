using Notes.Api.Core.Data.Repositories;
using tc_dev.Core.Data;

namespace Notes.Api.Core.Data
{
    public interface INotesUnitOfWork : IUnitOfWork
    {
        IDomainUserRepository DomainUserRepository { get; }

        INoteBookRepository NoteBookRepository { get; }

        IPageRepository PageRepository { get; }

        INoteRepository NoteReposigory { get; }
    }
}
