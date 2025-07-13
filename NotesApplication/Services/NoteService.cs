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

    public NoteService(INoteRepository noteRepository, ITagRepository tagRepository, IMapper mapper)
    {
        _noteRepository = noteRepository;
        _tagRepository = tagRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<NoteReadDto>> GetNotesAsync()
    {
        var notes = await _noteRepository.GetAllAsync();
        var dtoNotes = notes.Select(n => _mapper.Map<NoteReadDto>(n));
        return dtoNotes;
    }

    public async Task<NoteReadDto?> GetNoteByIdAsync(Guid id)
    {
        var note = await _noteRepository.GetByIdAsync(id);
        
        if (note is null)
            return null;
        
        return _mapper.Map<NoteReadDto>(note);
    }

    public async Task<NoteReadDto> CreateNoteAsync(NoteCreateDto noteDto)
    {
        
        var note = _mapper.Map<NoteCreateDto, Note>(noteDto);
        
        var createdNote =  await _noteRepository.CreateAsync(note);
        
        await _noteRepository.SaveChangesAsync();
        
        return _mapper.Map<Note, NoteReadDto>(createdNote);
    }

    public async Task<NoteReadDto> UpdateNoteAsync(NoteUpdateDto dto, Guid id)
    {
        var note = await _noteRepository.GetByIdAsync(id);

        if (note == null)
        {
            throw new Exception("Note could not be found");
        }
        
        _mapper.Map(dto, note);
        
        var updatedNote = _noteRepository.Update(note);
        await _noteRepository.SaveChangesAsync();

        return _mapper.Map<Note, NoteReadDto>(updatedNote);
    }

    public async Task DeleteNoteAsync(Guid id)
    {
        var note = await _noteRepository.GetByIdAsync(id);

        if (note == null)
        {
            throw new Exception("Note could not be found");
        }
        
        _noteRepository.Delete(note);
        await _noteRepository.SaveChangesAsync();
    }

    public async Task<NoteReadDto> AddTagToNoteAsync(Guid noteId, Guid tagId)
    {
        var tag = await _tagRepository.GetByIdAsync(tagId);
        if (tag == null) throw new Exception("Tag could not be found");

        var note = await _noteRepository.GetByIdAsync(noteId);
        if (note == null) throw new Exception("Note could not be found");
        
        note.Tags.Add(tag);
        _noteRepository.Update(note);
        await _noteRepository.SaveChangesAsync();

        return _mapper.Map<NoteReadDto>(note);
    }

    public async Task RemoveTagFromNoteAsync(Guid noteId, Guid tagId)
    {
        var tag = await _tagRepository.GetByIdAsync(tagId);
        if (tag == null) 
            throw new Exception("Tag could not be found");

        var note = await _noteRepository.GetByIdAsync(noteId);
        if (note == null) 
            throw new Exception("Note could not be found");
        
        if (!note.Tags.Contains(tag))
            throw new Exception("Tag is not assigned to this note");
        
        note.Tags.Remove(tag);
        _noteRepository.Update(note);
        await _noteRepository.SaveChangesAsync();
    }
}