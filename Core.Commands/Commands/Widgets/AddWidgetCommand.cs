namespace Core.Commands.Commands.Widgets;

/// <summary>
/// Command used to pass in values 
/// </summary>
/// <param name="Name"></param>
public record AddWidgetCommand(string Name) : BaseCommand<AddWidgetResult>;
