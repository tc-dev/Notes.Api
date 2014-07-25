using System.Collections.Generic;
using System.Web.Http;
using Notes.Api.Core.Data;
using Notes.Api.Core.Data.Repositories;
using tc_dev.Core.Common.Utilities;

namespace Notes.Api.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController
    {
        private readonly INotesUnitOfWork _unitOfWork;
        private readonly IDomainUserRepository _domainUserRepo;

        public UserController(INotesUnitOfWork unitOfWork) {
            unitOfWork.ThrowIfNull("unitOfWork");

            _unitOfWork = unitOfWork;
            _domainUserRepo = unitOfWork.DomainUserRepository;
        }

        [HttpGet]
        public IEnumerable<string> Get() {
            return new[] {"user1", "user2"};
        }
    }
}