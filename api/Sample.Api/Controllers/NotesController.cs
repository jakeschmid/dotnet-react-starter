using Microsoft.AspNetCore.Mvc;
using Sample.Api.DTOs;
using Sample.Api.Services;

namespace Sample.Api.Controllers;

/// <summary>
/// Manages notes resources.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class NotesController : ControllerBase
{
    private readonly INotesService _notesService;

    public NotesController(INotesService notesService)
    {
        _notesService = notesService;
    }

    /// <summary>
    /// Retrieves all notes.
    /// </summary>
    /// <returns>A list of all notes ordered by creation date (newest first).</returns>
    /// <response code="200">Returns the list of notes.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<NoteResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var notes = await _notesService.GetAllAsync();
        return Ok(notes);
    }

    /// <summary>
    /// Retrieves a single note by ID.
    /// </summary>
    /// <param name="id">The unique identifier of the note.</param>
    /// <returns>The note with the specified ID.</returns>
    /// <response code="200">Returns the requested note.</response>
    /// <response code="404">If the note does not exist.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(NoteResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var note = await _notesService.GetByIdAsync(id);
        if (note is null) return NotFound();
        return Ok(note);
    }

    /// <summary>
    /// Creates a new note.
    /// </summary>
    /// <param name="request">The note creation payload.</param>
    /// <returns>The newly created note.</returns>
    /// <response code="201">Returns the created note.</response>
    /// <response code="400">If the request payload is invalid.</response>
    [HttpPost]
    [ProducesResponseType(typeof(NoteResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateNoteRequest request)
    {
        var note = await _notesService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = note.Id }, note);
    }

    /// <summary>
    /// Updates an existing note.
    /// </summary>
    /// <param name="id">The unique identifier of the note to update.</param>
    /// <param name="request">The update payload.</param>
    /// <returns>The updated note.</returns>
    /// <response code="200">Returns the updated note.</response>
    /// <response code="400">If the request payload is invalid.</response>
    /// <response code="404">If the note does not exist.</response>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(NoteResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateNoteRequest request)
    {
        var note = await _notesService.UpdateAsync(id, request);
        if (note is null) return NotFound();
        return Ok(note);
    }

    /// <summary>
    /// Deletes a note by ID.
    /// </summary>
    /// <param name="id">The unique identifier of the note to delete.</param>
    /// <returns>No content on success.</returns>
    /// <response code="204">Note deleted successfully.</response>
    /// <response code="404">If the note does not exist.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _notesService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
