using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Services;
using UKParliament.CodeTest.Web.ViewModels;

namespace UKParliament.CodeTest.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonService _service;
        private readonly IMapper _mapper;


        public PersonController(ILogger<PersonController> logger, IPersonService service, IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Get person by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:int}")]
        [HttpGet("{id}")]
        public ActionResult<PersonViewModel> GetById(int id)
        {
            _logger.LogInformation("Selecting person by ID.");
            var personViewModel = _mapper.Map<PersonViewModel>(_service.GetPersonById(id));
            return Ok(personViewModel);
        }

        /// <summary>
        /// Get person list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IList<PersonViewModel>> GetByList()
        {
            _logger.LogInformation("Selecting people list.");
            var result = _service.GetPersonList().OrderBy(a => a.Id);
            var list = _mapper.Map<IList<PersonViewModel>>(result);
            return Ok(list);
        }

        /// <summary>
        /// Add new person
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public ActionResult AddPerson([FromBody] PersonViewModel model)
        {
            try
            {
                _logger.LogInformation("Including a person's data.");
                var person = _mapper.Map<Person>(model);
                var result = _service.PostNewPerson(person);

                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// update person
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("update/{id}")]
        public ActionResult UpdatePerson(int id, [FromBody] PersonViewModel model)
        {
            _logger.LogInformation("Updating person data.");
            var origin = _service.GetPersonById(id);
            Person person = _mapper.Map(model, origin);
            person.Id = id;
            var result = _service.PostUpdatePerson(person);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// delete person
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public ActionResult DeletePerson(int id)
        {
            try
            {
                _logger.LogInformation("Deleting a Person's Data.");
                var result = _service.PostDeletePerson(id);

                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}