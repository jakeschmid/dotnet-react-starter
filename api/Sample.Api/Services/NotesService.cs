using Microsoft.EntityFrameworkCore;
using Sample.Api.Data;
using Sample.Api.DTOs;
using Sample.Api.Models;

namespace Sample.Api.Services;

public class NotesService : INotesService
{
    private readonly AppDbContext _db;

    public NotesService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<NoteResponse>> GetAllAsync()
    {
        return await _db.Notes
            .OrderByDescending(n => n.CreatedAt)
            .Select(n => MapToResponse(n))
            .ToListAsync();
    }

    public async Task<NoteResponse?> GetByIdAsync(Guid id)
    {
        var note = await _db.Notes.FindAsync(id);
        return note is null ? null : MapToResponse(note);
    }

    public async Task<NoteResponse> CreateAsync(CreateNoteRequest request)
    {
        var note = new Note
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Content = request.Content,
            CreatedAt = DateTime.UtcNow
        };

        _db.Notes.Add(note);
        await _db.SaveChangesAsync();

        return MapToResponse(note);
    }

    public async Task<NoteResponse?> UpdateAsync(Guid id, UpdateNoteRequest request)
    {
        var note = await _db.Notes.FindAsync(id);
        if (note is null) return null;

        note.Title = request.Title;
        note.Content = request.Content;

        await _db.SaveChangesAsync();

        return MapToResponse(note);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var note = await _db.Notes.FindAsync(id);
        if (note is null) return false;

        _db.Notes.Remove(note);
        await _db.SaveChangesAsync();

        return true;
    }

    private static NoteResponse MapToResponse(Note note) => new()
    {
        Id = note.Id,
        Title = note.Title,
        Content = note.Content,
        CreatedAt = note.CreatedAt
    };
}
