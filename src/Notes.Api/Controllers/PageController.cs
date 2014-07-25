using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Notes.Api.Core.Data;
using Notes.Api.Core.Data.Repositories;
using Notes.Api.Core.Domain.Models;
using Notes.Api.Models;
using tc_dev.Core.Common.Utilities;

namespace Notes.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/page")]
    public class PageController : ApiController
    {
        private readonly INotesUnitOfWork _unitOfWork;
        private readonly IPageRepository _pageRepository;

        public PageController(INotesUnitOfWork unitOfWork) {
            unitOfWork.ThrowIfNull("unitOfWork");

            _unitOfWork = unitOfWork;
            _pageRepository = unitOfWork.PageRepository;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Get(int id, bool includeNotes = false) {
            try {
                var query = _pageRepository.Query().ById(id);
                if (includeNotes)
                    query.Include(m => m.Notes);
                var pageEntity = await query.FirstOrDefaultAsync();

                if (pageEntity == null)
                    return NotFound();

                var model = new PageModel {
                    Id = pageEntity.Id,
                    NoteBookId = pageEntity.NoteBookId,
                    ColorHex = pageEntity.ColorHex,
                    Title = pageEntity.Title,
                    Notes = !pageEntity.Notes.Any() 
                        ? null
                        : pageEntity.Notes.Select(n =>
                            new NoteModel {
                                Id = n.Id,
                                PageId = n.PageId,
                                DateCreated = n.DateCreated,
                                DateLastModified = n.DateLastModified,
                                Text = n.Text
                            }).ToList()
                };

                return Ok(model);
            }
            catch {
                return InternalServerError();
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(PageBindingModel model) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try {
                var page = new Page {
                    NoteBookId = model.NoteBookId,
                    ColorHex = model.ColorHex,
                    Title = model.Title
                };

                _pageRepository.Insert(page);
                await _unitOfWork.SaveChangesAsync();

                var uri = Url.Link("DefaultApi", new {id = page.Id});
                return Created(uri, page);
            }
            catch {
                return InternalServerError(); // TODO
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Put(int id, PageBindingModel model) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try {
                var pageEntity = await _pageRepository.Query()
                    .ById(id)
                    .FirstOrDefaultAsync();

                if (pageEntity == null)
                    return NotFound();

                pageEntity.NoteBookId = model.NoteBookId;
                pageEntity.Title = model.Title;
                pageEntity.ColorHex = model.ColorHex;

                _pageRepository.Update(pageEntity);
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
                var pageEntity = await _pageRepository.Query()
                    .ById(id)
                    .FirstOrDefaultAsync();

                if (pageEntity == null)
                    return NotFound();

                _pageRepository.Remove(pageEntity);
                await _unitOfWork.SaveChangesAsync();

                return Ok();
            }
            catch {
                return InternalServerError(); // TODO
            }
        }
    }
}