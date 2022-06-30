using System;

namespace Form_Functions;

public struct PredicateInformation<T>
{
    /// <summary>
    /// The predicate to check the value of type <see cref="T"/>
    /// </summary>
    public Predicate<T> Predicate { get; }
    /// <summary>
    /// The message to display when the predicate is not met.
    /// </summary>
    public string Message { get; set; }
    /// <summary>
    /// The title of the <see cref="Message"/> to display when the predicate is not met.
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// Tells the <see cref="InputBox"/> whether or not it should crash upon not meeting the <see cref="Predicate"/>, 
    /// by default it is set to true.
    /// </summary>
    public bool ShouldCrash { get; set; }

    // ctor
    public PredicateInformation(Predicate<T> predicate, string message, string title, bool shouldCrash = true)
    {
        Predicate = predicate;
        Message = message;
        Title = title;
        ShouldCrash = shouldCrash;
    }
}