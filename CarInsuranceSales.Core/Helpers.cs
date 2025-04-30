using Telegram.Bot.Types.ReplyMarkups;

namespace CarInsuranceSales;

/// <summary>
/// Utility class holding helpers methods
/// </summary>
public static class Helpers
{
    /// <summary>
    /// Converts array of actions into reply keyboard markup
    /// </summary>
    /// <param name="actions">An array of keyboard actions</param>
    /// <returns><see cref="ReplyKeyboardMarkup"/></returns>
    public static ReplyKeyboardMarkup ToMarkup(this string[] actions)
    {
        var markup = new ReplyKeyboardMarkup
        {
            OneTimeKeyboard = true,
            ResizeKeyboard = true
        };

        foreach (var action in actions)
        {
            markup.AddButton(action);
        }

        return markup;
    }
}
