using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
#nullable enable
namespace Form_Functions;

public struct TextBoxInformation
{
    public string Title { get; }
    public string PlaceHolderText { get; }
    public PredicateInformation<string>[] PredicatesInformation { get; }

    public TextBoxInformation(string title = "Enter input please",
                              string placeHolderText = "Enter input..", 
                              params PredicateInformation<string>[] predicates)
    {
        Title = title;
        PlaceHolderText = placeHolderText;

        PredicatesInformation = GetPredicateArray(predicates);
    }

    private static PredicateInformation<T>[] GetPredicateArray<T>(IEnumerable<PredicateInformation<T>> predicates) => 
        (from information in predicates 
        where information is { Predicate: { } }
        select information).ToArray();
}