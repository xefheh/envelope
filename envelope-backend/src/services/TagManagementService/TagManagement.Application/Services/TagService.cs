using Envelope.Common.ResultPattern;
using TagManagement.Application.Exceptions;
using TagManagement.Application.Request;
using TagManagement.Application.Response;
using TagManagement.Domain.Entities;
using TagManagement.Application.Repositories;
using Envelope.Common.Exceptions;
using TagManagement.Domain.Enums;

namespace TagManagement.Application.Services
{
    public class TagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<Result<Guid>> AddTagAsync(TagRequest request)
        {
            var exception = CheckTagData(request);

            var tagId =Guid.NewGuid();

            if (exception is not null)
            {
                return Result<Guid>.OnFailure(exception);
            }
          
            var tag = new Tag()
            {
                Id= tagId,
                Name=request.TagName,
                Type=request.TagType,
                EntityId=request.EntityId,
            };

            var result = new TagDTO()
            {
                TagId = tag.Id,
                TagName = tag.Name,
                TagType = tag.Type,
                EntityId = tag.EntityId,
            };

            await _tagRepository.AddTagAsync(tag);
            return Result<Guid>.OnSuccess(result.TagId);
        }

        public async Task<Result<bool>> RemoveTagAsync(Guid tagId)
        {
            var result = new TagDTO()
            {
                TagId = tagId,
            };

            var isDeleted=await _tagRepository.RemoveTagAsync(tagId);

            return isDeleted ?
                Result<bool>.OnFailure(new NotFoundException(typeof(Tag), result.TagId)) :
                Result<bool>.OnSuccess(true);
        }


        public async Task<Result<IEnumerable<TagDTO>>> GetTagsForEntityAsync(TagType tagType, Guid entityId)
        {
            var tags = await _tagRepository.GetTagsForEntityAsync(entityId);

            var result = tags.Select(tag => new TagDTO()
            {
                TagId = tag?.Id ?? Guid.Empty,
                TagName = tag?.Name,
                TagType = tag?.Type ?? default,
                EntityId = tag?.EntityId ?? Guid.Empty,
            }).ToList();

            return Result<IEnumerable<TagDTO>>.OnSuccess(result);
        }
        private Exception? CheckTagData(TagRequest request)
        {
            if (String.IsNullOrEmpty(request.TagName))
            {
                return new InvalidNameException($"Invalid tag name: {request.TagName}");
            }
            return null;
        }
    }
}
