using AutoMapper;
using Comfy.Application.Common.Mappings;
using Comfy.Domain;

namespace Comfy.Application.Handlers.Products.CompleteProduct.DTO;

public record QuestionDTO : IMapWith<Question>
{
    public string Text { get; init; } = null!;
    public int UsefulQuestionCount { get; init; }
    public int NeedlessQuestionCount { get; init; }
    public string Username { get; init; } = null!;
    public IEnumerable<QuestionAnswerDTO> Answers { get; init; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Question, QuestionDTO>()
            .ForMember(dto => dto.Username, x => x.MapFrom(question => question.User.UserName));
    }
}