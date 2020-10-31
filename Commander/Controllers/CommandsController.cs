using System.Collections.Generic;
using AutoMapper;
using Data;
using DTOs;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers
{
    //api/commands or whatever current name of controller is
    //[Route("api/[controller]")]
    //api/commands
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;
        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        //api/commands/{id}
        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);
            if (commandItem != null)
            {
                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(Command cmd)
        {
            var commandModel = _mapper.Map<Command>(cmd);
            _repository.CreateCommand(commandModel);
            var saved = _repository.SaveChanges();
            if (saved)
            {
                var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);
                return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDto.Id }, commandReadDto);
            }
            return NoContent();
        }

        //PUT api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto cmd)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(cmd, commandModelFromRepo);
            _repository.UpdateCommand(commandModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDocument)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }
            //for Patch
            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);
            //ModelState is doing the validation that exists on model
            patchDocument.ApplyTo(commandToPatch, ModelState);
            //return validation if modelstate fails
            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }
            //same as update
            _mapper.Map(commandToPatch, commandModelFromRepo);
            _repository.UpdateCommand(commandModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
        //DELETE api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteCommand(commandModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        
     }
    }
}