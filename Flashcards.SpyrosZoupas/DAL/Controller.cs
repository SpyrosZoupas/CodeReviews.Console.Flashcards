﻿using Flashcards.DAL.DTO;
using Flashcards.DAL.Model;

namespace Flashcards.DAL;

public class Controller
{
    private readonly Repository _repository;
    
    public Controller(Repository repository)
    {
        _repository = repository;
    }

    public bool CreateFlashcard(string front, string back, string stackName) => 
        _repository.Insert("Flashcard", GetFlashcardObjectForCreate(front,back, stackName));

    public bool CreateStack(string name) =>
        _repository.Insert("Stack", GetStackObject(name));

    public bool CreateStudySession(DateTime date, int score, string stackName) =>
        _repository.Insert("StudySession", new StudySession { Date = date, Score = score, StackID = _repository.GetStackIDByName(stackName) });

    public bool UpdateFlashcard(int id, string front, string back, string stackName) => 
        _repository.Update("Flashcard", GetFlashcardObjectForUpdate(id, front, back, stackName));

    public bool UpdateStack(string currentName, string updatedName) =>
        _repository.Update("Stack", GetStackObject(currentName, updatedName));

    public bool DeleteFlashcard(int id) =>
        _repository.Delete("Flashcard", id);

    public bool DeleteStack(string name) =>
        _repository.Delete("Stack", _repository.GetStackIDByName(name));

    public FlashcardStackDto GetFlashCardByID(int id) =>
        _repository.GetFlashcardByID(id);

    public List<FlashcardStackDto> GetStackByName(string name) =>
        _repository.GetStackByName(name);

    public List<FlashcardStackDto> GetAllFlashcards() =>
        _repository.GetAllFlashcards();

    public List<StackDto> GetAllStacks() =>
        _repository.GetAllStacks();

    public List<StudySessionDto> GetAllStudySessions() =>
        _repository.GetAllStudySessions();

    public List<StudySessionPerMonthDto> GetStudySessionsPerMonthPerStack(int year) =>
        _repository.GetStudySessionsPerMonthPerStack(year);

    public List<StudySessionPerMonthDto> GetAverageScoreOfStudySessionsPerMonthPerStack(int year) =>
        _repository.GetAverageScoreOfStudySessionsPerMonthPerStack(year);

    public bool StackNameExists(string name) =>
        _repository.StackNameExists(name);

    public bool FlashcardIDExists(int id) =>
        _repository.FlashcardIDExists(id);

    private Flashcard GetFlashcardObjectForCreate(string front, string back, string stackName) =>
        new Flashcard
        {
            Front = front,
            Back = back,
            StackID = _repository.GetStackIDByName(stackName)
        };

    private Flashcard GetFlashcardObjectForUpdate(int id, string front, string back, string stackName) =>
        new Flashcard
        {
            ID = id,
            Front = front,
            Back = back,
            StackID = _repository.GetStackIDByName(stackName)
        };

    private Stack GetStackObject(string name) =>
        new Stack
        {
            Name = name
        };

    private Stack GetStackObject(string currentName, string updatedName) =>
        new Stack
        {
            ID = _repository.GetStackIDByName(currentName),
            Name = updatedName
        };
}
