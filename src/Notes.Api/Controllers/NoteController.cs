using System.Threading.Tasks;
using System.Web.Http;
using Notes.Api.Core.Data;
using Notes.Api.Core.Data.Repositories;
using Notes.Api.Core.Domain.Models;
using Notes.Api.Models;
using tc_dev.Core.Common.Utilities;
using tc_dev.Core.Identity;

namespace Notes.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/note")]
    public class NoteController : ApiController
    {
        private readonly INotesUnitOfWork _unitOfWork;
        private readonly INoteRepository _noteRepository;

        public NoteController(INotesUnitOfWork unitOfWork) {
            unitOfWork.ThrowIfNull("unitOfWork");

            _unitOfWork = unitOfWork;
            _noteRepository = unitOfWork.NoteRepository;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Get(int id) {
            try {
                var noteEntity = await _noteRepository.Query()
                    .ById(id)
                    .FirstOrDefaultAsync();

                if (noteEntity == null)
                    return NotFound();

                var model = new NoteModel {
                    Id = noteEntity.Id,
                    PageId = noteEntity.PageId,
                    DateCreated = noteEntity.DateCreated,
                    DateLastModified = noteEntity.DateLastModified,
                    Text = noteEntity.Text
                };

                return Ok(model);
            }
            catch {
                return InternalServerError(); // TODO
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(NoteBindingModel model) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try {
                var note = new Note {
                    PageId = model.PageId,
                    Text = model.Text
                };

                _noteRepository.Insert(note);
                await _unitOfWork.SaveChangesAsync();

                var uri = Url.Link("DefaultApi", new {id = note.Id});
                return Created(uri, note);
            }
            catch {
                return InternalServerError(); // TODO
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Put(int id, NoteBindingModel model) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try {
                var noteEntity = await _noteRepository.Query()
                    .ById(id)
                    .FirstOrDefaultAsync();

                if (noteEntity == null)
                    return NotFound();

                noteEntity.PageId = model.PageId;
                noteEntity.Text = model.Text;

                _noteRepository.Update(noteEntity);
                await _unitOfWork.SaveChangesAsync();

                return Ok();
            }
            catch {
                return InternalServerError(); // TODO
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id) {
            try {
                var noteEntity = await _noteRepository.Query()
                    .ById(id)
                    .FirstOrDefaultAsync();

                if (noteEntity == null)
                    return NotFound();

                _noteRepository.Remove(noteEntity);
                await _unitOfWork.SaveChangesAsync();

                return Ok();
            }
            catch {
                return InternalServerError(); // TODO
            }
        }
    }
}