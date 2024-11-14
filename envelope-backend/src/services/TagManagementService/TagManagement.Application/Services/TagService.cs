using Envelope.Common.ResultPattern;
using TagManagement.Application.Exceptions;
using TagManagement.Application.Request;
using TagManagement.Application.Response;
using TagManagement.Domain.Entities;
using TagManagement.Application.Repositories;

namespace TagManagement.Application.Services
{
    public class TagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<Result<TagDTO>> AddTag(TagRequest request)
        {
            var exception= await CheckTagData(request);

            if (exception is not null)
            {
                return Result<TagDTO>.OnFailure(exception);
            }

            var tag = new Tag()
            {
                Id = request.TagId,
                Name = request.TagName,
                Type = request.TagType,
                EntityId = request.EntityId,
            };

            await _tagRepository.AddTag(tag);

            var result = new TagDTO()
            {
                TagId = tag.Id,
                TagName = tag.Name,
                TagType = tag.Type,
                EntityId = tag.EntityId,
            };
            return Result<TagDTO>.OnSuccess(result);
        }

        public async Task<Result<TagDTO>> RemoveTag(TagRequest request)
        {
            var tag = new Tag()
            {
                Id = request.TagId
            };          

            var result = new TagDTO()
            {
                TagId = tag.Id,
            };
            await _tagRepository.RemoveTag(result.TagId);
            return Result<TagDTO>.OnSuccess(result);
        }


        public async Task<Result<IEnumerable<TagDTO>>> GetTagsForEntity(TagRequest request)
        {
            var exception = await CheckTagData(request);

            if (exception is not null)
            {
                return Result<IEnumerable<TagDTO>>.OnFailure(exception);
            }

            var tag = new Tag()
            {
                Id = request.TagId,
                Name = request.TagName,
                Type = request.TagType,
                EntityId = request.EntityId,
            };

            var tags = await _tagRepository.GetTagsForEntity(tag.EntityId);

            var result = tags.Select(tag => new TagDTO()
            {
                TagId = tag?.Id ?? Guid.Empty,
                TagName = tag?.Name,
                TagType = tag?.Type ?? default,
                EntityId = tag?.EntityId ?? Guid.Empty,
            }).ToList();

            return Result<IEnumerable<TagDTO>>.OnSuccess(result);
        }
        private async Task<Exception?> CheckTagData(TagRequest request)
        {
            if (request.TagName is null or "")
            {
                return new InvalidNameException($"Invalid tag name: {request.TagName}");
            }
            //возможно нужно добавить еще проверок,но вроде все

            return null;
        }
    }
}
