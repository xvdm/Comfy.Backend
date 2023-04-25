using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain;

namespace Comfy.Application.Handlers.Products.CompleteProduct.DTO;

public class QuestionDTO : IMapWith<Question>
{
    public string Text { get; set; } = null!;
    public int UsefulQuestionCount { get; set; }
    public int NeedlessQuestionCount { get; set; }
    public string Username { get; set; } = null!;

    public IEnumerable<QuestionAnswerDTO> Answers { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Question, QuestionDTO>()
            .ForMember(dto => dto.Username, x => x.MapFrom(question => question.User.UserName));
    }
}