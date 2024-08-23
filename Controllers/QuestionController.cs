
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectApi.Data;
using ProjectApi.Dtos;
using ProjectApi.Services;
using Microsoft.EntityFrameworkCore;
using Question = ProjectApi.Dtos.Question;
using TestTrainee = ProjectApi.Dtos.TestTrainee;


namespace ProjectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService questionService;
        private readonly QuestionBankDatabaseContext _context;
        public QuestionController(IQuestionService questionService, QuestionBankDatabaseContext context)
        {
            this.questionService = questionService;
            _context = context;
        }
        [HttpGet("AllQuestions")]
        public async Task<IEnumerable<Question>> GetAllQuestions()
        {
            var data = await questionService.GetAllQuestions();
            return data;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> FindQuestion(int id)
        {
            try
            {
                var rec = await questionService.FindQuestion(id);
                return Ok(rec);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateQuestion([FromBody] Question question)
        {
            try
            {
                await questionService.UpdateQuestion(question);
                return Ok();
            }
            catch (Exception ex)
            {
                var message = new { Message = ex.Message };
                return NotFound(message);
            }
        }

        [HttpPost] //single question of type text
        public async Task<ActionResult> AddSingleQuestion(Question question)
        {
            try
            {
                await questionService.AddQuestion(question);
                return Ok();
            }
            catch (Exception ex) 
            {
                var msg = new { Mesage = ex.Message };
                return NotFound(msg);
            }
        }

        [HttpPost("BulkQuestions")] //Bulk upload from excel
        public async Task<IActionResult> AddQuestionsInBulk(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("No file uploaded");
                await questionService.AddQuestionsInBulk(file);
                return Ok();
            }
            catch (Exception ex)
            {
                var message = new { Message = ex.Message };
                return NotFound(message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteQuestion(int id)
        {
            try
            {
                await questionService.DeleteQuestion(id);
                return Ok();
            }
            catch (Exception ex)
            {
                var msg = new { Mesage = ex.Message };
                return NotFound(msg);
            }
        }
    }
}
