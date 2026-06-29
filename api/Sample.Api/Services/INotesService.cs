using Sample.Api.DTOs;

namespace Sample.Api.Services;

public interface INotesService
{
    Task<IEnumerable<NoteResponse>> GetAllAsync();
    Task<NoteResponse?> GetByIdAsync(Guid id);
    Task<NoteResponse> CreateAsync(CreateNoteRequest request);
    Task<NoteResponse?> UpdateAsync(Guid id, UpdateNoteRequest request);
    Task<bool> DeleteAsync(Guid id);
}
