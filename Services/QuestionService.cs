using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using ProjectApi.Data;
using ProjectApi.Dtos;
using System.Drawing.Imaging;
using Question = ProjectApi.Dtos.Question;

namespace ProjectApi.Services
{
    public interface IQuestionService
    {
        public Task AddQuestion(Question question);
        public Task DeleteQuestion(int questionId);
        public Task UpdateQuestion(Question question);
        public Task<List<Question>> GetAllQuestions();
        public Task<Question> FindQuestion(int questionId);
        public Task AddQuestionsInBulk(IFormFile file);
    }
    public class QuestionService : IQuestionService
    {
        private readonly QuestionBankDatabaseContext _context;
        public QuestionService(QuestionBankDatabaseContext context)
        {
            _context = context;
        }
        public Task AddQuestion(Question question)
        {
            try
            {
                _context.Questions.Add(new Data.Question
                {
                    //QuestionId = question.QuestionId,
                    Subject = question.Subject,
                    Topic = question.Topic,
                    DifficultyLevel = question.DifficultyLevel,
                    QuestionText = question.QuestionText,
                    OptionA = question.OptionA,
                    OptionB = question.OptionB,
                    OptionC = question.OptionC,
                    OptionD = question.OptionD,
                    CorrectAnswer = question.CorrectAnswer,
                    CreatedBy = question.CreatedBy,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                });
                _context.SaveChanges();
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

        public Task AddQuestionsInBulk(IFormFile file)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var questions = new List<Data.Question>();
            var imagePath = "C:\\Users\\6147956\\source\\repos\\ProjectApi\\Images\\";

            if (!Directory.Exists(imagePath))
                Directory.CreateDirectory(imagePath);

            using (var package = new ExcelPackage(file.OpenReadStream()))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    var question = new Data.Question
                    {
                        Subject = ProcessOption(worksheet.Cells[row, 1], imagePath),
                        Topic = ProcessOption(worksheet.Cells[row, 2], imagePath),
                        DifficultyLevel = ProcessOption(worksheet.Cells[row, 3], imagePath),
                        QuestionText = ProcessOption(worksheet.Cells[row, 4], imagePath),
                        OptionA = ProcessOption(worksheet.Cells[row, 5], imagePath),
                        OptionB = ProcessOption(worksheet.Cells[row, 6], imagePath),
                        OptionC = ProcessOption(worksheet.Cells[row, 7], imagePath),
                        OptionD = ProcessOption(worksheet.Cells[row, 8], imagePath),
                        CorrectAnswer = ProcessOption(worksheet.Cells[row, 9], imagePath),
                        CreatedBy = 1,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,

                    };
                    _context.Add(question);
                    _context.SaveChanges();


                    //questions.Add(question);
                }
            }


            //_context.SaveChangesAsync();

            return Task.CompletedTask;
        }

        private string ProcessOption(ExcelRange cell, string imagePath)
        {


            if (cell.Value is string)
            {
                return cell.Text;
            }
            else
            {
                // Get the row and column of the cell
                int row = cell.Start.Row;
                int column = cell.Start.Column;

                // Check if the worksheet contains any drawings (images)
                var worksheet = cell.Worksheet;
                if (worksheet.Drawings.Count > 0)
                {
                    foreach (var drawing in worksheet.Drawings)
                    {
                        if (drawing is ExcelPicture picture && picture.From.Row == row - 1 && picture.From.Column == column - 1)
                        {
                            // Generate a unique file name for the image
                            var imageName = Guid.NewGuid().ToString() + ".jpg";
                            var imageFullPath = Path.Combine(imagePath, imageName);
                            // Save the image to the specified path on the server
                            // using (var imgStream = new MemoryStream(picture.Image))
                            //{
                            // //   using (var img = Image.FromStream(imgStream))
                            //    {
                            //        img.Save(imageFullPath, ImageFormat.Jpeg);
                            //    }
                            //}

                            picture.Image.Save(imageFullPath, ImageFormat.Jpeg); //For the save to work, you should have the EPPlus 5.0.3v installed(latest version doesn't work).
                            return $"/Images/{imageName}";
                        }
                    }
                }

                // If no image is found in the specified cell, return the cell's text value
                return cell.Text;
            }
        }


        public Task DeleteQuestion(int questionId)
        {
            try
            {
                var question = _context.Questions.FirstOrDefault(q => q.QuestionId == questionId);
                _context.Remove(question);
                _context.SaveChanges();
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

        public async Task<Question> FindQuestion(int questionId)
        {
            try
            {
                var question = await _context.Questions.FirstOrDefaultAsync(q => q.QuestionId == questionId);
                if (question == null)
                {
                    throw new Exception("Question record not found");
                }
                return new Question
                {
                    QuestionId = questionId,
                    Subject = question.Subject,
                    Topic = question.Topic,
                    DifficultyLevel = question.DifficultyLevel,
                    QuestionText = question.QuestionText,
                    OptionA = question.OptionA,
                    OptionB = question.OptionB,
                    OptionC = question.OptionC,
                    OptionD = question.OptionD,
                    CorrectAnswer = question.CorrectAnswer,
                    CreatedBy = (int)(question?.CreatedBy),
                    CreatedAt = (DateTime)(question?.CreatedAt),
                    UpdatedAt  = (DateTime)(question?.UpdatedAt)
                };
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

        public async Task<List<Question>> GetAllQuestions()
        {
            try
            {
                var data = _context.Questions.Select((q) => new Question
                {
                    QuestionId = (int)q.QuestionId,
                    Subject = q.Subject,
                    Topic = q.Topic,
                    DifficultyLevel = q.DifficultyLevel,
                    QuestionText = q.QuestionText,
                    OptionA = q.OptionA,
                    OptionB = q.OptionB,
                    OptionC = q.OptionC,
                    OptionD = q.OptionD,
                    CorrectAnswer= q.CorrectAnswer,
                    CreatedBy = (int)q.CreatedBy,
                    CreatedAt = (DateTime)(q.CreatedAt),
                    UpdatedAt = (DateTime)(q.UpdatedAt)
                    //UpdatedAt = (DateTime)q.UpdatedAt,
                    //CreatedAt = (DateTime)q.CreatedAt,
                    //CreatedBy = (int)q.CreatedBy
                });
                //Console.WriteLine(data);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

        public Task UpdateQuestion(Question question)
        {
            try
            {
                var que = _context.Questions.SingleOrDefault(q => q.QuestionId == question.QuestionId);
                que.QuestionId = question.QuestionId;
                que.Subject = question.Subject;
                que.Topic = question.Topic;
                que.DifficultyLevel = question.DifficultyLevel;
                que.QuestionText = question.QuestionText;
                que.OptionA = question.OptionA;
                que.OptionB = question.OptionB;
                que.OptionC = question.OptionC;
                que.OptionD = question.OptionD;
                que.CorrectAnswer = question.CorrectAnswer; 
                question.CreatedBy = question.CreatedBy;
                question.CreatedAt = question.CreatedAt;
                que.UpdatedAt = DateTime.Now;
                return _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }
    }
}
