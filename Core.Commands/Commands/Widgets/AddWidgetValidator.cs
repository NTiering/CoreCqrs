namespace Core.Commands.Commands.Widgets;

/// <summary>
/// Ensures the correctness of the values passed in
/// </summary>
public class AddWidgetValidator : AbstractValidator<AddWidgetCommand>
{
    public AddWidgetValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithErrorCode(GetErrorCode("NameEmpty"));
    }

    private string GetErrorCode(string msg)
    {
        return GetType().Name + "." + msg;
    }
}
