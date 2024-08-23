using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using ProjectApi.Models;
using ProjectApi.Dtos;
using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using System.Text;
using Image = System.Drawing.Image;
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
        public Task<byte[]> GeneratePdf(List<int> questionIds);
        public Task UpdateQuestionAsync(Question question, IFormFile optionAFile, IFormFile optionBFile, IFormFile optionCFile, IFormFile optionDFile);
    }
    public class QuestionService : IQuestionService
    {
        private readonly QuestionBankDatabaseContext _context;
        private static readonly string _imageFolder = "C:\\Users\\6147956\\source\\repos\\ProjectApi\\Images\\";
        public QuestionService(QuestionBankDatabaseContext context)
        {
            _context = context;
        }
        public Task AddQuestion(Question question)
        {
            try
            {
                _context.Questions.Add(new Models.Question
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
                    Explaination = question.Explaination
                    //UpdatedAt = DateTime.UtcNow
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
            var questions = new List<Models.Question>();
            var imagePath = "C:\\Users\\6147956\\source\\repos\\ProjectApi\\Images\\";

            if (!Directory.Exists(imagePath))
                Directory.CreateDirectory(imagePath);

            using (var package = new ExcelPackage(file.OpenReadStream()))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    var question = new Models.Question
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
                        Explaination = ProcessOption(worksheet.Cells[row, 10], imagePath),
                        CreatedBy = 1,
                        CreatedAt = DateTime.Now
                        //UpdatedAt = DateTime.Now,

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
                    Explaination = question.Explaination,
                    CreatedBy = (int)(question?.CreatedBy),
                    CreatedAt = (DateTime)(question?.CreatedAt),
                    //UpdatedAt  = (DateTime)(question?.UpdatedAt)
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
                    CorrectAnswer = q.CorrectAnswer,
                    Explaination = q.Explaination,
                    CreatedBy = (int)q.CreatedBy,
                    CreatedAt = (DateTime)(q.CreatedAt),
                    //UpdatedAt = (DateTime)(q.UpdatedAt)
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
                que.Explaination = question.Explaination;
                question.CreatedBy = question.CreatedBy;
                question.CreatedAt = question.CreatedAt;
                //que.UpdatedAt = DateTime.Now;
                return _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex.InnerException ?? ex;
            }
        }

        public async Task<byte[]> GeneratePdf(List<int> questionIds)
        {
            try
            {
                var questions = await _context.Questions.Where(q => questionIds.Contains(q.QuestionId)).ToListAsync();
                if (questions == null || questions.Count == 0)
                {
                    throw new ArgumentException("No questions found for the provided IDs.");
                }
                using (var ms = new MemoryStream())
                {
                    PdfWriter writer = new PdfWriter(ms);
                    PdfDocument pdf = new PdfDocument(writer);
                    Document document = new Document(pdf);

                    // Add questions to the PDF
                    foreach (var question in questions)
                    {
                        AddContentToPdf(document, "Question", question.QuestionText);
                        AddContentToPdf(document, "A", question.OptionA);
                        AddContentToPdf(document, "B", question.OptionB);
                        AddContentToPdf(document, "C", question.OptionC);
                        AddContentToPdf(document, "D", question.OptionD);
                        document.Add(new Paragraph(" ")); // Empty line
                    }

                    document.Close();

                    return ms.ToArray();

                }
            }
            catch (Exception pdfEx)
            {
                throw new Exception(pdfEx.Message);
            }
        }
        private void AddContentToPdf(Document document, string optionLabel, string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                document.Add(new Paragraph($"{optionLabel}: N/A"));
            }
            else if (IsImagePath(content))
            {
                // Assuming the content is a relative image path
                var imagePath = Path.Combine("C:\\Users\\6147954\\source\\repos\\ProjectAPI\\", content);

                if (File.Exists(imagePath))
                {
                    ImageData imageData = ImageDataFactory.Create(imagePath);
                    iText.Layout.Element.Image image = new iText.Layout.Element.Image(imageData);
                    float imageHeightInPoints = 10 * 12f; // Approx. 12 points per line, so 15 lines = 15 * 12 = 180 points
                    float imageAspectRatio = image.GetImageWidth() / image.GetImageHeight(); // Width/Height
                    float imageWidthInPoints = imageHeightInPoints * imageAspectRatio;

                    image.ScaleToFit(imageWidthInPoints, imageHeightInPoints);
                    document.Add(new Paragraph($"{optionLabel}:"));
                    document.Add(image);
                }
                else
                {
                    document.Add(new Paragraph($"{optionLabel}: [Image not found]"));
                }
            }
            else
            {
                // Assuming the content is plain text
                document.Add(new Paragraph($"{optionLabel}: {content}"));
            }
        }
        private bool IsImagePath(string content)
        {
            // Check if the content is a relative path (e.g., "Images\file.png" or "Images/file.jpg")
            return content.StartsWith("Images\\") || content.StartsWith("Images/");
        }





        public async Task UpdateQuestionAsync(Question question, IFormFile optionAFile, IFormFile optionBFile, IFormFile optionCFile, IFormFile optionDFile)
        {
            var _question = _context.Questions.FirstOrDefault(q => q.QuestionId == question.QuestionId);
            if (_question == null)
            {
                throw new KeyNotFoundException("Question not found");
            }
            _question.Subject = question.Subject;
            _question.Topic = question.Topic;
            _question.DifficultyLevel = question.DifficultyLevel;
            _question.QuestionText = question.QuestionText;
            _question.OptionA = await ProcessOptionAsync(optionAFile, question.OptionA, _question.OptionA);
            _question.OptionB = await ProcessOptionAsync(optionAFile, question.OptionB, _question.OptionB);
            _question.OptionC = await ProcessOptionAsync(optionAFile, question.OptionC, _question.OptionC);
            _question.OptionD = await ProcessOptionAsync(optionAFile, question.OptionD, _question.OptionD);
            _question.CorrectAnswer = question.CorrectAnswer;
            _context.Questions.Update(_question);
            await _context.SaveChangesAsync();
        }
        private async Task<string> ProcessOptionAsync(IFormFile optionFile, string optionText, string oldValue)
        {
            if (optionFile != null && optionFile.Length > 0)
            {
                // Delete the old image if it exists and is an image path
                if (IsTheImagePath(oldValue))
                {
                    DeleteImage(oldValue);
                }

                // Save the new image and return its relative path
                return await SaveImageAsync(optionFile);
            }

            // If no file is provided, use the provided text (or retain the existing text)
            return optionText ?? oldValue;
        }
        private bool IsTheImagePath(string value)
        {
            return !string.IsNullOrEmpty(value) && (value.EndsWith(".png") || value.EndsWith(".jpg") || value.EndsWith(".jpeg") || value.EndsWith(".gif"));
        }
        private void DeleteImage(string imagePath)
        {
            var fullPath = Path.Combine(_imageFolder, Path.GetFileName(imagePath));
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
        private async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            var imageName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var fullPath = Path.Combine(_imageFolder, imageName);

            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return Path.Combine("Images", imageName);
        }
    }
}

