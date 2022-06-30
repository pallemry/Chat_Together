using System;

namespace Form_Functions;
#nullable enable
public static class InputBoxTemplatesFactory
{
    public static InputBox GetNewOtpInputBox(string newMailAddress, int randomOtp)
    {
        const string mainTitle = "Enter the otp you received";
        const string placeHolderText = "Enter OTP..";
        const string messageWhenIncorrectValueEntered = "The OTP we've sent you is not the OTP that you've provided";
        const string titleWhenIncorrectValueEntered = "Wrong OTP";
        const bool shouldCrash = false;

        var title = $"We've sent an OTP to: {newMailAddress}, please enter it";
        var func = (Predicate<string>) (s => s.Trim().Equals(randomOtp.ToString()));

        var predicateInformation = new PredicateInformation<string>(func, messageWhenIncorrectValueEntered,
                                                                    titleWhenIncorrectValueEntered, shouldCrash);
        var textBoxInformation = new TextBoxInformation(title, placeHolderText, predicateInformation);
        return new InputBox(mainTitle, textBoxInformation);
    }

    public static InputBox ChangeEmail(string? originalEmailAddress)
    {
        return new ("Change Email Address",
                    new TextBoxInformation("Email Address",
                                           "Enter your email address here please",
                                           IsEmailValidPredicateInformation(originalEmailAddress)));
    }

    public static InputBox SetUpEmail()
    {
        return new ("Set Up Email",
                    new TextBoxInformation("Enter your email address here please",
                                           "Enter Email..",
                                           IsEmailValidPredicateInformation(null)));
    }

    public static InputBox VerifyPassword(string originalPassword)
    {
        var passwordPredicateInformation = new PredicateInformation<string>(
                                                                            s => s.Equals(originalPassword),
                                                                            "Password given was not the correct password!",
                                                                            "Incorrect Password!");
        var textBoxInformation = new TextBoxInformation("In order to continue, you must verify your password",
                                                        "Enter your password",
                                                        passwordPredicateInformation);
        var passwordInputBox = new InputBox("Confirm password", textBoxInformation);    
        return passwordInputBox;
    }

    private static PredicateInformation<string> IsEmailValidPredicateInformation(string? originalEmailAddress)
    {
        return new (VerifyEmailPredicate(originalEmailAddress),
                    "The input wasn't a valid email address",
                    "Invalid Input");
    }

    private static Predicate<string> VerifyEmailPredicate(string? originalEmailAddress1)
    {
        return s => Globals.IsValidEmail(s) &&
                    !s.Equals(originalEmailAddress1);
    }
}