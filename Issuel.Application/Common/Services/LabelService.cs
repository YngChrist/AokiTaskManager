using Ardalis.GuardClauses;
using Issuel.Application.Common.Dto.Label;
using Issuel.Application.Common.Dto.Label.Requests;
using Issuel.Application.Common.Interfaces;
using Issuel.Application.Common.Mappings;
using Issuel.Domain.Entities;
using Issuel.Domain.Validation;

namespace Issuel.Application.Common.Services;

public class LabelService : ILabelService
{
    private readonly ILabelRepository _labelRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly LabelMapper _labelMapper;

    public LabelService(ILabelRepository labelRepository, IUnitOfWork unitOfWork, LabelMapper labelMapper)
    {
        _labelRepository = labelRepository;
        _unitOfWork = unitOfWork;
        _labelMapper = labelMapper;
    }

    public async Task<LabelDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Guard.Against.Default(id);
        
        var label = await _labelRepository.GetByIdAsync(id, cancellationToken);
        ThrowIf.EntityIsNull(label, id, nameof(Label));
        
        return _labelMapper.Map(label!);
    }
    
    public async Task<LabelDto[]> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var labels = await _labelRepository.GetAllAsync(cancellationToken);
        
        return _labelMapper.Map(labels);
    }
    
    public async Task<LabelDto> CreateAsync(CreateLabelRequest request, CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(request, nameof(CreateLabelRequest));

        var label = new Label(request.Name);
        await _labelRepository.AddAsync(label, cancellationToken);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return _labelMapper.Map(label);
    }

    public async Task<LabelDto> UpdateAsync(UpdateLabelRequest request, CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(request, nameof(UpdateLabelRequest));
        
        var label = await _labelRepository.GetByIdAsync(request.Id, cancellationToken);
        ThrowIf.EntityIsNull(label, request.Id, nameof(Label));
        
        label!.Update(request.Name);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return _labelMapper.Map(label);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Guard.Against.Default(id);

        var label = await _labelRepository.GetByIdAsync(id, cancellationToken);
        ThrowIf.EntityIsNull(label, id, nameof(Label));

        await _labelRepository.DeleteAsync(label!, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}