using AutoMapper;
using NotesApplication.DTOs;
using NotesApplication.Repositories;

namespace NotesApplication.Services;

using Data;
using Models;

public class NoteService : INoteService
{
    private readonly INoteRepository _noteRepository;
    private readonly ITagRepository _tagRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<NoteService> _logger;

    public NoteService(INoteRepository noteRepository, ITagRepository tagRepository, IMapper mapper, ILogger<NoteService> logger)
    {
        _noteRepository = noteRepository;
        _tagRepository = tagRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<NoteReadListDto>> GetNotesAsync()
    {
        _logger.LogInformation("Getting notes");
        var notes = await _noteRepository.GetAllAsync();
        var dtoNotes = notes.Select(n => _mapper.Map<NoteReadListDto>(n));
        _logger.LogInformation($"Notes - {dtoNotes}");
        return dtoNotes;
    }

    public async Task<NoteReadDetailDto> GetNoteByIdAsync(Guid id)
    {
        var note = await _noteRepository.GetByIdAsync(id);

        if (note is null)
            throw new KeyNotFoundException($"Tag could not be found, id = {id}");
        
        return _mapper.Map<NoteReadDetailDto>(note);
    }

    public async Task<NoteReadDetailDto> CreateNoteAsync(NoteCreateDto noteDto)
    {
        
        var note = _mapper.Map<NoteCreateDto, Note>(noteDto);
        
        var createdNote =  await _noteRepository.CreateAsync(note);
        
        await _noteRepository.SaveChangesAsync();
        
        return _mapper.Map<Note, NoteReadDetailDto>(createdNote);
    }

    public async Task<NoteReadDetailDto> UpdateNoteAsync(NoteUpdateDto dto, Guid id)
    {
        var note = await _noteRepository.GetByIdAsync(id);

        if (note == null) 
            throw new KeyNotFoundException($"Note could not be found, id = {id}");
        
        _mapper.Map(dto, note);
        
        var updatedNote = _noteRepository.Update(note);
        await _noteRepository.SaveChangesAsync();

        return _mapper.Map<Note, NoteReadDetailDto>(updatedNote);
    }

    public async Task DeleteNoteAsync(Guid id)
    {
        var note = await _noteRepository.GetByIdAsync(id);

        if (note == null)
            throw new KeyNotFoundException($"Note could not be found, id = {id}");
        
        _noteRepository.Delete(note);
        await _noteRepository.SaveChangesAsync();
    }

    public async Task<NoteReadDetailDto> AddTagToNoteAsync(Guid noteId, Guid tagId)
    {
        var tag = await _tagRepository.GetByIdAsync(tagId);
        if (tag == null) 
            throw new KeyNotFoundException($"Tag could not be found, id = {tagId}");

        var note = await _noteRepository.GetByIdAsync(noteId);
        if (note == null) 
            throw new KeyNotFoundException($"Note could not be found, id = {noteId}");
        
        note.Tags.Add(tag);
        _noteRepository.Update(note);
        await _noteRepository.SaveChangesAsync();

        return _mapper.Map<NoteReadDetailDto>(note);
    }

    public async Task RemoveTagFromNoteAsync(Guid noteId, Guid tagId)
    {
        var tag = await _tagRepository.GetByIdAsync(tagId);
        if (tag == null) 
            throw new KeyNotFoundException($"Tag could not be found, id = {tagId}");

        var note = await _noteRepository.GetByIdAsync(noteId);
        if (note == null) 
            throw new KeyNotFoundException($"Note could not be found, id = {noteId}");
        
        if (!note.Tags.Contains(tag))
            throw new KeyNotFoundException("Tag is not assigned to this note");
        
        note.Tags.Remove(tag);
        _noteRepository.Update(note);
        await _noteRepository.SaveChangesAsync();
    }
}